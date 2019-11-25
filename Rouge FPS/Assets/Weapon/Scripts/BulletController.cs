﻿//======================================
// 弾用スクリプト
//======================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Rigidbodyコンポーネントを必須にする
[RequireComponent(typeof(Rigidbody))]
public class BulletController : MonoBehaviour
{
    [SerializeField] GameObject backHitEffectPrefab;
    [SerializeField] GameObject enemyHitEffectPrefab;
    [SerializeField] Vector3 hitEffectScale = new Vector3(1.0f,1.0f,1.0f);
    GameObject hitEffect;
    Vector3 hitPos;
    Quaternion rotation;
    GunController GCScript;
    GunController PrimaryScript;
    GunController SecondaryScript;
    ChangeEquip CEScript;
    public int Damage;

    void Start()
    {
        GameObject FPSCon = GameObject.Find("FPSController");
        CEScript = FPSCon.GetComponent<ChangeEquip>();   

        if(CEScript.ownGun == 1)
        {
            // プライマリ武器のGunControllerを持ってくる
            GameObject anotherObject = GameObject.Find(CEScript.Weapon1.name);
            GCScript = anotherObject.GetComponent<GunController>();
        }
        else if(CEScript.ownGun == 2)
        {
            // セカンダリ武器のGunControllerを持ってくる
            GameObject anotherObject = GameObject.Find(CEScript.Weapon2.name);
            GCScript = anotherObject.GetComponent<GunController>();
        }

        // ダメージを取得
        Damage = GCScript.damage;

        Debug.Log(GCScript.damage);
    }

    void Update()
    {
        // ダメージ量を変更した場合
        //Damage = 
    }

    void HitEffect(GameObject Prefab)
    {
        // エフェクトを生成
        hitEffect = Instantiate<GameObject>(Prefab,new Vector3(hitPos.x,hitPos.y,hitPos.z),rotation);
        hitEffect.transform.localScale = hitEffectScale;

        // エフェクト削除
		Destroy(hitEffect, 5.0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        // ヒット時座標取得
        foreach (ContactPoint point in collision.contacts)
        {
            hitPos = point.point;
        }

        // ヒット時接触した点における法線を取得
        Vector3 normalVector = collision.contacts[0].normal;

        // 法線方向に回転させる
        rotation = Quaternion.LookRotation(normalVector);


        switch (collision.gameObject.tag)
        {
            // "Back"タグに当たった場合、エフェクトを出して弾を削除する
            case "Back":
                HitEffect(backHitEffectPrefab);
                Destroy(gameObject);
                break;

            // "Enemy"タグに当たった場合、エフェクトを出して弾を削除する
            case "Enemy":
                HitEffect(enemyHitEffectPrefab);
                Destroy(gameObject);
                break;
            
            // タグをつけてないものに当たった場合、物理マテリアルに応じる
            case "Untagged":
                Destroy(gameObject,5.0f);   // 今のところ弾は適当に消す
                break;

            default:
                break;
        }
    }
}