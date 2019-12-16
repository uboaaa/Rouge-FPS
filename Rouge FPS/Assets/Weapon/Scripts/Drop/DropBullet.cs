using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBullet : MonoBehaviour
{
    [SerializeField] int bulletNum;     // 拾った時の弾の数
    private bool hitFlg;                // 当たった判定用

    ChangeEquip CEScript;               // [ChangeEquip]用の変数
    GameObject Weapon;                  // 今持っている武器
    void Start(){}

    void Update()
    {
        // 当たった時の処理 && 武器を持っていた場合
        if(hitFlg && Weapon)
        {
            // 弾を追加する
            Weapon.GetComponent<GunController>().Ammo += bulletNum;

            // 自分を削除
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if( other.gameObject.tag == "Player")
        {
            // 当たった時の持っている武器を取得
            CEScript = other.GetComponent<ChangeEquip>();

            // 今持っている武器を取ってくる
            Weapon = CEScript.nowWeapon();

            hitFlg = true;
        }
    }
}
