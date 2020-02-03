//======================================
// 弾用スクリプト
//======================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // インスペクター関係============================================
    // ヒット時のエフェクト用Prefab
    [SerializeField] GameObject backHitEffectPrefab;
    [SerializeField] GameObject enemyHitEffectPrefab;

    // ヒットエフェクトのサイズ変更用
    [SerializeField] Vector3 hitEffectScale = new Vector3(1.0f,1.0f,1.0f);
    

    // パラメーター関係==============================================
    Vector3 hitPos;                 // ヒット場所の座標
    Quaternion rotation;            // ヒット場所の角度

    public int Damage{get;private set;}     // 銃にあるダメージを受け取る用

    // スクリプト関係================================================
    GunController   GCScript;       // [GunController]用の変数
    ChangeEquip     CEScript;       // [ChangeEquip]用の変数
    GameObject anotherObject;

    void Start()
    {
        GameObject FPSCon = GameObject.Find("FPSController");
        CEScript = FPSCon.GetComponent<ChangeEquip>();   

        if(ChangeEquip.ownGun == 1)
        {
            // プライマリ武器のGunControllerを持ってくる
            anotherObject = GameObject.Find(ChangeEquip.PrimaryWeapon.name);
            GCScript = anotherObject.GetComponent<GunController>();
            // ダメージを取得
            Damage = GCScript.Damage;
            Damage += GunController.skillDamage;

        } else if(ChangeEquip.ownGun == 2) {
            // セカンダリ武器のGunControllerを持ってくる
            anotherObject = GameObject.Find(ChangeEquip.SecondaryWeapon.name);
            GCScript = anotherObject.GetComponent<GunController>();
            // ダメージを取得
            Damage = GCScript.Damage;
            Damage += GunController.skillDamage;
        }

        // ダメージのばらつき +-1
        Damage += UnityEngine.Random.Range(-1, 2);
    }

    void Update()
    {
        // ポーズ中動作しないようにする
        if(!PauseScript.pause()){}
        if(!SkillManagement.GetTimeStop()){}
    }

    void HitEffect(GameObject Prefab)
    {
        if(Prefab != null)
        {
            // エフェクトを生成
            GameObject　hitEffect = Instantiate<GameObject>(Prefab,new Vector3(hitPos.x,hitPos.y,hitPos.z),rotation);
            hitEffect.transform.localScale = hitEffectScale;

            // エフェクト削除
		    Destroy(hitEffect, 5.0f);
        }
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
                if(backHitEffectPrefab)
                {
                    HitEffect(backHitEffectPrefab);
                }
                Destroy(gameObject);
                break;

            // "Enemy"タグに当たった場合、エフェクトを出して弾を削除する
            case "Enemy":
                if(enemyHitEffectPrefab)
                {
                    HitEffect(enemyHitEffectPrefab);
                }
                Destroy(gameObject);
                break;
            
            // // タグをつけてないものに当たった場合、物理マテリアルに応じる
            // case "Untagged":
            //     Destroy(gameObject);
            //     break;

            default:
                break;
        }
    }
}
