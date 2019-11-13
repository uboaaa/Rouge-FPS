using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackPower : MonoBehaviour
{
    // Start is called before the first frame update
    private EnemyParameter ep = null;
    public int enemyatkpow = 0;
    public bool SelfFlg = false;
    void Start()
    {
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
}
