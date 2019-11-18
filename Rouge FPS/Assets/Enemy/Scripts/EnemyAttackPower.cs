using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackPower : MonoBehaviour
{
    // Start is called before the first frame update
    private EnemyParameter ep = null;
    public int enemyatkpow = 0;
    public bool SelfFlg = false;

    public static bool DamageFlg;
    void Start()
    {
        DamageFlg=false;

        if(SelfFlg == false)
        {
            ep = GetComponent<EnemyParameter>();
        
            // パラメータから攻撃力を取得
            enemyatkpow = ep.atk;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(enemyatkpow);
    }
        void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
        MyStatus.downHp((float)enemyatkpow);
        DamageFlg=true;
        Destroy(this);}

    }
   public static bool GetEnemyBHitGet(){return DamageFlg;}
}
