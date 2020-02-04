using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBullet : MonoBehaviour
{
    [SerializeField] int bulletNum;     // 拾った時の弾の数
    private bool hitFlg;                // 当たった判定用

    ChangeEquip CEScript;               // [ChangeEquip]用の変数
    GameObject Weapon;                  // 今持っている武器
    Rigidbody rd;
    FlyingObject FOScript;

    void Update()
    {
        // ポーズ中動作しないようにする
        if(!PauseScript.pause()){}
        if(!SkillManagement.GetTimeStop()){}
        
        // 当たった時の処理 && 武器を持っていた場合
        if(hitFlg && Weapon)
        {
            // サウンドを鳴らす
       AudioManager.Instance.PlaySE("machinegun-slide1");

            // 弾を追加する
            Weapon.GetComponent<GunController>().remAmmo += bulletNum;
            //Weapon.GetComponent<GunController>().Ammo += bulletNum;

            // 自分を削除
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            // 当たった時の持っている武器を取得
            CEScript = other.GetComponent<ChangeEquip>();

            // 今持っている武器を取ってくる
            Weapon = ChangeEquip.nowWeapon();
            if(!Weapon)
            {
                return;
            }

            if(Weapon.GetComponent<GunController>().remAmmo < Weapon.GetComponent<GunController>().AmmoSize + GunController.skillAmmo)
            {
                hitFlg = true;
            }
        }

        if(other.gameObject.tag == "Back")
        {
            rd = this.GetComponent<Rigidbody>();
            rd.useGravity = false;

            if(!FOScript)
            {
                FOScript = gameObject.AddComponent<FlyingObject>();
                FOScript.swingPow = 0.05f; 
                FOScript.Range = 4;
            }
        }
    }
}
