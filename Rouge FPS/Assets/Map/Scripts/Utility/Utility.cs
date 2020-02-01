using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
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


    //リストからランダムに値を返す
    public static T GetAtRandom<T>(this List<T> list)
    {
        if (list.Count == 0)
        {
            Debug.Log("リストが空です");
            return default;
        }

        return list[Random.Range(0, list.Count)];
    }

    //リストからランダムに値を返す（偏りあり）
    //※末尾に近いほうが返されやすい
    public static T GetAtRandom_Bias<T>(this List<T> list)
    {
        if (list.Count == 0)
        {
            Debug.Log("リストが空です");
        }

        //
        var rand = 1.0f - (Random.value * Random.value);
        int element = Mathf.RoundToInt(rand * (list.Count - 1));

        return list[element];
    }
}
