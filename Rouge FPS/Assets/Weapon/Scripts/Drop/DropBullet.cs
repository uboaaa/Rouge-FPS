using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBullet : MonoBehaviour
{
    [SerializeField] int bulletNum;     // 拾った時の弾の数
    private bool hitFlg;                // 当たった判定用

    ChangeEquip CEScript;               // [ChangeEquip]用の変数
    GameObject Weapon;                  // 今持っている武器
    public float maxDistance = 100.0f;   // 計測可能な最大距離
    public float distance;              // 計測距離
    Rigidbody rd;
    FlyingObject FOScript;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit,maxDistance))
        {
            if(hit.collider.tag == "Back")
            {
                distance = hit.distance;
            }
        }

        if(distance <= 0.8f)
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

        // 当たった時の処理 && 武器を持っていた場合
        if(hitFlg && Weapon)
        {
            // サウンドを鳴らす
            SoundManager soundManager = GameObject.Find("DropBullet").GetComponent<SoundManager>();
            soundManager.Play(0);

            // 弾を追加する
            Weapon.GetComponent<GunController>().remAmmo += bulletNum;
            //Weapon.GetComponent<GunController>().Ammo += bulletNum;

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
            Weapon = ChangeEquip.nowWeapon();
            if(!Weapon)
            {
                return;
            }

            if(Weapon.GetComponent<GunController>().remAmmo < Weapon.GetComponent<GunController>().AmmoSize)
            {
                hitFlg = true;
            }
        }
    }
}
