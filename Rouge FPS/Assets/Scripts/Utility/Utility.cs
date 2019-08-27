using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{
    //指定範囲内からランダムで整数値を返す
    public static int GetRandomInt(int min,int max)
    {
        return min + Mathf.FloorToInt(Random.value * (max - min + 1));
    }

    //引数に応じて確率判定し、成否を返す
    public static bool RandomJudge(float rate)
    {
        return Random.value < rate;
    }
}
