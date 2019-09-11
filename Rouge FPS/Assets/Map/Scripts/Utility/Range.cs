using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range
{
    //private int Left { get; set; }
    //private int Right { get; set; }
    //private int Up { get; set; }
    //private int Down { get; set; }

    public Position Start { get; set; } //始点
    public Position End { get; set; }   //終点
    public int Id { get; set; }         //区画のid

    public List<int> m_connectIdList = new List<int>();    //接している区画リスト

    //ヨコ幅取得
    public int GetWidthX()
    {
        return End.X - Start.X + 1;
    }

    //タテ幅取得
    public int GetWidthY()
    {
        return End.Y - Start.Y + 1;
    }

    public Range(Position start,Position end)
    {
        Start = start;
        End = end;
    }

    public Range(int startX,int startY,int endX,int endY):this(new Position(startX,startY),new Position(endX, endY)) { }

    public Range() : this(0, 0, 0, 0) { }

    public override string ToString()
    {
        return string.Format("{0}=>{1}", Start, End);
    }
}
