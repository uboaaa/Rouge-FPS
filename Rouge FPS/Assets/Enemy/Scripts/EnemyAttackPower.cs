using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackPower : MonoBehaviour
{
    // Start is called before the first frame update
    private EnemyParameter ep = null;
    public int enemyatkpow = 0;
    public void SetAtkPower(int i){ enemyatkpow = i;}
    public bool BulletFlg = false;
    
    private GameObject FPSCon;
    private GameObject Flush;
    public static bool DamageFlg;
    void Start()
    {
        DamageFlg = false;
        FPSCon=GameObject.Find("FPSController");
        Flush=GameObject.Find("DamageRed");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(ep.atk);
    }
    void OnCollisionEnter(Collision collision)
    {

        // if(collision.gameObject.tag == "Player")
        // {
            
        //     MyStatus.downHp((float)enemyatkpow);
        //     DamageFlg = true;
            
        //     Destroy(this);
        // }

        // 弾じゃなかったらそのまま攻撃力を取得
        if(BulletFlg == false)
        {
            ep = this.gameObject.GetComponent<EnemyParameter>();
            
            // パラメータから攻撃力を取得
            enemyatkpow = ep.atk;
        }

        switch (collision.gameObject.tag)
        {


            case "Player":
                if(Flush.GetComponent<FlushController>().GetRed()<0.1f)
                FPSCon.GetComponent<MyStatus>().downHp((float)enemyatkpow);
                DamageFlg = true;

                break;


            default:
                DamageFlg = false;

                break;
        }

    }

    void OnTriggerEnter(Collider other)
    {


            
        // hitPos = other.ClosestPointOnBounds(this.transform.position);
        

        // ヒット時接触した点における法線を取得
        //Vector3 normalVector = collision.contacts[0].normal;

        // 法線方向に回転させる
        //rotation = Quaternion.LookRotation(normalVector);

        switch (other.gameObject.tag)
        {
            // "Back"タグに当たった場合、エフェクトを出して弾を削除する
            case "Back":
                //HitEffect(HitEffectPrefab);
                if(this.gameObject.tag == "EnemyBullet")
                {
                    Destroy(this.gameObject);
                }
                break;

            // "Player"タグに当たった場合、エフェクトを出して弾を削除する
            case "Player":
                //HitEffect(HitEffectPrefab);
                FPSCon.GetComponent<MyStatus>().downHp((float)enemyatkpow);
                DamageFlg = true;
                if(this.gameObject.tag == "EnemyBullet")
                {
                    Destroy(this.gameObject);
                }
                
                break;

            
            // タグをつけてないものに当たった場合、物理マテリアルに応じる
            case "Untagged":
                //HitEffect(HitEffectPrefab);
                if(this.gameObject.tag == "EnemyBullet")
                {
                    Destroy(this.gameObject);   // 今のところ弾は適当に消す
                }
                break;

            default:
                break;
        }

    }
   public static bool GetEnemyBHitGet(){return DamageFlg;}
}
