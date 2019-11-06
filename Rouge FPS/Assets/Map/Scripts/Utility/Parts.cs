using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    //変数名わかりにくいので変更
    public Position Start { get; set; } //始点
    public Position End { get; set; }   //終点

    //部屋データ取得関連
    //***部屋パターン制作後、ランダム取得し設定
    //***スタート部屋、ゴール部屋を先に設定するようにする

    //コンストラクタ(始点、終点)
    Room(Position start, Position end)
    {
        Start = start;
        End = end;
    }
    public Room(int startX, int startY, int endX, int endY) : this(new Position(startX, startY), new Position(endX, endY)) { }

    //コンストラクタ(座標のみ)
    public Room(int x,int y)
    {
        //仮設定
        Start = new Position(x, y);
        End = new Position(x + 3, y + 3);
    }
}

public class Pass
{
    public Position Start { get; set; } //始点
    public Position End { get; set; }   //終点

    public int[] m_idComb;

    public bool m_passEnable = true;

    Pass(Position start, Position end)
    {
        Start = start;
        End = end;
    }
    public Pass(int startX, int startY, int endX, int endY) : this(new Position(startX, startY), new Position(endX, endY)) { }
    public Pass():this(new Position(0,0),new Position(0, 0)) { }
}
