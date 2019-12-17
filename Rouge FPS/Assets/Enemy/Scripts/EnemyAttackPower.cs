﻿using System.Collections;
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

    private GameObject FPSCon;
    void Start()
    {

        FPSCon = GameObject.Find("FPSController");
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
       
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            
          FPSCon.GetComponent<MyStatus>().downHp((float)enemyatkpow);
            DamageFlg = true;
            
            Destroy(this);
        }

    }
   public static bool GetEnemyBHitGet(){return DamageFlg;}
}
