using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator
{
    private const int MINIMUM_RANGE_WIDTH = 6;  //部屋の最小幅

    private int m_mapSizeX;   //ダンジョンの最大X軸
    private int m_mapSizeY;   //ダンジョンの最大Y軸
    private int m_maxRoom;    //最大部屋数

    private List<Range> m_roomList = new List<Range>();     //部屋リスト
    private List<Range> m_rangeList = new List<Range>();    //区画リスト
    private List<Range> m_passList = new List<Range>();     //通路リスト
    private List<Range> m_roomPassList = new List<Range>(); //出入口リスト

    public int[,] GenerateMap(int mapSizeX, int mapSizeY, int maxRoom)
    {
        this.m_mapSizeX = mapSizeX;
        this.m_mapSizeY = mapSizeY;
        this.m_maxRoom = maxRoom;

        int[,] map = new int[mapSizeX, mapSizeY];

        CreateRange(maxRoom);
        CreateRoom();

        // ここまでの結果を一度配列に反映する
        foreach (Range pass in m_passList)
        {
            for (int x = pass.Start.X; x <= pass.End.X; x++)
            {
                for (int y = pass.Start.Y; y <= pass.End.Y; y++)
                {
                    map[x, y] = 1;
                }
            }
        }

        foreach (Range roomPass in m_roomPassList)
        {
            for (int x = roomPass.Start.X; x <= roomPass.End.X; x++)
            {
                for (int y = roomPass.Start.Y; y <= roomPass.End.Y; y++)
                {
                    map[x, y] = 1;
                }
            }
        }

        foreach (Range room in m_roomList)
        {
            for (int x = room.Start.X; x <= room.End.X; x++)
            {
                for (int y = room.Start.Y; y <= room.End.Y; y++)
                {
                    map[x, y] = 1;
                }
            }
        }

        TrimPassList(ref map);

        return map;
    }

    public void CreateRange(int maxRoom)
    {
        //区画のリストの初期値としてマップ全体を入れる
        m_rangeList.Add(new Range(0, 0, m_mapSizeX - 1, m_mapSizeY - 1));

        bool isDevided;
        do
        {
            //タテ→ヨコの順で部屋を区切る
            isDevided = DevideRange(false);
            isDevided = DevideRange(true) || isDevided;

            //最大区画数（部屋数）を超えたら終了
            if (m_rangeList.Count >= maxRoom) break;
        } while (isDevided);
    }

    //マップ区切り処理
    public bool DevideRange(bool isVertical)
    {
        bool isDevided = false;

        //区画ごとに切るか判定
        List<Range> newRangeList = new List<Range>();
        foreach (Range range in m_rangeList)
        {
            //これ以上分割できない場合は次の区画へ
            if (isVertical && range.GetWidthY() < MINIMUM_RANGE_WIDTH * 2 + 1)
            {
                continue;
            }
            else if (!isVertical && range.GetWidthX() < MINIMUM_RANGE_WIDTH * 2 + 1)
            {
                continue;
            }

            System.Threading.Thread.Sleep(1);

            //40%の確率で分割
            //区画数が１つなら必ず分割
            if (m_rangeList.Count > 1 && Utility.RandomJudge(0.4f))
            {
                continue;
            }

            //長さから最小の区画サイズ２つ分を引き、残りからランダムで分割位置を決める
            int length = isVertical ? range.GetWidthY() : range.GetWidthX();
            int margin = length - MINIMUM_RANGE_WIDTH * 2;
            int baseIndex = isVertical ? range.Start.Y : range.Start.X;
            int devideIndex = baseIndex + MINIMUM_RANGE_WIDTH + Utility.GetRandomInt(1, margin) - 1;

            //分割された区画の大きさを変更し、新しい区画を追加リストに追加
            //同時に、分割した境界を通路として保存
            Range newRange = new Range();
            if (isVertical)
            {
                m_passList.Add(new Range(range.Start.X, devideIndex, range.End.X, devideIndex));
                newRange = new Range(range.Start.X, devideIndex + 1, range.End.X, range.End.Y);
                range.End.Y = devideIndex - 1;
            }
            else
            {
                m_passList.Add(new Range(devideIndex, range.Start.Y, devideIndex, range.End.Y));
                newRange = new Range(devideIndex + 1, range.Start.Y, range.End.X, range.End.Y);
                range.End.X = devideIndex - 1;
            }

            //追加リストに新しい区画を退避
            newRangeList.Add(newRange);

            isDevided = true;
        }

        //追加リストに退避しておいた新しい区画を追加
        m_rangeList.AddRange(newRangeList);

        return isDevided;
    }

    //部屋を作る処理
    private void CreateRoom()
    {
        //部屋のない区画が偏らないようにリストをシャッフル
        m_rangeList.Sort((a, b) => Utility.GetRandomInt(0, 1) - 1);

        //１区画あたり１部屋を作成。作成しない区画もある
        foreach (Range range in m_rangeList)
        {
            System.Threading.Thread.Sleep(1);
            //30％の確率で部屋を作らない
            //最大部屋数の半分に満たない場合は作る
            if (m_roomList.Count > m_maxRoom / 2 && Utility.RandomJudge(0.3f)) continue;

            //猶予を計算
            int marginX = range.GetWidthX() - MINIMUM_RANGE_WIDTH + 1;
            int marginY = range.GetWidthY() - MINIMUM_RANGE_WIDTH + 1;

            //開始位置を決定
            int randomX = Utility.GetRandomInt(1, marginX);
            int randomY = Utility.GetRandomInt(1, marginY);

            //部屋の各座標を計算
            int startX = range.Start.X + randomX;
            int endX = range.End.X - Utility.GetRandomInt(0, (marginX - randomX)) - 1;
            int startY = range.Start.Y + randomY;
            int endY = range.End.Y - Utility.GetRandomInt(0, (marginY - randomY)) - 1;

            //部屋リストへ追加
            Range room = new Range(startX, startY, endX, endY);
            m_roomList.Add(room);

            //通路を作る
            CreatePass(range, room);
        }
    }


    //出入口を作る処理
    private void CreatePass(Range range, Range room)
    {
        List<int> directionList = new List<int>();
        if (range.Start.X != 0)
        {
            //Xマイナス方向
            directionList.Add(0);
        }
        if (range.End.X != m_mapSizeX - 1)
        {
            //Xプラス方向
            directionList.Add(1);
        }
        if (range.Start.Y != 0)
        {
            //Yマイナス方向
            directionList.Add(2);
        }
        if (range.End.Y != m_mapSizeY - 1)
        {
            //Yプラス方向
            directionList.Add(3);
        }

        //通路の有無が偏らないよう、リストをシャッフル
        directionList.Sort((a, b) => Utility.GetRandomInt(0, 1) - 1);

        bool isFirst = true;
        foreach (int direction in directionList)
        {
            System.Threading.Thread.Sleep(1);
            //80％の確率で通路を作らない
            //通路がない場合は必ず作る
            if (!isFirst && Utility.RandomJudge(0.8f))
            {
                continue;
            }
            else
            {
                isFirst = false;
            }

            //向きの判定
            int random;
            switch (direction)
            {
                case 0:// Xマイナス方向
                    random = room.Start.Y + Utility.GetRandomInt(1, room.GetWidthY()) - 1;
                    m_roomPassList.Add(new Range(range.Start.X, random, room.Start.X - 1, random));
                    break;

                case 1:// Xプラス方向
                    random = room.Start.Y + Utility.GetRandomInt(1, room.GetWidthY()) - 1;
                    m_roomPassList.Add(new Range(room.End.X + 1, random, range.End.X, random));
                    break;

                case 2:// Yマイナス方向
                    random = room.Start.X + Utility.GetRandomInt(1, room.GetWidthX()) - 1;
                    m_roomPassList.Add(new Range(random, range.Start.Y, random, room.Start.Y - 1));
                    break;

                case 3:// Yプラス方向
                    random = room.Start.X + Utility.GetRandomInt(1, room.GetWidthX()) - 1;
                    m_roomPassList.Add(new Range(random, room.End.Y + 1, random, range.End.Y));
                    break;
            }
        }
    }



    //余分な通路を削除する処理
    private void TrimPassList(ref int[,] map)
    {
        //どの部屋の通路からも接続されなかった通路を削除
        for (int i = m_passList.Count - 1; i >= 0; i--)
        {
            Range pass = m_passList[i];

            bool isVertical = pass.GetWidthY() > 1;

            //通路が部屋通路から接続されているかチェック
            bool isTrimTarget = true;
            if (isVertical)
            {
                int x = pass.Start.X;
                for (int y = pass.Start.Y; y <= pass.End.Y; y++)
                {
                    if (map[x - 1, y] == 1 || map[x + 1, y] == 1)
                    {
                        isTrimTarget = false;
                        break;
                    }
                }
            }
            else
            {
                int y = pass.Start.Y;
                for (int x = pass.Start.X; x <= pass.End.X; x++)
                {
                    if (map[x, y - 1] == 1 || map[x, y + 1] == 1)
                    {
                        isTrimTarget = false;
                        break;
                    }
                }
            }

            //削除対象となった通路を削除
            if (isTrimTarget)
            {
                m_passList.Remove(pass);

                //マップ配列からも削除
                if (isVertical)
                {
                    int x = pass.Start.X;
                    for (int y = pass.Start.Y; y <= pass.End.Y; y++)
                    {
                        map[x, y] = 0;
                    }
                }
                else
                {
                    int y = pass.Start.Y;
                    for (int x = pass.Start.X; x <= pass.End.X; x++)
                    {
                        map[x, y] = 0;
                    }
                }
            }
        }

        //外周に接している通路を別の通路との接続点まで削除
        //上下基準で探査
        for (int x = 0; x < m_mapSizeX - 1; x++)
        {
            if (map[x, 0] == 1)
            {
                for (int y = 0; y < m_mapSizeY; y++)
                {
                    if (map[x - 1, y] == 1 || map[x + 1, y] == 1) break;
                    map[x, y] = 0;
                }
            }

            if (map[x, m_mapSizeY - 1] == 1)
            {
                for (int y = m_mapSizeY - 1; y >= 0; y--)
                {
                    if (map[x - 1, y] == 1 || map[x + 1, y] == 1) break;
                    map[x, y] = 0;
                }
            }
        }

        //左右基準で探査
        for (int y = 0; y < m_mapSizeY - 1; y++)
        {
            if (map[0, y] == 1)
            {
                for (int x = 0; x < m_mapSizeY; x++)
                {
                    if (map[x, y - 1] == 1 || map[x, y + 1] == 1) break;
                    map[x, y] = 0;
                }
            }

            if (map[m_mapSizeX - 1, y] == 1)
            {
                for (int x = m_mapSizeX - 1; x >= 0; x--)
                {
                    if (map[x, y - 1] == 1 || map[x, y + 1] == 1) break;
                    map[x, y] = 0;
                }

            }
        }
    }
}
