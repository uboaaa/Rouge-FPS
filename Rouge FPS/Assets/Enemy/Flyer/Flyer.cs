using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer : MonoBehaviour
{
    public GameObject player;

    public float speed = 0.0f;//移動スピード
    private Vector3 vec;
    private float rot = 0.0f;

    private Animator animator;



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


        // 角度計算
        rot = GetAim(new Vector2(transform.position.x, transform.position.z),
            new Vector2(player.gameObject.transform.position.x, player.gameObject.transform.position.z));
    }

    void Update()
    {

        // 移動 =========

        ////targetの方に少しずつ向きが変わる
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.gameObject.transform.position - transform.position), 0.009f);

        //targetに向かって進む
        transform.position += transform.forward * speed;


        // ビルボード ========
        Vector3 p = player.gameObject.transform.position;
        p.y = transform.position.y;
        transform.LookAt(p);


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
        Debug.Log(rot);



    }

}
