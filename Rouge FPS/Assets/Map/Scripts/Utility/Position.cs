using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position(int x,int y)
    {
        X = x;
        Y = y;
    }

    public Position() : this(0, 0) { }
    public Position(Position p) : this(p.X, p.Y) { }

    public override string ToString()
    {
        return string.Format("{0},{1}", X, Y);
    }

    //演算子のオーバーロード

    //単項-演算子
    public static Position operator -(Position p)
    {
        return new Position(-p.X, -p.Y);
    }

    //二項+演算子
    public static Position operator +(Position p1, Position p2)
    {
        return new Position(p1.X + p2.X, p1.Y + p2.Y);
    }

    //二項-演算子
    public static Position operator -(Position p1, Position p2)
    {
        return new Position(p1.X - p2.X, p1.Y - p2.Y);
    }
}
