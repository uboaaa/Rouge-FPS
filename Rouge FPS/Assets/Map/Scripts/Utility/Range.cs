using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range
{

    public Position Start { get; set; } //始点
    public Position End { get; set; }   //終点
    public int Id { get; set; }         //区画のid

    public Room m_Room = null;

    public Dictionary<int, Direction> m_connectIdList = new Dictionary<int, Direction>();   //接している区画リスト（区画ID,接している辺の方向）
    private Dictionary<Direction, Line> m_lineList = new Dictionary<Direction, Line>();     //辺リスト（辺の方向,辺）
    public Dictionary<Direction, List<Position>> m_PassPositionDic { get; } = new Dictionary<Direction, List<Position>>();  //通路生成地点リスト（通路のある方向、座標）


    //==================================
    //幅の取得
    //==================================
    //ヨコ
    public int width_X()
    {
        return End.X - Start.X + 1;
    }
    //タテ
    public int width_Y()
    {
        return End.Y - Start.Y + 1;
    }

    //==================================
    //各頂点の座標取得
    //==================================
    //左上
    public Position vertex_LeftUp()
    {
        return Start;
    }
    //右上
    public Position vertex_RightUp()
    {
        return new Position(End.X, Start.Y);
    }
    //左下
    public Position vertex_LeftDown()
    {
        return new Position(Start.X, End.Y);
    }
    //右下
    public Position vertex_RightDown()
    {
        return End;
    }

    //==================================
    //辺の取得
    //==================================
    public Line line_Left()
    {
        return m_lineList[Direction.LEFT];
    }

    public Line line_Right()
    {
        return m_lineList[Direction.RIGHT];
    }

    public Line line_Up()
    {
        return m_lineList[Direction.UP];
    }

    public Line line_Down()
    {
        return m_lineList[Direction.DOWN];
    }

    //==================================
    //ライン情報を設定
    //***区画切り分け後に行う
    //==================================
    public void SetLines()
    {
        //辺
        m_lineList.Add(Direction.LEFT, new Line(vertex_LeftUp(), vertex_LeftDown()));     //左辺
        m_lineList.Add(Direction.RIGHT, new Line(vertex_RightUp(), vertex_RightDown()));  //右辺
        m_lineList.Add(Direction.UP, new Line(vertex_LeftUp(), vertex_RightUp()));        //上辺
        m_lineList.Add(Direction.DOWN, new Line(vertex_LeftDown(), vertex_RightDown()));  //下辺
    }

    //==================================
    //隣接チェック
    //隣接している場合、IDを返す。なければ-1
    //==================================
    public bool IsNext(Range other)
    {
        //自身の区画の辺と他の区画の辺で交差判定
        //交差した場合は区画のIDを返す
        //左辺
        if (this.line_Left().IsClossing(other.line_Right()))
        {
            m_connectIdList.Add(other.Id, Direction.LEFT);
            return true;
        }
        //右辺
        if (this.line_Right().IsClossing(other.line_Left()))
        {
            m_connectIdList.Add(other.Id, Direction.RIGHT);
            return true;
        }
        //上辺
        if (this.line_Up().IsClossing(other.line_Down()))
        {
            m_connectIdList.Add(other.Id, Direction.UP);
            return true;
        }
        //下辺
        if (this.line_Down().IsClossing(other.line_Up()))
        {
            m_connectIdList.Add(other.Id, Direction.DOWN);
            return true;
        }

        return false;
    }

    //=====================================
    //直線チェック
    //引数：接続確認したい区画
    //延ばした直線が指定の部屋に接続される場合、接続可能な始点座標を返す
    //=====================================

    //***リスト関連のエラーあるから直す！！！

    public bool IsStraight(Range range, out Pass pass)
    {

        //指定した先の区画への向きを取得
        Direction direction = m_connectIdList[range.Id];

        //指定した先の区画の部屋が直線で接続できるか確認するためのフラグ
        bool connect_Start = false;     //始点（左上）で確認用
        bool connect_End = false;       //終点（右下）で確認用

        //各座標用の変数
        int self_start = 0, self_end = 0, other_start = 0, other_end = 0;

        switch (direction)
        {
            //左右に隣接している場合は部屋の各Y軸座標をセット
            case Direction.LEFT:
            case Direction.RIGHT:
                self_start = m_Room.Start.Y;
                self_end = m_Room.End.Y;
                other_start = range.m_Room.Start.Y;
                other_end = range.m_Room.End.Y;
                break;
            //上下に隣接している場合は各X軸座標をセット
            case Direction.UP:
            case Direction.DOWN:
                self_start = m_Room.Start.X;
                self_end = m_Room.End.X;
                other_start = range.m_Room.Start.X;
                other_end = range.m_Room.End.X;
                break;
        }


        //条件分け用のフラグをセット
        //***条件分岐が限定的？見直し必要
        if (self_start <= other_start && self_end >= other_start) connect_Start = true;
        if (self_start <= other_end && self_end >= other_end) connect_End = true;



        //直線を伸ばせる地点を条件分け
        //接続できる場合は通路生成

        pass = new Pass();    //passを初期化***戻り値をタプル型に変更？

        if (connect_Start && connect_End)               //指定先の部屋から引いた直線は全て接続できる
        {
            pass = CreateStraightPass(other_start, other_end, range, direction);

        }
        else if (connect_Start && !connect_End)         //指定先の部屋の始点～指定元の部屋の終点
        {
            pass = CreateStraightPass(other_start, self_end , range, direction);

        }
        else if (!connect_Start && connect_End)         //指定元の部屋の始点～指定先の部屋の終点
        {
            pass = CreateStraightPass(self_start, other_end, range, direction);

        }
        else if (!(connect_Start || connect_End))       //直線では接続できない
        {
            Debug.Log("生成なし(方向:" + direction + "/" + self_start + "," + self_end + "/" + other_start + "," + other_end + ")");
            return false;
        }

        return true;
    }

    //直線で接続可能な場合の通路生成
    //引数：ランダムの最小値、最大値、接続先の部屋クラス、接続する方向
    private Pass CreateStraightPass(int min, int max, Range other, Direction direction)
    {
        int rand;
        if (max - min <= 1)
        {
            rand = Utility.GetRandomInt(min, max);
        }
        else
        {
            rand = Utility.GetRandomInt(min + 1, max - 1);
        }

        Pass pass = new Pass();

        //方向毎に通路を生成し返す
        //***部屋にはみ出している通路部分を削る！
        switch (direction)
        {
            case Direction.LEFT:
                pass.Start = new Position(this.m_Room.Start.X - 1, rand);
                pass.End = new Position(other.m_Room.End.X + 1, rand);
                break;
            case Direction.RIGHT:
                pass.Start = new Position(this.m_Room.End.X + 1, rand);
                pass.End = new Position(other.m_Room.Start.X - 1, rand);
                break;
            case Direction.UP:
                pass.Start = new Position(rand, this.m_Room.Start.Y - 1);
                pass.End = new Position(rand, other.m_Room.End.Y + 1);
                break;
            case Direction.DOWN:
                pass.Start = new Position(rand, this.m_Room.End.Y + 1);
                pass.End = new Position(rand, other.m_Room.Start.Y - 1);
                break;
        }

        Debug.Log("直線生成(" + pass.Start.X + "," + pass.Start.Y + "/" + pass.End.X + "," + pass.End.Y + ")");

        //生成した通路の始点を保存
        if (!this.m_PassPositionDic.ContainsKey(direction))
        {
            this.m_PassPositionDic.Add(direction, new List<Position>());
        }
        this.m_PassPositionDic[direction].Add(pass.Start);

        //指定された区画に通路の終点を保存
        //※された側なので方向を反転する
        Direction inverse = Direction.UP;
        switch (direction)
        {
            case Direction.LEFT:
                inverse = Direction.RIGHT;
                break;
            case Direction.RIGHT:
                inverse = Direction.LEFT;
                break;
            case Direction.UP:
                inverse = Direction.DOWN;
                break;
            case Direction.DOWN:
                inverse = Direction.UP;
                break;
        }
        if (!other.m_PassPositionDic.ContainsKey(inverse))
        {
            other.m_PassPositionDic.Add(inverse, new List<Position>());
        }
        other.m_PassPositionDic[inverse].Add(pass.End);

        return pass;
    }

    //=======================================================
    //直角通路のチェック
    //引数：（繋げたい区画の）Rangeクラス、出力用通路クラス
    //=======================================================
    public void IsCurve(Range _range, out Pass _pass, out Pass _viaPass1, out Pass _viaPass2)
    {
        Room other_room = _range.m_Room;

        //中間地点１，２
        Position via1 = new Position();
        Position via2 = new Position();

        //指定した区画への向きを取得(部屋１→部屋２)
        Direction direction1 = this.m_connectIdList[_range.Id];
        //中間地点１を求める
        _viaPass1 = CreateViaPoint(m_Room, other_room, this, direction1, out via1);
        //始点リストに登録
        if (!this.m_PassPositionDic.ContainsKey(direction1))
        {
            this.m_PassPositionDic.Add(direction1, new List<Position>());
        }
        this.m_PassPositionDic[direction1].Add(_viaPass1.Start);

        //指定された区画からの向きを取得(部屋２→部屋１)
        Direction direction2 = _range.m_connectIdList[this.Id];
        //中間地点２を求める
        _viaPass2 = CreateViaPoint(other_room, m_Room, _range, direction2, out via2);
        //始点リストに登録
        if (!_range.m_PassPositionDic.ContainsKey(direction2))
        {
            _range.m_PassPositionDic.Add(direction2, new List<Position>());
        }
        _range.m_PassPositionDic[direction2].Add(_viaPass2.Start);

        //中間地点をつなぐ通路を生成
        _pass = ConnectViaPosition(via1, via2);
        
    }

    //中間地点を生成
    //***生成位置がおかしい、生成してない？部分もある
    private Pass CreateViaPoint(Room _room1,Room _room2,Range _range,Direction _dir,out Position _via)
    {
        _via = new Position();

        //部屋のずれた方向を取得
        //左右に隣接→上下のどちらにあるか？
        if (_dir == Direction.LEFT || _dir == Direction.RIGHT)
        {
            int dist_Y = _room2.End.Y - _room1.Start.Y;
            if (dist_Y < 0)
            {
                //上にずれている
                //カウントが1以上の時、最もYが"小さい"データを取得
                int y = _room1.End.Y;
                if (_range.m_PassPositionDic.ContainsKey(_dir))
                {
                    //通路の始点をループで回す
                    foreach (Position passPos in _range.m_PassPositionDic[_dir])
                    {
                        //より小さいY座標を保存
                        y = passPos.Y < y ? passPos.Y : y;
                    }
                }
                //中間地点１にセット
                _via.Y = Utility.GetRandomInt(_room1.Start.Y, y);
                //二つの通路がくっつくのを防ぐため
                if (_via.Y == y - 1) _via.Y += 1;
            }
            else if(dist_Y > 0)
            {
                //下にずれている
                //カウントが1以上の時、最もYが"大きい"データを取得
                int y = _room1.Start.Y;
                if (m_PassPositionDic.ContainsKey(_dir))
                {
                    //通路の始点をループで回す
                    foreach (Position passPos in _range.m_PassPositionDic[_dir])
                    {
                        //より大きいY座標を保存
                        y = passPos.Y > y ? passPos.Y : y;
                    }
                }
                //中間地点１にセット
                _via.Y = Utility.GetRandomInt(y, _room1.End.Y);
                if (_via.Y == y + 1) _via.Y -= 1;
            }
        }
        //上下に隣接→左右のどちらにあるか？
        else if (_dir == Direction.UP || _dir == Direction.DOWN)
        {
            int dist_X = _room2.End.X - _room1.Start.X;
            if (dist_X < 0)
            {
                //左
                //カウントが1以上の時、最もYが"小さい"データを取得
                int x = _room1.End.X;
                if (_range.m_PassPositionDic.ContainsKey(_dir))
                {
                    //通路の始点をループで回す
                    foreach (Position passPos in _range.m_PassPositionDic[_dir])
                    {
                        //より小さいY座標を保存
                        x = passPos.X < x ? passPos.X : x;
                    }
                }
                //中間地点１にセット
                _via.X = Utility.GetRandomInt(_room1.Start.X, x);
                if (_via.X == x - 1) _via.X += 1;
            }
            else if(dist_X > 0)
            {
                //右
                //カウントが1以上の時、最もYが"大きい"データを取得
                int x = _room1.Start.X;
                if (_range.m_PassPositionDic.ContainsKey(_dir))
                {
                    //通路の始点をループで回す
                    foreach (Position passPos in _range.m_PassPositionDic[_dir])
                    {
                        //より小さいY座標を保存
                        x = passPos.X > x ? passPos.X : x;
                    }
                }
                //中間地点１にセット
                _via.X = Utility.GetRandomInt(x, _room1.End.X);
                if (_via.X == x + 1) _via.X -= 1;
            }
        }

        //
        Pass viaPass =new Pass();

        //隣接している方向によって軸の座標、通路を設定
        switch (_dir)
        {
            //左
            case Direction.LEFT:
                _via.X = _range.Start.X;
                viaPass = new Pass(_room1.Start.X - 1, _via.Y, _via.X, _via.Y);
                break;
            //右
            case Direction.RIGHT:
                _via.X = _range.End.X;
                viaPass = new Pass(_room1.End.X + 1, _via.Y, _via.X, _via.Y);
                break;
            //上
            case Direction.UP:
                _via.Y = _range.Start.Y;
                viaPass = new Pass(_via.X, _room1.Start.Y - 1, _via.X, _via.Y);
                break;
            //下
            case Direction.DOWN:
                _via.Y = _range.End.Y;
                viaPass = new Pass(_via.X, _room1.End.Y + 1, _via.X, _via.Y);
                break;
        }

        //中間地点までの通路を出力
        return viaPass;
    }

    //中継通路を生成
    private Pass ConnectViaPosition(Position via1,Position via2)
    {
        //中間地点１，２をつなぐ通路を生成
        Pass pass = new Pass(via1.X, via1.Y, via2.X, via2.Y);

        return pass;
    }



    //コンストラクタ
    //引数：始点座標（左上）、終点座標（右下）、ID
    public Range(Position start, Position end, int id)
    {
        //始点・終点
        Start = start;
        End = end;
        //ID
        Id = id;

    }



    public Range(int startX, int startY, int endX, int endY, int id) : this(new Position(startX, startY), new Position(endX, endY), id) { }

    public Range() : this(0, 0, 0, 0, 0) { }

    public override string ToString()
    {
        return string.Format("{0}=>{1}", Start, End);
    }
}
