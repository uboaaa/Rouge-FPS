using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDown : MonoBehaviour
{

    
  public GameObject FPSCon;
  Transform myTransform2;

      Vector3 worldAngle ;
  float z;
    // Start is called before the first frame update
    void Start()
    {
    myTransform2 = this.transform;
    z=0.0f;
    worldAngle = myTransform2.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        

 // ワールド座標を基準に、回転を取得
    
    if(FPSCon.GetComponent<MyStatus>().GetHp()<1){
              worldAngle.z +=1.0f;  
                myTransform2.eulerAngles = worldAngle; // 回転角度を設定
        if(worldAngle.z<91.0f){
        // worldAngle.z +=1.0f;  
        // myTransform2.eulerAngles = worldAngle; // 回転角度を設定
        }
    }

    }
}
