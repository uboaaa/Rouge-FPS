using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraReset : MonoBehaviour
{
 

    void Start()
    {

    }


    void Update()
    {
        //カメラの方向を取得
        //camera.transform.rotation  = Quaternion.identity;
        if (Input.GetKey(KeyCode.Return)) { 
        this.transform.rotation =  Quaternion.Euler(0.0f, 0.0f, 0.0f);
            }
    }
}
