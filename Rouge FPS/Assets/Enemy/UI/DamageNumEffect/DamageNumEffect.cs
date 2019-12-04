using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumEffect : MonoBehaviour
{

    GameObject enemy;
    EnemyHitEffect eh;

    private bool hitflg = false;
    public bool GetHitFlg(){ return hitflg;}
    public void SetHitFlg(bool _a)
    {
        hitflg = _a;
    }

    private int BulletDamage = 0;
    public void SetBulletDamage(int a)
    {
        BulletDamage = a;
    }

    GameObject DamageNumPrefab;

    public float positionY;

    // public bool FeedOutFlg;
    // public float AlphaTime;

    void FeedOut(GameObject _go)
    {

        // TextMesh tm = _go.GetComponent<TextMesh>();
        // float a = tm.color.a;
        // tm.color = new Color(1.0f, 1.0f, 1.0f, a);
        // a -= 0.5f;

        // if (a < 0)
        // {
        //     Destroy(_go);
        // }
        TextMesh tm = _go.GetComponent<TextMesh>();
        float a = tm.color.a;
        for(int i = 0;i < 10;i++)
        {
            tm.color = new Color(1.0f, 1.0f, 1.0f, a);
            a -= 0.5f;
        }
        Destroy(_go,1.0f);
    }


    // Start is called before the first frame update
    void Start()
    {
        // エネミー情報取得
        enemy = this.gameObject;
        // エネミーヒットエフェクト
        eh = GetComponent<EnemyHitEffect>();

        DamageNumPrefab = Resources.Load("DamageNum") as GameObject;

        // FeedOutFlg = false;
        // AlphaTime = 1.0f;

    }


    // Update is called once per frame
    void Update()
    {
        if (hitflg == true)
        {
            // ダメージの値を挿入
            DamageNumPrefab.GetComponent<TextMesh>().text = "" + BulletDamage;

            // プレハブ生成
            GameObject dn = Instantiate(DamageNumPrefab) as GameObject;

            // 敵の座標へ移動
            float value = Random.Range(-1.0f, 1.0f);    // 乱数
            dn.gameObject.transform.position = new Vector3(enemy.gameObject.transform.position.x + value, enemy.gameObject.transform.position.y + positionY, enemy.gameObject.transform.position.z + value);

            // 向き方向の取得
            Vector3 force = new Vector3(0.0f, 3.5f, 0);

            // Rigidbodyに力を加えて発射
            dn.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);


            hitflg = false;
        }

    }

}
