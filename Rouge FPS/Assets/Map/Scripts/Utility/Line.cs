using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;           //Mathクラスを省略

public class Line
{
    public Position m_line_Start { get; set; }
    public Position m_line_End { get; set; }

    //判定用
    //*両端を除いた線分を返す
    public Line toCheck()
    {
        Position st, ed;
        if (m_line_Start.X == m_line_End.X)
        {
            st = new Position(m_line_Start.X, m_line_Start.Y + 1);
            ed = new Position(m_line_End.X, m_line_End.Y - 1);
        }
        else
        {
            st = new Position(m_line_Start.X + 1, m_line_Start.Y);
            ed = new Position(m_line_End.X - 1, m_line_End.Y);
            
        }

        return new Line(st, ed);
    }

    //線分の重なり判定
    //交差時、trueを返す
    //引数：判定したい線分
    public bool IsClossing(Line other)
    {

        //判定用の線分を取得
        Line ch_this_line = this.toCheck();
        Line ch_other_line = other.toCheck();
        

        //同直線上に線分があるか
        if (ch_this_line.m_line_Start.X != ch_other_line.m_line_Start.X &&
            ch_this_line.m_line_Start.Y != ch_other_line.m_line_Start.Y)
        {
            //なかった場合
            return false;
        }

        //自身の線分に引数の線分が含まれているか
        if(!ch_this_line.IncludePoint(ch_other_line.m_line_Start) && 
           !ch_this_line.IncludePoint(ch_other_line.m_line_End))
        {
            //引数の線分に自身の線分が含まれいてるか
            if (!ch_other_line.IncludePoint(ch_this_line.m_line_Start) && 
                !ch_other_line.IncludePoint(ch_this_line.m_line_End))
            {
                //どちらにも含まれないので接触してない
                return false;
            }
        }

        return true;
    }

    //点P(point)が線分に含まれるかを判定
    //引数：判定したい点
    public bool IncludePoint(Position point)
    {
        //線分の長さを求める
        double len_line = Sqrt(Pow(m_line_End.X - m_line_Start.X, 2) + Pow(m_line_End.Y - m_line_Start.Y, 2));
        //線分の始点から点P(point)までの長さを求める
        double len_point = Sqrt(Pow(point.X - m_line_Start.X, 2) + Pow(point.Y - m_line_Start.Y, 2));

        //ベクトルA(始点→終点)とベクトルB(始点→点P)の内積を求める
        double Dot = (m_line_End.X - m_line_Start.X) * (point.X - m_line_Start.X) + (m_line_End.Y - m_line_Start.Y) * (point.Y - m_line_Start.Y);

        //
        if(Dot!=len_line*len_point || len_line < len_point)
        {
            return false;
        }

        return true;
    }

    public Line(Position start,Position end)
    {
        m_line_Start = start;
        m_line_End = end;
    }
}
