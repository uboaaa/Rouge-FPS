using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDown : MonoBehaviour
{

    
  public GameObject FPSCon;
  Transform myTransform2;

  Vector3 _Rotation ;
      Vector3 worldAngle ;
  float z;

  GameObject tmp;
    // Start is called before the first frame update
    void Start()
    {
      tmp = GameObject.Find("FirstPersonCharacter");
  
    _Rotation = this.transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
  myTransform2 = this.transform;
    worldAngle = myTransform2.eulerAngles;
    if(FPSCon.GetComponent<MyStatus>().GetHp()<1){
 
 
              worldAngle.z +=1.0f;  
                myTransform2.eulerAngles = worldAngle; // 回転角度を設定
    }
else{     _Rotation = this.transform.localEulerAngles;
 // ワールド座標を基準に、回転を取得
            // worldAngle.x=0;
            //   worldAngle.y=0;
            }
    }
}
