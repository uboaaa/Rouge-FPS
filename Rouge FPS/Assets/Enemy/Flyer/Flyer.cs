using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer : MonoBehaviour
{
    private GameObject player;
    public GameObject bullet;

    public float speed = 0.0f;//移動スピード
    private Vector3 vec;
    private float rot = 0.0f;

    private Animator animator;


    // 弾丸の速度
    public float bspeed = 1000;

    

    

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

        player = GameObject.Find("FPSController");


        // 角度計算
        rot = GetAim(new Vector2(transform.position.x, transform.position.z),
            new Vector2(player.gameObject.transform.position.x, player.gameObject.transform.position.z));
    }

    void Update()
    {

        // 移動 =========

        //targetに向かって進む
        transform.position += transform.forward * speed;



        // アニメーション ========
        //あらかじめ設定していたintパラメーター「trans」の値を取り出す.
        int trans = animator.GetInteger("trans");


        // 角度計算
        rot = GetAim(new Vector2(transform.position.x, transform.position.z),
            new Vector2(player.gameObject.transform.position.x, player.gameObject.transform.position.z));


        // 角度計算
        // 正面
        if (rot >= -45.0f && rot <= 45.0f)
        {
            trans = 0;
            //speed = 0.05f;

        }
        // 右面
        else if (rot >= 45.0f && rot <= 135.0f)
        {
            trans = 1;

            speed = 0.0f;
        }
        // 左面
        else if (rot >= -135 && rot <= -45)
        {
            trans = 3;

            speed = 0.0f;
        }
        // 後面
        else
        {
            trans = 2;

            speed = 0.0f;
        }

        //intパラメーターの値を設定する.
        animator.SetInteger("trans", trans);


        // デバッグ表示
        //Debug.Log(rot);

        // 弾発射
        // z キーが押された時
        if (Input.GetKeyDown(KeyCode.Z))
        {

            // 弾丸の複製
            GameObject bullets = Instantiate(bullet) as GameObject;

            // 向き方向の取得
            Vector3 aim = player.gameObject.transform.position - this.transform.position;

            // // 向き
            // Vector3 force;

            // force = aim * speed;

            // Rigidbodyに力を加えて発射
            bullets.GetComponent<Rigidbody>().AddForce(aim,ForceMode.Impulse);

            // 弾丸の位置を調整
            bullets.transform.position = this.gameObject.transform.position;


            Destroy(bullets, 3.0f); // 三秒後に削除

        }
    }

}

