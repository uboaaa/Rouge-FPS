using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitEffect : MonoBehaviour
{

    private GameObject enemy = null;

    // エネミーパラメータ
    private EnemyParameter ep = null;
    // ヒットエフェクト関連
    // マテリアル
    private Material hitmaterial = null;
    // シェーダパラメータ管理用
    private int propID = 0;
    // 輝度パラメータ
    private float brightness = 0.5f;
    // 輝度パラメータ調整用
    private float i = 0.1f;
    // ヒットエフェクトフラグ
    private bool hitflg = false;
    public bool GetHitFlg(){ return hitflg;}
    public void SetHitFlg(bool _a)
    {
        hitflg = _a;
    }



    // ヒットエフェクト関数
    private void HitEffect()
    {
        brightness += i;
        if (brightness > 1.0f)
        {
            i *= -1;
        }
        if (brightness < 0.5f)
        {
            brightness = 0.5f;
            i = 0.1f;
            hitflg = false;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        // エネミー情報取得
        enemy = this.gameObject;


        // ヒットマテリアル挿入
        hitmaterial = enemy.GetComponent<SpriteRenderer>().material;


        // エネミーパラメータ
        ep = GetComponent<EnemyParameter>();
        // ヒットエフェクト用
        propID = Shader.PropertyToID("_Brightness");

        
    }

    // Update is called once per frame
    void Update()
    {
        // ヒットエフェクト更新
        hitmaterial.SetFloat(propID, brightness);
        // ヒットフラグがONなら
        if (hitflg == true)
        {
            // ヒットエフェクト
            HitEffect();
            

        }
        
        //Debug.Log(brightness);
    }
}
