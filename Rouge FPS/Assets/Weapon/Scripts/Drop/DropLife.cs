using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLife : MonoBehaviour
{
    [SerializeField] float LifeNum;     // 拾った時のライフの数
    private bool hitFlg;                // 当たった判定用

    GameObject FPSCon;

    void Start()
    {
        FPSCon = GameObject.Find("FPSController");
    }

    void Update()
    {
        // 当たった時の処理
        if(hitFlg)
        {
            // 体力の回復
            FPSCon.GetComponent<MyStatus>().AddHp(LifeNum);
            
            // 自分を削除
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if( other.gameObject.tag == "Player")
        {
            if(FPSCon.GetComponent<MyStatus>().GetMaxHp() > FPSCon.GetComponent<MyStatus>().GetHp())
            {
                hitFlg = true;
            }
        }
    }
}
