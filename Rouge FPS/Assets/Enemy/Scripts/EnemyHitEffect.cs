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
    public bool hitflg = false;



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

        // ヒットマテリアル情報取得
        hitmaterial = Resources.Load("HitMaterial") as Material;

        // ヒットマテリアル挿入
        enemy.GetComponent<SpriteRenderer>().material = hitmaterial;

        // // コピー
        // Material hm = Instantiate(hitmaterial);
        
        // //対象のシェーダー情報を取得
        // Shader sh = enemy.GetComponent<SpriteRenderer>().material.shader;

        // //取得したシェーダーを元に新しいマテリアルを作成
        // Material mat = new Material(sh);


        // // 新しいマテリアルを挿入
        // enemy.GetComponent<SpriteRenderer>().material = hm;



        
        

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

        
        
        
        Debug.Log(brightness);
    }
}
