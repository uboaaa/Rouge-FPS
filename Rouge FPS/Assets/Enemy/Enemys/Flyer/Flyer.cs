using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer : MonoBehaviour
{

    // 弾
    public GameObject bullet = null;

    // プレイヤー
    private GameObject player = null;

    // オーラエフェクト
    private GameObject AuraEffect = null;
    private GameObject ae = null;

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
    private int rnd = 0;
    public GameObject PopObject1 = null;

    public GameObject PopObject2 = null;







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

        if (AILevel == 1)
        {
            // マテリアル
            material.SetFloat(propID_h, 0.0f);
            material.SetFloat(propID_s, 0.5f);
            material.SetFloat(propID_c, 0.5f);

            // パラメータ
            ep.hp = 50;
            ep.atk = 8;
            ep.def = 0;
            ep.speed = 0;
            ep.startrot = 60;

            // ポイントライト
            plight.color = new Color(0.5f, 0.5f, 1.0f, 1.0f);

        }
        else if (AILevel == 2)
        {

            material.SetFloat(propID_h, 0.45f);
            material.SetFloat(propID_s, 1.0f);
            material.SetFloat(propID_c, 0.7f);

            ep.hp = 200;
            ep.atk = 12;
            ep.def = 10;
            ep.speed = 0;
            ep.startrot = 60;

            plight.color = new Color(1.0f, 0.5f, 0.5f, 1.0f);

            // オーラエフェクト
            AuraEffect = Resources.Load("AuraEffectYellow") as GameObject;
            ae = Instantiate(AuraEffect) as GameObject;
            ae.transform.position = new Vector3(
                    this.gameObject.transform.position.x,
                    this.gameObject.transform.position.y - 1.0f,
                    this.gameObject.transform.position.z
                );
        }
        else if (AILevel == 3)
        {

            material.SetFloat(propID_h, 0.3f);
            material.SetFloat(propID_s, 0.4f);
            material.SetFloat(propID_c, 1.0f);

            ep.hp = 300;
            ep.atk = 16;
            ep.def = 20;
            ep.speed = 0;
            ep.startrot = 60;

            plight.color = new Color(1.0f, 0.5f, 1.0f, 1.0f);

            AuraEffect = Resources.Load("AuraEffectRed") as GameObject;
            ae = Instantiate(AuraEffect) as GameObject;
            ae.transform.position = new Vector3(
                    this.gameObject.transform.position.x,
                    this.gameObject.transform.position.y - 1.0f,
                    this.gameObject.transform.position.z
                );
        }
    }

    void Update()
    {
        //ここ追加
        if (!PauseScript.pause())
        {
            if (!SkillManagement.GetTimeStop())
            {
                if(!SettingScript.Settingpause()){



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
            }
        }


        // デバッグ表示
        //Debug.Log("FlyerHP");
        //Debug.Log(ep.hp);


        // 敵の体力が０になったら
        if (ep.hp == 0)
        {
            // 死亡時の爆発エフェクトを再生
            GameObject de = Instantiate(deadeffect) as GameObject;
            de.transform.position = this.gameObject.transform.position;
            de.transform.position = new Vector3(de.transform.position.x, de.transform.position.y, de.transform.position.z);

            // 乱数処理
            rnd = Random.Range(0, 3);

            // 死んだときにアイテムポップ
            // 0は無生成
            if (rnd == 0)
            {

            }
            // 1を生成
            else if (rnd == 1)
            {
                if (PopObject1)
                {
                    GameObject po1 = Instantiate(PopObject1) as GameObject;
                    po1.transform.position = this.gameObject.transform.position;
                }
            }
            // 2を生成
            else if (rnd == 2)
            {
                if (PopObject2)
                {
                    GameObject po2 = Instantiate(PopObject2) as GameObject;
                    po2.transform.position = this.gameObject.transform.position;
                }
            }
            // 解放処理
            Destroy(ae);
            Destroy(de, 2.0f);
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


        // 発射フラグがONなら
        if (fireflg == true)
        {
            if (ftime == 0)
            {
                // 弾丸の複製
                GameObject bullets = Instantiate(bullet) as GameObject;

                // マテリアル
                Material b_material = bullets.GetComponent<SpriteRenderer>().material;

                if (AILevel == 1)
                {
                    b_material.SetFloat(propID_h, 0.0f);
                    b_material.SetFloat(propID_s, 0.5f);
                    b_material.SetFloat(propID_c, 0.5f);
                }
                else if (AILevel == 2)
                {
                    b_material.SetFloat(propID_h, 0.45f);
                    b_material.SetFloat(propID_s, 1.0f);
                    b_material.SetFloat(propID_c, 0.7f);
                }
                else if (AILevel == 3)
                {
                    b_material.SetFloat(propID_h, 0.3f);
                    b_material.SetFloat(propID_s, 0.5f);
                    b_material.SetFloat(propID_c, 1.0f);
                }

                // 攻撃力の挿入
                eap = bullets.GetComponent<EnemyAttackPower>();
                eap.SetAtkPower(ep.atk);

                // 向き方向の取得
                Vector3 aim = player.gameObject.transform.position - this.transform.position;

                aim = aim.normalized * 25.0f;

                // Rigidbodyに力を加えて発射
                bullets.GetComponent<Rigidbody>().AddForce(aim, ForceMode.Impulse);

                // 弾丸の位置を調整
                bullets.transform.position = this.gameObject.transform.position;

                // 三秒後に削除
                Destroy(bullets, 3.0f);
            }

            ftime++;
            if (AILevel == 1)
            {
                if (ftime > 60) { ftime = 0; }
            }
            else if (AILevel == 2)
            {
                if (ftime > 50) { ftime = 0; }
            }
            else if (AILevel == 3)
            {
                if (ftime > 40) { ftime = 0; }
            }

        }


        if (moveflg == true)
        {
            if (mtime == 0)
            {
                // 乱数
                mvalue = Random.Range(-0.1f, 0.1f);
            }

            this.gameObject.transform.position = new Vector3(
                this.gameObject.transform.position.x + mvalue,
                this.gameObject.transform.position.y + mvalue,
                this.gameObject.transform.position.z + mvalue
            );

            if (ae)
            {
                ae.transform.position = new Vector3(
                    this.gameObject.transform.position.x,
                    this.gameObject.transform.position.y - 1.0f,
                    this.gameObject.transform.position.z
                );
            }

            mtime++;
            if (mtime > 15)
            {
                mtime = 0;
                moveflg = false;
            }

        }
        }

    }


    // 弾との当たり判定
    private void OnCollisionEnter(Collision collision)
    {

        if (!PauseScript.pause())
        {
            if (!SkillManagement.GetTimeStop())
            {
                if(!SettingScript.Settingpause()){

                if (collision.gameObject.tag == "Bullet")
                {
                    // 弾のダメージを取得
                    dn.SetBulletDamage(collision.gameObject.GetComponent<BulletController>().Damage);

                    eh.SetHitFlg(true);
                    dn.SetHitFlg(true);
                    foundflg = true;
                    fireflg = true;

                    if (moveflg == false) { moveflg = true; }
                    ep.hp -= collision.gameObject.GetComponent<BulletController>().Damage;
                    if (ep.hp < 0) { ep.hp = 0; }
                    //intパラメーターの値を設定する.
                    animator.SetInteger("trans", trans);
                }
                }

            }
        }

    }

    private void OnTriggerEnter(Collider collider)
    {

        if (!PauseScript.pause())
        {
            if (!SkillManagement.GetTimeStop())
            {
                if(!SettingScript.Settingpause()){
                if (collider.gameObject.tag == "Bullet")
                {
                    // 弾のダメージを取得
                    dn.SetBulletDamage(collider.gameObject.GetComponent<BulletController>().Damage);

                    eh.SetHitFlg(true);
                    dn.SetHitFlg(true);
                    foundflg = true;
                    fireflg = true;

                    if (moveflg == false) { moveflg = true; }
                    ep.hp -= collider.gameObject.GetComponent<BulletController>().Damage;
                    if (ep.hp < 0) { ep.hp = 0; }
                    //intパラメーターの値を設定する.
                    animator.SetInteger("trans", trans);
                }

            }
            }
        }
    }
}

