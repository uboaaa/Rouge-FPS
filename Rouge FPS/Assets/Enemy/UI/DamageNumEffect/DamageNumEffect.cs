using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumEffect : MonoBehaviour
{

    GameObject enemy;
    EnemyHitEffect eh;
    
    public bool hitflg = false;
    public int BulletDamage = 0;

    GameObject DamageNumPrefab;
    // private struct DamageNumParam
    // {
        public bool FeedOutFlg = false; 
        public float AlphaTime;
    //}
    

    // DamageNumParam dnp;
    // List<DamageNumParam> dnpList;

    // private List<float> Alpha;  



    // Start is called before the first frame update
    void Start()
    {
        // エネミー情報取得
        enemy = this.gameObject;
        // エネミーヒットエフェクト
        eh = GetComponent<EnemyHitEffect>();

        DamageNumPrefab = Resources.Load("DamageNum") as GameObject;

        //DamageNumParam[] dnp = new DamageNumParam[10];
    }

    GameObject dn;
    // Update is called once per frame
    void Update()
    {
        if(hitflg == true)
        {
            // ダメージの値を挿入
            DamageNumPrefab.GetComponent<TextMesh>().text = "" + BulletDamage;

            // プレハブ生成
            dn = Instantiate(DamageNumPrefab) as GameObject;
            

            // 敵の座標へ移動
            float value = Random.Range(-1.0f, 1.0f);    // 乱数
            dn.gameObject.transform.position = new Vector3(enemy.gameObject.transform.position.x + value,enemy.gameObject.transform.position.y,enemy.gameObject.transform.position.z + value);

            // 向き方向の取得
            Vector3 force = new Vector3(0.0f,3.5f,0);

            // Rigidbodyに力を加えて発射
            dn.GetComponent<Rigidbody>().AddForce(force,ForceMode.Impulse);


            Destroy(dn, 0.6f); // 2秒後に削除
            //Alpha.Add(1.0f);
            // dnpList.Add();
            FeedOutFlg = true;

            AlphaTime = 1.0f;
            //TextMesh tm = dn.GetComponent<TextMesh>();
            //tm.color = new Color(1.0f,1.0f,1.0f,1.0f);

            hitflg = false;
        }

        
        if(FeedOutFlg)
        {
            if(dn)
            {
                TextMesh tm = dn.GetComponent<TextMesh>();
                tm.color = new Color(1.0f,1.0f,1.0f,AlphaTime);
                
                // if(tm.color.a > 0.0f)
                // {
                //     float AlphaTime = tm.color.a;
                //     AlphaTime -= 0.05f;
                //     tm.color = new Color(1.0f,1.0f,1.0f,tm.color.a);
                // }
                // else
                // {
                //     AlphaTime = 1.0f;
                //     FeedOutFlg = false;
                //     Destroy(dn);
                // }
            }
            AlphaTime -= 0.05f;
            
            if(AlphaTime < 0.0f)
            {
                AlphaTime = 0.0f;
                FeedOutFlg = false;
                Destroy(dn);
            }
            
        }

        
    }
}
