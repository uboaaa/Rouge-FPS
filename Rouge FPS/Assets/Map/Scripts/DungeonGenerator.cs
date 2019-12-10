using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//***交差判定が上手く行ってない？***
//***一つ前の判定に変更？***

public enum Direction { LEFT, RIGHT, UP, DOWN };    //方向判定用のキー

public class DungeonGenerator
{
    private const int MINIMUM_RANGE_WIDTH = 10;  //区画の最小幅

    private int m_mapSizeX;     //ダンジョンの最大X軸
    private int m_mapSizeY;     //ダンジョンの最大Y軸
    private int m_maxRoom;      //最大部屋数
    private int m_idCount = 0;  //区画のIDのカウント

    private Dictionary<int, Range> m_rangeList = new Dictionary<int, Range>();    //区画リスト

    private List<Pass> m_passList = new List<Pass>();       //通路リスト
    private List<int[]> m_rangeIdComb = new List<int[]>();  //区画組み合わせリスト 

    //================================================//
    //ダンジョン生成関数群
    //================================================//
    //ベースマップ生成
    //※部屋のみ生成
    //※生成後、二次配列を返す
    public int[,] GenerateBaseMap(int _mapSizeX, int _mapSizeY, int _maxRoom)
    {
        this.m_mapSizeX = _mapSizeX;
        this.m_mapSizeY = _mapSizeY;
        this.m_maxRoom = _maxRoom;

        int[,] map = new int[MapInitializer.MAP_SIZE_X, MapInitializer.MAP_SIZE_Y];

        //区画を生成
        CreateRange(m_maxRoom);

        
        //部屋を生成
        CreateRoom();

        return map;
    }

    //通路生成
    //※部屋データを組み込んだマップに通路を配置
    public int[,] CompPassMap(int[,] _map)
    {

        //通路を生成
        CreatePass();

        //余分な通路を削除
        //DeletePass();


        //通路
        //***マップのログ出力データを16進数にして、各IDがわかるようにしたい
        foreach (Pass pass in m_passList)
        {
            
            Position start = pass.Start;
            Position end = pass.End;

            if (start.X > end.X || start.Y > end.Y)
            {
                Position tmp = start;
                start = end;
                end = tmp;
            }

            for (int x = start.X; x <= end.X; x++)
            {
                for (int y = start.Y; y <= end.Y; y++)
                {
                    _map[x, y] = 2;
                }
            }
        }

        return _map;
    }

    //===================================================================//
    //区画を生成
    //===================================================================//
    private void CreateRange(int _maxRoom)
    {
        //区画のリストの初期値としてマップ全体を入れる
        m_rangeList.Add(m_idCount, new Range(0, 0, m_mapSizeX - 1, m_mapSizeY - 1, m_idCount));
        //区画id用のカウントをアップ
        m_idCount += 1;

       
        //部屋数に合わせて分割
        //分割回数を設定
        int devCount = _maxRoom - 1;
        int count = 0;
        int loopCnt = 0;
        do
        {
            //ヨコ分割
            //分割した時カウントアップ
            count += SplitRange(true) ? 1 : 0;

            //部屋数チェック
            if (count >= devCount) break;

            //タテ分割
            //分割した時カウントアップ
            count += SplitRange(false) ? 1 : 0;

            //部屋数チェック
            if (count >= devCount) break;

            //一定ループ回数を超過した場合、分割数を減らす
            loopCnt++;
            if (loopCnt > 100)
            {
                Debug.Log("分割数を削減");
                devCount -= 1;
                loopCnt = 0;
            }

        } while (true);

        //区画ごとに辺情報を設定
        foreach (KeyValuePair<int, Range> range in m_rangeList)
        {
            range.Value.SetLines();
        }

        //区画ごとに隣接している区画の確認
        foreach (KeyValuePair<int, Range> range in m_rangeList)
        {
            //Debug.Log("ID:" + range.Key + ",始点:(" + range.Value.Start.X + "," + range.Value.Start.Y + "),終点:(" + range.Value.End.X + "," + range.Value.End.Y + ")");
            //隣接する区画をチェック
            CheckNext(range.Value);
        }
    }

    //==================================================================//
    //部屋数に応じて分割する処理
    //==================================================================//
    private bool SplitRange(bool _isVertical)
    {
        bool isSplit = false;

        foreach (KeyValuePair<int,Range> range in m_rangeList)
        {
            //これ以上分割できない場合は次の区画へ
            if (_isVertical && range.Value.width_Y() < MINIMUM_RANGE_WIDTH * 2 + 1)
            {
                continue;
            }
            else if (!_isVertical && range.Value.width_X() < MINIMUM_RANGE_WIDTH * 2 + 1)
            {
                continue;
            }
            System.Threading.Thread.Sleep(1);

            //20%の確率で分割
            //区画数が１つなら必ず分割
            if (m_rangeList.Count > 1 && Utility.RandomJudge(0.2f))
            {
                continue;
            }

            //長さから最小の区画サイズ２つ分を引き、残りからランダムで分割位置を決める
            int length = _isVertical ? range.Value.width_Y() : range.Value.width_X();
            int margin = length - MINIMUM_RANGE_WIDTH * 2 ;                                             //区分け可能の余分幅
            int baseIndex = _isVertical ? range.Value.Start.Y : range.Value.Start.X;                    //最小基準の位置
            int devideIndex = baseIndex + MINIMUM_RANGE_WIDTH + Utility.GetRandomInt(1, margin) - 1;    //分割位置

            //分割された区画の大きさを変更し、新しい区画を追加リストに追加
            //同時に、分割した境界を通路として保存→通路分を引かないので保存しない
            Range newRange = new Range();
            if (_isVertical)
            {
                newRange = new Range(range.Value.Start.X, devideIndex, range.Value.End.X, range.Value.End.Y, m_idCount);                //新しい区画を生成
                range.Value.End.Y = devideIndex;                                                                                        //分割された区画の座標変更
            }
            else
            {
                newRange = new Range(devideIndex, range.Value.Start.Y, range.Value.End.X, range.Value.End.Y, m_idCount);
                range.Value.End.X = devideIndex;
            }

            //区画追加
            m_rangeList.Add(m_idCount, newRange);

            //区画id用のカウントをアップ、分割フラグをtrue
            m_idCount += 1;
            isSplit = true;

            //foreachを抜ける
            break;
        }

        //分割したのでtrue
        return isSplit;
    }

    
    //=======================================================================//
    //隣接する区画があるかチェックする処理
    //=======================================================================//
    private void CheckNext(Range _self)
    {
        //各区画のChangeVertexで検索
        foreach (KeyValuePair<int, Range> other in m_rangeList)
        {
            //同じ区画IDは飛ばす
            if (_self.Id == other.Value.Id) continue;
            //隣接した場合、組み合わせをリストにセット
            if (_self.IsNext(other.Value))
            {
                int[] newComb = new int[2];
                newComb[0] = _self.Id;
                newComb[1] = other.Value.Id;
                if (newComb[0] > newComb[1])
                {
                    int tmp = newComb[0];
                    newComb[0] = newComb[1];
                    newComb[1] = tmp;
                }
                //組み合わせをセット
                //交差判定が上手くいってない？
                SetCombo(newComb);
            }

        }
    }

    //=========================================================================//
    //組み合わせリスト内に同じ組み合わせがないか探査する処理
    //****逐一探査するので、最後にまとめてソート後探査したい****
    //=========================================================================//
    private void SetCombo(int[] _newComb)
    {
        //各組合せと比較する
        foreach (int[] comb in m_rangeIdComb)
        {
            //組み合わせが違うなら次へ
            if (comb[0] != _newComb[0]) continue;
            if (comb[1] != _newComb[1]) continue;
            //同じの場合はセットせずに関数を終了
            return;
        }
        //組み合わせがリストになければ、新たに追加
        m_rangeIdComb.Add(_newComb);
    }

    //=======================================================================//
    //部屋を作る処理
    //=======================================================================//
    private void CreateRoom()
    {
        //Debug.Log(m_rangeList.Count);

        //１区画あたり１部屋を作成
        foreach (KeyValuePair<int, Range> range in m_rangeList)
        {
            System.Threading.Thread.Sleep(1);
            
            //部屋の各座標を計算
           
            int startX = range.Value.Start.X + 2;
            int endX = range.Value.End.X - 2;
            int startY = range.Value.Start.Y + 2;
            int endY = range.Value.End.Y - 2;

            //この区画の部屋をセット
            Room room = new Room(startX, startY, endX, endY);
            range.Value.m_Room = room;
        }
    }

    //=========================================================//
    //出入口を作る処理
    //=========================================================//
    private void CreatePass()
    {
        Debug.Log(m_rangeIdComb.Count);

        foreach (int[] rangeId in m_rangeIdComb)
        {
            
            //組み合わせリストのIDからそれぞれの区画を取得
            Range rangeA = m_rangeList[rangeId[0]];
            Range rangeB = m_rangeList[rangeId[1]];

            //部屋がセットされていない場合次へ
            if (rangeA.m_Room == null) continue;
            if (rangeB.m_Room == null) continue;

            //取得した向きから直線で通路を生成できるかを確認
            //※できた場合：第二引数に通路クラスを返す,Trueを返す、できない場合：Falseを返す
            Pass newPass;
            bool isStraight = rangeA.IsStraight(rangeB, out newPass);

            //失敗した時、生成元と先を交換して再度確認
            if (!isStraight)
            {
                isStraight = rangeB.IsStraight(rangeA, out newPass);
            }

            //更に失敗した時、
            if (!isStraight)
            {
                //角ありの通路の始点、中継点を求める


                continue; //とりあえず直線のみ
            }

            //生成された通路データに区画の組み合わせを保存
            //newPass.m_idComb[0] = rangeId[0];
            //newPass.m_idComb[1] = rangeId[1];

            //始点or中継点から終点までの通路をリストに追加
            m_passList.Add(newPass);

        }
        Debug.Log(m_passList.Count);
        
    }

    private void DeletePass()
    {

    }


    //========================================
    //取得用関数群
    //========================================
    public Dictionary<int,Room> GetRooms()
    {
        Dictionary<int, Room> _list = new Dictionary<int, Room>();
        foreach(KeyValuePair<int,Range> _range in m_rangeList)
        {
            _list.Add(_range.Key, _range.Value.m_Room);
        }

        return _list;
    }

    public void SetRooms(Dictionary<int,Room> _list)
    {
        foreach(KeyValuePair<int,Room> _room in _list)
        {
            m_rangeList[_room.Key].m_Room = _room.Value;
        }
    }

    public int SmallistRoom(out Room _room)
    {
        //最小面積の部屋のIDを取得
        int smallist = 0;
        int id = -1;
        foreach(KeyValuePair<int,Range> _pair in m_rangeList)
        {
            int tmp = _pair.Value.m_Room.calcArea();
            if(smallist==0 || tmp <= smallist)
            {
                smallist = tmp;
                id = _pair.Key;
            }
        }

        //取得したIDとその情報を返す
        _room = m_rangeList[id].m_Room;
        return id;
    }
}
