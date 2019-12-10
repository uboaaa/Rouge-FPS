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
            pass = CreateStraightPass(other_start, other_end, range.m_Room, direction);

        }
        else if (connect_Start && !connect_End)         //指定先の部屋の始点～指定元の部屋の終点
        {
            pass = CreateStraightPass(other_start, self_end , range.m_Room, direction);

        }
        else if (!connect_Start && connect_End)         //指定元の部屋の始点～指定先の部屋の終点
        {
            pass = CreateStraightPass(self_start, other_end, range.m_Room, direction);

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
    private Pass CreateStraightPass(int min, int max, Room other_room, Direction direction)
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
                pass.Start = new Position(m_Room.Start.X - 1, rand);
                pass.End = new Position(other_room.End.X + 1, rand);
                break;
            case Direction.RIGHT:
                pass.Start = new Position(m_Room.End.X + 1, rand);
                pass.End = new Position(other_room.Start.X - 1, rand);
                break;
            case Direction.UP:
                pass.Start = new Position(rand, m_Room.Start.Y - 1);
                pass.End = new Position(rand, other_room.End.Y + 1);
                break;
            case Direction.DOWN:
                pass.Start = new Position(rand, m_Room.End.Y + 1);
                pass.End = new Position(rand, other_room.Start.Y - 1);
                break;
        }

        Debug.Log("直線生成(" + pass.Start.X + "," + pass.Start.Y + "/" + pass.End.X + "," + pass.End.Y + ")");

        return pass;
    }

    public bool IsCurve(Range range, out Pass pass)
    {
        Room other_room = range.m_Room;

        //指定した先の区画への向きを取得
        Direction direction = m_connectIdList[range.Id];

        pass = new Pass();

        pass = CreateCurvePass(other_room, direction);
        

        

        return true;
    }

    private Pass CreateCurvePass(Room other_room, Direction direction)
    {
        //中間地点を求める
        

        Pass pass = new Pass();

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
