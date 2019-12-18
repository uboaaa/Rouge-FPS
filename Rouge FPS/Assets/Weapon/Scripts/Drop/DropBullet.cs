using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBullet : MonoBehaviour
{
    [SerializeField] int bulletNum;     // 拾った時の弾の数
    private bool hitFlg;                // 当たった判定用

    ChangeEquip CEScript;               // [ChangeEquip]用の変数
    GameObject Weapon;                  // 今持っている武器
    float maxDis = 30.0f;
    Rigidbody rd;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit,maxDis))
        {
            if(hit.collider.tag == "Back")
            {
                maxDis -= 0.7f;
            }
        }

        if(maxDis <= 0.0f)
        {
            rd = this.GetComponent<Rigidbody>();
            rd.useGravity = false;
        }

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
            if(!Weapon)
            {
                return;
            }

            if(Weapon.GetComponent<GunController>().Ammo < Weapon.GetComponent<GunController>().MagazineSize)
            {
                hitFlg = true;
            }
        }
    }
}
