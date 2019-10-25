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

    void Start(){}

    void Update(){}

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


        // "Enemy"タグに当たった場合、エフェクトを出して弾を削除する
        if(collision.gameObject.tag == "Enemy")
        {
            HitEffect();
            Destroy(gameObject);
        }
    }
}
