using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squid : MonoBehaviour
{
    // プレイヤー
    private GameObject player = null;

    // オーディオ
    AudioSource audioSource = null;
    public AudioClip sound1 = null;
    public AudioClip sound2 = null;
    public AudioClip sound3 = null;

    // アニメータ
    private Animator animator = null;
    // マテリアル
    private Material material = null;
    // エネミーパラメータ
    private EnemyParameter ep = null;
    public EnemyParameter GetEp()
    {
        return ep;
    }

    // ヒットエフェクト
    private EnemyHitEffect eh = null;
    // ダメージナンバーエフェクト
    private DamageNumEffect dn = null;

    // デッドエフェクト
    public GameObject deadeffect = null;
    // 死亡フラグ
    private bool deadflg = false;
    // アルファ値
    private Color color;
    // アルファ値管理
    private float a = 0;

    // AIレベル
    private int AILevel = 0;
    // シェーダパラメータ管理用
    // Hue
    private int propID_h = 0;
    // Saturation
    private int propID_s = 0;
    // Contrast
    private int propID_c = 0;

    private int SquidHp = 0;
    public int GetHp()
    {
        return SquidHp;
    }

    // // ライト
    // private Light plight = null;

    // 攻撃力スクリプト
    private EnemyAttackPower eap;
    // 発射フラグ
    public bool fireflg = false;
    // 発射管理
    private int ftime = 0;


    // 角度計算用
    private float rot = 0;

    // 発見フラグ
    bool foundflg = false;

    // アニメ関数
    int trans = 0;



    // 乱数用
    float mvalue;
    // 爆発エフェクト管理用
    int decnt = 0;


    // HpBar関連
    public GameObject Hpbar = null;









    // 攻撃関連 =========================================================
    // 行動フラグ（０＝待機、１＝攻撃１、２＝攻撃２、３＝攻撃３）
    private int ActionFlg = 0;
    // 行動速度調整用
    private int ActionCnt = 0;
    private int ActionTime = 0;
    // 攻撃１（火炎弾）
    // 火炎弾
    public GameObject Bullet = null;
    // 発射エフェクト
    public GameObject FireEffect = null;

    // 攻撃２（雑魚敵生成）
    // 雑魚敵
    public GameObject MiniSquid = null;
    // 雑魚敵ポップスモーク
    public GameObject PopSmoke = null;

    // 攻撃３（触手攻撃）
    // 触手
    public GameObject Tentacle = null;
    // 砂ほこり
    public GameObject DustSmoke = null;
    // 触手攻撃フラグ
    private bool AttackFlg = false;
    // 攻撃調整用
    private int AttackCnt = 0;
    // プレイヤー座標保存用
    private Vector3 SavePlayerPos;




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
        // カラー
        color = this.gameObject.GetComponent<SpriteRenderer>().color;
        // エネミーパラメータ
        ep = this.gameObject.GetComponent<EnemyParameter>();
        // エネミーヒットエフェクト
        eh = this.gameObject.GetComponent<EnemyHitEffect>();
        // ダメージナンバーエフェクト
        dn = this.gameObject.GetComponent<DamageNumEffect>();
        // ライト
        //plight = this.gameObject.GetComponent<Light>();


        // レベル情報取得
        AILevel = ep.GetLevel();

        // 行動時間調整
        ActionTime = 120;


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
            ep.hp = 3000;
            ep.atk = 30;
            ep.def = 0;
            //ep.speed = 1.0f;
            ep.startrot = 60;

            // ポイントライト
            //plight.color = new Color(0.5f,0.5f,1.0f,1.0f);

            SquidHp = ep.hp;


        }
        else if (AILevel == 2)
        {
            material.SetFloat(propID_h, 0.45f);
            material.SetFloat(propID_s, 1.0f);
            material.SetFloat(propID_c, 0.7f);

            //ep.hp = 10;
            ep.atk = 20;
            ep.def = 10;
            //ep.speed = 1.3f;
            ep.startrot = 60;

            //plight.color = new Color(1.0f,0.5f,0.5f,1.0f);
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

                        if (!AttackFlg)
                        {
                            // // 体力を元に戻す
                            // ep.hp = 120;

                            // 正面を向き
                            trans = 0;
                            animator.SetInteger("trans", trans);

                            // HPbar再生
                            if (Hpbar)
                            {
                                Hpbar.SetActive(true);
                            }
                        }



                        if (AILevel == 2)
                        {

                            material.SetFloat(propID_h, 0.45f);
                            //material.SetFloat(propID_s, 1.0f);
                            //material.SetFloat(propID_c, 0.7f);

                            //ep.hp = 10;
                            ep.atk = 20;
                            ep.def = 10;

                            ActionTime = 60;

                            AILevel = 0;
                            //plight.color = new Color(1.0f,0.5f,0.5f,1.0f);
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

                    // 行動パターン =============================================

                    if (foundflg && !deadflg)
                    {
                        // 待機モーション
                        if (ActionFlg == 0)
                        {
                            ActionCnt++;
                            if (ActionCnt > 60)
                            {
                                ActionCnt = 0;
                                ActionFlg = Random.Range(0, 4);
                            }
                        }
                        // 攻撃１モーション
                        else if (ActionFlg == 1)
                        {
                            if (ActionCnt == 0)
                            {
                                fireflg = true;
                            }

                            ActionCnt++;
                            if (ActionCnt > ActionTime)
                            {
                                ActionCnt = 0;
                                // 待機モーションへ戻る
                                ActionFlg = 0;
                            }
                        }
                        // 攻撃２モーション
                        else if (ActionFlg == 2)
                        {
                            if (ActionCnt == 0)
                            {
                                audioSource.volume = 0.3f;
                                audioSource.PlayOneShot(sound3);

                                GameObject ps1 = Instantiate(PopSmoke) as GameObject;
                                ps1.transform.position = new Vector3(this.transform.localPosition.x + 2, this.transform.position.y - 3, this.transform.localPosition.z + 3);
                                Destroy(ps1, 2.0f);
                                GameObject go1 = Instantiate(MiniSquid) as GameObject;
                                go1.transform.position = new Vector3(this.transform.localPosition.x + 2, this.transform.position.y - 3, this.transform.localPosition.z + 3);

                                GameObject ps2 = Instantiate(PopSmoke) as GameObject;
                                ps2.transform.position = new Vector3(this.transform.localPosition.x + 2, this.transform.position.y - 3, this.transform.localPosition.z - 3);
                                Destroy(ps2, 2.0f);
                                GameObject go2 = Instantiate(MiniSquid) as GameObject;
                                go2.transform.position = new Vector3(this.transform.localPosition.x + 2, this.transform.position.y - 3, this.transform.localPosition.z - 3);
                            }

                            ActionCnt++;
                            if (ActionCnt > ActionTime)
                            {
                                ActionCnt = 0;
                                // 待機モーションへ戻る
                                ActionFlg = 0;
                            }

                        }
                        // 攻撃３モーション
                        else if (ActionFlg == 3)
                        {
                            if (ActionCnt == 0)
                            {
                                AttackFlg = true;
                                trans = 4;
                            }

                            ActionCnt++;
                            if (ActionCnt > 60)
                            {
                                ActionCnt = 0;
                                // 待機モーションへ戻る
                                ActionFlg = 0;
                            }

                        }

                        // 攻撃１処理
                        if (fireflg)
                        {
                            if (ftime == 0)
                            {
                                audioSource.volume = 0.3f;
                                audioSource.PlayOneShot(sound3);

                                GameObject fe = Instantiate(FireEffect) as GameObject;
                                fe.transform.position = new Vector3(
                                    this.gameObject.transform.position.x,
                                    this.gameObject.transform.position.y,
                                    this.gameObject.transform.position.z
                                );
                                Destroy(fe, 1.0f);
                            }

                            if (ftime == 60)
                            {
                                // 弾丸の複製
                                GameObject bullets = Instantiate(Bullet) as GameObject;
                                // 攻撃力の挿入
                                eap = bullets.GetComponent<EnemyAttackPower>();
                                eap.SetAtkPower(ep.atk);
                                // 向き方向の取得
                                Vector3 aim = player.gameObject.transform.position - this.transform.position;
                                // Rigidbodyに力を加えて発射
                                bullets.GetComponent<Rigidbody>().AddForce(aim * 1.5f, ForceMode.Impulse);
                                // 弾丸の位置を調整
                                bullets.transform.position = this.gameObject.transform.position;
                                // 三秒後に削除
                                Destroy(bullets, 3.0f);
                            }

                            ftime++;
                            // if (AILevel == 1)
                            // {
                            if (ftime > 60)
                            {
                                ftime = 0;
                                fireflg = false;
                            }
                            //}
                            // if (AILevel == 0)
                            // {
                            //     if (ftime > 45) { 
                            //         ftime = 0;
                            //         fireflg = false;
                            //     }
                            // }

                        }

                        // 攻撃３処理
                        if (AttackFlg == true)
                        {
                            if (trans == 4)
                            {
                                if (AttackCnt > 30)
                                {
                                    trans = 5;
                                    AttackCnt = 0;
                                }
                            }

                            if (trans == 5)
                            {
                                if (AttackCnt == 0)
                                {
                                    GameObject ds1 = Instantiate(DustSmoke) as GameObject;
                                    ds1.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 2, this.transform.position.z);
                                    Destroy(ds1, 4.0f);

                                    audioSource.volume = 1.0f;
                                    audioSource.PlayOneShot(sound1);
                                }


                                if (AttackCnt == 30)
                                {
                                    SavePlayerPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);
                                }
                                if (AttackCnt == 50)
                                {
                                    audioSource.volume = 1.0f;
                                    audioSource.PlayOneShot(sound2);

                                    GameObject tentacle = Instantiate(Tentacle) as GameObject;
                                    tentacle.transform.position = SavePlayerPos;

                                    // 攻撃力の挿入
                                    EnemyAttackPower eap = tentacle.GetComponent<EnemyAttackPower>();
                                    eap.SetAtkPower(20);
                                }

                                if (AttackCnt > 180)
                                {
                                    trans = 6;
                                    AttackCnt = 0;
                                }
                            }

                            if (trans == 6)
                            {
                                if (AttackCnt > 30)
                                {
                                    trans = 0;
                                    AttackCnt = 0;
                                    AttackFlg = false;
                                }
                            }

                            AttackCnt++;

                            animator.SetInteger("trans", trans);

                        }
                    }



                    // 敵の体力が０になったら
                    if (deadflg == false)
                    {
                        if (ep.hp == 0)
                        {
                            deadflg = true;

                            a = 0.5f;

                            animator.enabled = false;

                            // // 解放処理
                            Destroy(this.gameObject, 5.5f);
                            Destroy(Hpbar.gameObject, 5.5f);
                        }
                    }

                    if (deadflg)
                    {
                        mvalue = Random.Range(-2.0f, 2.0f);
                        if (decnt == 0)
                        {
                            GameObject de = Instantiate(deadeffect) as GameObject;
                            de.transform.position = new Vector3(this.transform.position.x + mvalue, this.transform.position.y + mvalue, this.transform.position.z + mvalue);
                            Destroy(de, 2.0f);
                        }
                        decnt++;
                        if (decnt > 10) { decnt = 0; }


                        a -= 0.01f;
                        material.SetFloat(propID_s, a);
                        if (a < 0) { a = 0; }

                        mvalue = Random.Range(-0.1f, 0.1f);

                        this.gameObject.transform.position = new Vector3(
                            this.gameObject.transform.position.x + mvalue,
                            this.gameObject.transform.position.y - 0.025f,
                            this.gameObject.transform.position.z + mvalue
                        );
                    }


                    // デバッグ表示
                    //Debug.Log("SquidHp");
                    //Debug.Log(ep.hp);

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
                        ep.hp -= collision.gameObject.GetComponent<BulletController>().Damage;
                        if (foundflg == false)
                        {
                            ep.hp = SquidHp;
                            foundflg = true;
                        }

                        if (AILevel == 1)
                        {
                            if (ep.hp <= (SquidHp / 2)) { AILevel = 2; }
                        }
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
                        ep.hp -= collider.gameObject.GetComponent<BulletController>().Damage;
                        if (foundflg == false)
                        {
                            ep.hp = SquidHp;
                            foundflg = true;
                        }

                        if (AILevel == 1)
                        {
                            if (ep.hp <= (SquidHp / 2)) { AILevel = 2; }
                        }
                        if (ep.hp < 0) { ep.hp = 0; }
                        //intパラメーターの値を設定する.
                        animator.SetInteger("trans", trans);
                    }
                }
            }
        }

    }
}
