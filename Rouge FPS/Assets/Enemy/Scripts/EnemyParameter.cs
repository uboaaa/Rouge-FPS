using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParameter : MonoBehaviour
{

    // エネミー
    private GameObject enemy = null;

    // AIレベル
    public int AILevel = 1;
    public int GetLevel() { return AILevel;}


    [Space(20)]
    // HP
    public int hp = 0;

    // 攻撃力
    public int atk = 0;

    // 防御力
    public int def = 0;

    // 移動スピード
    public float speed = 0;

    // 初期角度
    public float startrot = 0;


}
