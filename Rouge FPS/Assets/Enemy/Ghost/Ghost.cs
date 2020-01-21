using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
     // 弾
    private GameObject bullet = null;

    // プレイヤー
    private GameObject player = null;
    // アニメータ
    private Animator animator = null;
    // マテリアル
    private Material material = null;
    // エネミーパラメータ
    private EnemyParameter ep = null;
    // ヒットエフェクト
    private EnemyHitEffect eh = null;
    // ダメージナンバーエフェクト
    private DamageNumEffect dn = null;
    // デッドエフェクト
    public GameObject deadeffect;

    // 角度計算用
    private float rot;

    // 発見フラグ
    bool foundflg = false;
    
    // アニメ関数
    int trans;


    // 攻撃力スクリプト
    private EnemyAttackPower eap;
    // 発射フラグ
    public bool fireflg = false;
    // 発射管理
    private int ftime = 0;


    // 移動フラグ
    private bool moveflg = false;
    private int mtime = 0;
    float mvalue;


    // 弾丸の速度
    public float bspeed = 1000;


    // AIレベル
    private int AILevel = 0;
    // シェーダパラメータ管理用
    // Hue
    private int propID_h = 0;
    // Saturation
    private int propID_s = 0;
    // Contrast
    private int propID_c = 0;

    // ライト
    private Light plight = null;

    // 死んだ時に生成されるオブジェクト
    public GameObject PopObject = null;



    

    

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
        animator = this.gameObject.GetComponent<Animator>();
        // マテリアル
        material = this.gameObject.GetComponent<SpriteRenderer>().material;
        // エネミーパラメータ
        ep = this.gameObject.GetComponent<EnemyParameter>();
        // エネミーヒットエフェクト
        eh = this.gameObject.GetComponent<EnemyHitEffect>();
        // ダメージナンバーエフェクト
        dn = this.gameObject.GetComponent<DamageNumEffect>();
        // ライト
        plight = this.gameObject.GetComponent<Light>();



        // レベル情報取得
        AILevel = ep.GetLevel();


        // レベル処理
        propID_h = Shader.PropertyToID("_Hue");
        propID_s = Shader.PropertyToID("_Saturation");
        propID_c = Shader.PropertyToID("_Contrast");

        if(AILevel == 1)
        {
            // マテリアル
            material.SetFloat(propID_h, 0.0f);
            material.SetFloat(propID_s, 0.5f);
            material.SetFloat(propID_c, 0.5f);

            // パラメータ
            ep.hp = 20;
            ep.atk = 10;
            ep.def = 0;
            ep.speed = 0;
            ep.startrot = 60;

            // ポイントライト
            plight.color = new Color(0.5f,0.5f,1.0f,1.0f);


            // 弾取得
            bullet = Resources.Load("GhostBulletRed") as GameObject;
            
        }
        else if(AILevel == 2)
        {
            
            material.SetFloat(propID_h, 0.45f);
            material.SetFloat(propID_s, 1.0f);
            material.SetFloat(propID_c, 0.7f);

            ep.hp = 30;
            ep.atk = 15;
            ep.def = 10;
            ep.speed = 0;
            ep.startrot = 60;

            plight.color = new Color(1.0f,0.5f,0.5f,1.0f);

            bullet = Resources.Load("GhostBulletBlue") as GameObject;
        }
        else if(AILevel == 3)
        {

            material.SetFloat(propID_h, 0.3f);
            material.SetFloat(propID_s, 0.4f);
            material.SetFloat(propID_c, 1.0f);

            ep.hp = 40;
            ep.atk = 25;
            ep.def = 20;
            ep.speed = 0;
            ep.startrot = 60;

            plight.color = new Color(1.0f,0.5f,1.0f,1.0f);

            bullet = Resources.Load("GhostBulletGreen") as GameObject;
        }
    }

    void Update()
    {

        //if(!PauseScript.pause()){
        //if(!SkillManagement.GetTimeStop()){

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
                fireflg = true;
                trans = 0;
                //intパラメーターの値を設定する.
                animator.SetInteger("trans", trans);
            }

            // プレイヤーとの距離が範囲内なら
            if ((transform.position - player.gameObject.transform.position).magnitude < 5)
            {
                foundflg = true;
                fireflg = true;
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
        // Debug.Log("GhostFoundFlg");
        // Debug.Log(foundflg);

        // 敵の体力が０になったら
        if(ep.hp == 0)
        {
            // 死亡時の爆発エフェクトを再生
            GameObject de = Instantiate(deadeffect) as GameObject;
            de.transform.position = this.gameObject.transform.position;
            de.transform.position = new Vector3(de.transform.position.x,de.transform.position.y,de.transform.position.z);

            // 死んだときにアイテムポップ
            if(PopObject)
            {
                GameObject po = Instantiate(PopObject) as GameObject;
                po.transform.position = this.gameObject.transform.position;
            }

            // 解放処理
            Destroy(de,2.0f);
            Destroy(this.gameObject);
        }




        

        // // 弾発射
        // // z キーが押された時
        // if (Input.GetKeyDown(KeyCode.Z))
        // {
        //     if(fireflg == true)
        //     {
        //         fireflg = false;
        //     }
        //     else
        //     {
        //         fireflg = true;
        //         ftime = 0;
        //     }
        // }

        
        // 敵の体力が０になったら
        if(ep.hp == 0)
        {
            // 死亡時の爆発エフェクトを再生
            GameObject de = Instantiate(deadeffect) as GameObject;
            de.transform.position = this.gameObject.transform.position;
            de.transform.position = new Vector3(de.transform.position.x,de.transform.position.y - 1.2f,de.transform.position.z);

            // 死んだときにアイテムポップ
            if(PopObject)
            {
                GameObject po = Instantiate(PopObject) as GameObject;
                po.transform.position = this.gameObject.transform.position;
            }

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

        // 発射フラグがONなら
        if(fireflg == true)
        {
            if (ftime == 0)
            {
                // 弾丸の複製
                GameObject bullets = Instantiate(bullet) as GameObject;


                // 攻撃力の挿入
                eap = bullets.GetComponent<EnemyAttackPower>();
                eap.SetAtkPower(ep.atk);

                // 向き方向の取得
                Vector3 aim = player.gameObject.transform.position - this.transform.position;

                // Rigidbodyに力を加えて発射
                bullets.GetComponent<Rigidbody>().AddForce(aim, ForceMode.Impulse);

                // 弾丸の位置を調整
                bullets.transform.position = this.gameObject.transform.position;

                // 三秒後に削除
                Destroy(bullets, 3.0f);
            }

            ftime++;
            if(AILevel == 1)
            {
                if(ftime > 80){ ftime = 0;}
            }
            else if(AILevel == 2)
            {
                if(ftime > 70){ ftime = 0;}
            }
            else if(AILevel == 3)
            {
                if(ftime > 60){ ftime = 0;}
            }
        }


//}
    }


    // 弾との当たり判定
    private void OnCollisionEnter(Collision collision)
    {

        //if(!PauseScript.pause()){
        //if(!SkillManagement.GetTimeStop()){


        if (collision.gameObject.tag == "Bullet")
        {
            // 弾のダメージを取得
            dn.SetBulletDamage(collision.gameObject.GetComponent<BulletController>().Damage);
            
            eh.SetHitFlg(true);
            dn.SetHitFlg(true);
            foundflg = true;
            fireflg = true;

            if(moveflg == false) { moveflg = true; }
            ep.hp -= collision.gameObject.GetComponent<BulletController>().Damage;
            if(ep.hp < 0){ep.hp = 0;}
             //intパラメーターの値を設定する.
            animator.SetInteger("trans", trans);
        }

        //}
        //}

    }
}
