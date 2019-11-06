//===========================================================================
// 弾用スクリプト
//===========================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour
{
    [SerializeField] GameObject hitEffectPrefab;
    [SerializeField] Vector3 hitEffectScale = new Vector3(1.0f,1.0f,1.0f);
    GameObject hitEffect;
    Vector3 hitPos;
    Quaternion rotation;
    ChangeEquip CEScript;
    GunController GCScript;
    int Damage;


    void Start()
    {
        GameObject FPSCon = GameObject.Find("FPSController");
        CEScript = FPSCon.GetComponent<ChangeEquip>();

        if(CEScript.ownGun == 1)
        {
            GameObject anotherObject = GameObject.Find(CEScript.Weapon1.name);
            GCScript = anotherObject.GetComponent<GunController>();
        }
        else if(CEScript.ownGun == 2)
        {
            GameObject anotherObject = GameObject.Find(CEScript.Weapon2.name);
            GCScript = anotherObject.GetComponent<GunController>();
        }
        
        Damage = GCScript.damage;
    }

    void Update()
    {
        Debug.Log(Damage);
    }

    void HitEffect()
    {
        // エフェクトを生成
        hitEffect = Instantiate<GameObject>(hitEffectPrefab,new Vector3(hitPos.x,hitPos.y,hitPos.z),rotation);
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
                HitEffect();
                Destroy(gameObject);
                break;

            // "Enemy"タグに当たった場合、エフェクトを出して弾を削除する
            case "Enemy":
                HitEffect();
                Destroy(gameObject);
                break;
                
            default:
                break;
        }
    }
}
