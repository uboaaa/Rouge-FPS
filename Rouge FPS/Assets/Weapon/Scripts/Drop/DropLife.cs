using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLife : MonoBehaviour
{
    [SerializeField] float LifeNum;     // 拾った時のライフの数
    private bool hitFlg;                // 当たった判定用

    GameObject FPSCon;
    float maxDis = 30.0f;
    Rigidbody rd;
    FlyingObject FOScript;

    void Start()
    {
        FPSCon = GameObject.Find("FPSController");
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit,maxDis))
        {
             if(hit.collider.tag == "Back")
            {
                maxDis -= 0.7f;
            }
        }

        if(maxDis <= 0.5f)
        {
            rd = this.GetComponent<Rigidbody>();
            rd.useGravity = false;

            if(!FOScript)
            {
                FOScript = gameObject.AddComponent<FlyingObject>();
                FOScript.swingPow = 0.05f; 
                FOScript.Range = 4;
            }
        }
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
