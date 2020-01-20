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

    public static bool DamageFlg;
    void Start()
    {
        DamageFlg=false;

        // 弾じゃなかったらそのまま攻撃力を取得
        if(BulletFlg == false)
        {
            ep = GetComponent<EnemyParameter>();
        
            // パラメータから攻撃力を取得
            enemyatkpow = ep.atk;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(MyStatus.GetHp());
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            
            MyStatus.downHp((float)enemyatkpow);
            DamageFlg = true;
            
            Destroy(this);
        }

    }
   public static bool GetEnemyBHitGet(){return DamageFlg;}
}
