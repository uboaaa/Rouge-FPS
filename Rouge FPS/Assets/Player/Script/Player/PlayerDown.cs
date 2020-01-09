using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDown : MonoBehaviour
{

    
  GameObject FPSCon;
  Transform myTransform2;
  float z;
    // Start is called before the first frame update
    void Start()
    {
    FPSCon = GameObject.Find("FPSController");
    myTransform2 = this.transform;
    z=0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
 // ワールド座標を基準に、回転を取得
        Vector3 worldAngle = myTransform2.eulerAngles;
    if(FPSCon.GetComponent<MyStatus>().GetHp()<1){
        if(worldAngle.z<91.0f){
        worldAngle.z +=1.0f;  // ワールド座標を基準に、z軸を軸にした回転を10度に変更
        myTransform2.eulerAngles = worldAngle; // 回転角度を設定
        }
    }

    }
}
