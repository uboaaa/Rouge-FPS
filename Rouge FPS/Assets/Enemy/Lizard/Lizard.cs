using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MonoBehaviour
{
    // プレイヤー
    private GameObject player = null;
    // アニメータ
    private Animator animator = null;
    // エネミーパラメータ
    private EnemyParameter ep = null;
    // ヒットエフェクト
    private EnemyHitEffect eh = null;
    // ダメージナンバーエフェクト
    private DamageNumEffect dn = null;
    // デッドエフェクト
    public GameObject deadeffect = null;

    // 角度計算用
    private float rot = 0;

    // 発見フラグ
    bool foundflg = false;
    
    // アニメ関数
    int trans = 0;

    

    




    // p2からp1への角度を求める
    // @param p1 自分の座標
    // @param p2 相手の座標
    // @return 2点の角度(Degree)
    public float GetAim(Vector2 p1, Vector2 p2)
    {
        float dx = p2.x - p1.x;
        float dy = p2.y - p1.y;
        float rad = Mathf.Atan2(dy, dx);
        return rad * Mathf.Rad2Deg;
    }



    void Start()
    {
        // プレイヤー情報取得
        player = GameObject.Find("FPSController");


        //GetComponentを用いてコンポーネントを取り出す.
        // アニメータ
        animator = GetComponent<Animator>();
        // エネミーパラメータ
        ep = GetComponent<EnemyParameter>();
        // エネミーヒットエフェクト
        eh = GetComponent<EnemyHitEffect>();
        // ダメージナンバーエフェクト
        dn = GetComponent<DamageNumEffect>();


        
        
    }


    void Update()
    {


        


        // ========
        // アニメーション 
        // ========
        //あらかじめ設定していたintパラメーター「trans」の値を取り出す.
        trans = animator.GetInteger("trans");


        // 発見フラグ条件判定
        if (foundflg == false)
        {
            // 敵が正面を向いていて知覚できる範囲内なら
            if ((transform.position - player.gameObject.transform.position).magnitude < 15 && trans == 0)
            {
                foundflg = true;

                trans = 0;
                //intパラメーターの値を設定する.
                animator.SetInteger("trans", trans);
            }

            // プレイヤーとの距離が範囲内なら
            if ((transform.position - player.gameObject.transform.position).magnitude < 5)
            {
                foundflg = true;

                trans = 0;
                //intパラメーターの値を設定する.
                animator.SetInteger("trans", trans);
            }

            // プレイヤーから攻撃を受けたら


        }

        // 発見フラグがONなら
        if (foundflg == true)
        {
            // 正面を向き
            trans = 0;
            animator.SetInteger("trans", trans);
            
            //targetに向かって進む
            transform.position += transform.forward * ep.speed * 0.1f;
        }
        // 発見フラグがOFFなら
        else if (foundflg == false)
        {
            // 角度計算
            rot = GetAim(new Vector2(transform.position.x, transform.position.z),
                new Vector2(player.gameObject.transform.position.x, player.gameObject.transform.position.z));

            rot = rot + ep.startrot;


            // 角度計算
            // 正面
            if (rot >= -45.0f && rot <= 45.0f)
            {
                trans = 0;

            }
            // 右面
            else if (rot >= 45.0f && rot <= 135.0f)
            {
                trans = 1;

            }
            // 左面
            else if (rot >= -135 && rot <= -45)
            {
                trans = 3;

            }
            // 後面
            else
            {
                trans = 2;

            }

            //intパラメーターの値を設定する
            animator.SetInteger("trans", trans);
        }



        // デバッグ表示
        //Debug.Log("LizardHP");
        //Debug.Log(ep.hp);

        // 敵の体力が０になったら
        if(ep.hp == 0)
        {
            // 死亡時の爆発エフェクトを再生
            GameObject de = Instantiate(deadeffect) as GameObject;
            de.transform.position = this.gameObject.transform.position;
            de.transform.position = new Vector3(de.transform.position.x,de.transform.position.y - 1.2f,de.transform.position.z);
            // 解放処理
            Destroy(de,2.0f);
            Destroy(this.gameObject);
        }

        // // Xを押したら
        // if (Input.GetKey(KeyCode.X))
        // {
        //     GameObject go = Instantiate(this.gameObject) as GameObject;
        //     go.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 5);
        // }


    }

    // 弾との当たり判定
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Bullet")
        {
            // 弾のダメージを取得
            dn.SetBulletDamage(collision.gameObject.GetComponent<BulletController>().Damage);

            Debug.Log(collision.gameObject.GetComponent<BulletController>().Damage);

            eh.SetHitFlg(true);
            dn.SetHitFlg(true);
            foundflg = true;
            ep.hp -= collision.gameObject.GetComponent<BulletController>().Damage;
            if(ep.hp < 0){ep.hp = 0;}
             //intパラメーターの値を設定する.
            animator.SetInteger("trans", trans);
        }

    }
}


