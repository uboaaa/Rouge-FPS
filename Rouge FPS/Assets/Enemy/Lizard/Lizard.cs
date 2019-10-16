using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MonoBehaviour
{
    // プレイヤー
    public GameObject player;
    // アニメータ
    private Animator animator;
  
    // 移動スピード
    public float speed;
    // 角度計算用
    private float rot;
    // 初期角度
    public float startrot;
    // 発見フラグ
    bool foundflg = false;

    // ヒットエフェクト関連
    // マテリアル
    public Material hitmaterial;
    // シェーダパラメータ管理用
    public int propID = 0;
    // 輝度パラメータ調整用
    private float brightness = 0.5f;
    //  
    private float i = 0.1f;
    // ヒットエフェクトフラグ
    private bool hitflg = false;


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
        //GetComponentを用いてAnimatorコンポーネントを取り出す.
        animator = GetComponent<Animator>();
        propID = Shader.PropertyToID("_Brightness");

    }


    void Update()
    {
        // ヒットエフェクト
        if (hitflg == false)
        {
            if (Input.GetKey("1"))
            {
                hitflg = true;
            }
        }


        if (hitflg == true)
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

        hitmaterial.SetFloat(propID, brightness);


        // アニメーション ========
        //あらかじめ設定していたintパラメーター「trans」の値を取り出す.
        int trans = animator.GetInteger("trans");


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
            //targetに向かって進む
            transform.position += transform.forward * speed * 0.1f;
        }
        // 発見フラグがOFFなら
        else if (foundflg == false)
        {
            // 角度計算
            rot = GetAim(new Vector2(transform.position.x, transform.position.z),
                new Vector2(player.gameObject.transform.position.x, player.gameObject.transform.position.z));

            rot = rot + startrot;


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

            //intパラメーターの値を設定する.
            animator.SetInteger("trans", trans);
        }



        // // デバッグ表示
        // Debug.Log((transform.position - player.gameObject.transform.position).magnitude);



    }
}


