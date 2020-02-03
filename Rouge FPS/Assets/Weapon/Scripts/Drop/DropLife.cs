using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLife : MonoBehaviour
{
    [SerializeField] float LifeNum;     // 拾った時のライフの数
    private bool hitFlg;                // 当たった判定用

    GameObject FPSCon;
    Rigidbody rd;
    FlyingObject FOScript;

    void Start()
    {
        FPSCon = GameObject.Find("FPSController");
    }

    void Update()
    {
        // ポーズ中動作しないようにする
        if(!PauseScript.pause()){}
        if(!SkillManagement.GetTimeStop()){}
        
        // 当たった時の処理
        if(hitFlg)
        {
            // サウンドを鳴らす
            SoundManager soundManager = GameObject.Find("DropLife").GetComponent<SoundManager>();
            soundManager.Play(1);

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

        if(other.gameObject.tag == "Back")
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
    }
}
