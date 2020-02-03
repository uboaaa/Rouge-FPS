using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{
    // プレイヤー
    private GameObject player = null;
    // オーラエフェクト
    private GameObject AuraEffect = null;
    private GameObject ae = null;


    // オーディオ
    AudioSource audioSource = null;
    private int SeCnt = 0;

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
    public GameObject deadeffect = null;

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


    // 角度計算用
    private float rot = 0;

    // 発見フラグ
    bool foundflg = false;

    // アニメ関数
    int trans = 0;

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
        // オーディオ
        audioSource = this.gameObject.GetComponent<AudioSource>();
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
            ep.hp = 200;
            ep.atk = 10;
            ep.def = 0;
            ep.speed = 0.4f;
            ep.startrot = 60;

            // ポイントライト
            plight.color = new Color(0.5f, 0.5f, 1.0f, 1.0f);


        }
        else if (AILevel == 2)
        {
            material.SetFloat(propID_h, 0.45f);
            material.SetFloat(propID_s, 1.0f);
            material.SetFloat(propID_c, 0.7f);

            ep.hp = 800;
            ep.atk = 15;
            ep.def = 10;
            ep.speed = 0.4f;
            ep.startrot = 60;

            plight.color = new Color(1.0f, 0.5f, 0.5f, 1.0f);

            // オーラエフェクト
            AuraEffect = Resources.Load("AuraEffectYellow") as GameObject;
            ae = Instantiate(AuraEffect) as GameObject;
            ae.transform.position = this.gameObject.transform.position;
        }
        else if (AILevel == 3)
        {
            material.SetFloat(propID_h, 0.3f);
            material.SetFloat(propID_s, 0.4f);
            material.SetFloat(propID_c, 1.0f);

            ep.hp = 1200;
            ep.atk = 20;
            ep.def = 20;
            ep.speed = 0.4f;
            ep.startrot = 60;

            plight.color = new Color(1.0f, 0.5f, 1.0f, 1.0f);

            AuraEffect = Resources.Load("AuraEffectRed") as GameObject;
            ae = Instantiate(AuraEffect) as GameObject;
            ae.transform.position = this.gameObject.transform.position;
        }



    }


    void Update()
    {

        if (!PauseScript.pause())
        {
            if (!SkillManagement.GetTimeStop())
            {
                if (!SettingScript.Settingpause())
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


                        
                        if(SeCnt == 0)
                        {
                            // SE再生
                            audioSource.PlayOneShot(audioSource.clip);
                        }
                        SeCnt++;
                        if(SeCnt > 95) { SeCnt = 0;}


                        if (ae)
                        {
                            ae.transform.position = new Vector3(
                                this.gameObject.transform.position.x,
                                this.gameObject.transform.position.y - 2.0f,
                                this.gameObject.transform.position.z
                            );
                        }
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
                    if (ep.hp == 0)
                    {
                        // 死亡時の爆発エフェクトを再生
                        GameObject de = Instantiate(deadeffect) as GameObject;
                        de.transform.position = this.gameObject.transform.position;
                        de.transform.position = new Vector3(de.transform.position.x, de.transform.position.y - 1.5f, de.transform.position.z);

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

                    // // Xを押したら
                    // if (Input.GetKey(KeyCode.X))
                    // {
                    //     GameObject go = Instantiate(this.gameObject) as GameObject;
                    //     go.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 5);
                    // }
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
                if (!SettingScript.Settingpause())
                {
                    if (collision.gameObject.tag == "Bullet")
                    {
                        // 弾のダメージを取得
                        dn.SetBulletDamage(collision.gameObject.GetComponent<BulletController>().Damage);


                        eh.SetHitFlg(true);
                        dn.SetHitFlg(true);
                        foundflg = true;

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
                if (!SettingScript.Settingpause())
                {
                    if (collider.gameObject.tag == "Bullet")
                    {
                        // 弾のダメージを取得
                        dn.SetBulletDamage(collider.gameObject.GetComponent<BulletController>().Damage);


                        eh.SetHitFlg(true);
                        dn.SetHitFlg(true);
                        foundflg = true;
                        
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
