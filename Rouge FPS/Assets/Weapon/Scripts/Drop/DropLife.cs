using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLife : MonoBehaviour
{
    [SerializeField] float LifeNum;     // 拾った時のライフの数
    private bool hitFlg;                // 当たった判定用

    GameObject FPSCon;
    public float maxDistance = 100.0f;   // 計測可能な最大距離
    public float distance;              // 計測距離
    Rigidbody rd;
    FlyingObject FOScript;

    void Start()
    {
        FPSCon = GameObject.Find("FPSController");
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit,maxDistance))
        {
             if(hit.collider.tag == "Back")
            {
                distance = hit.distance;
            }
        }

        if(distance <= 0.8f)
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
