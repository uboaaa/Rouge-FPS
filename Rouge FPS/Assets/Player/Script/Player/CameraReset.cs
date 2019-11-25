using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraReset : MonoBehaviour
{
    Vector3 abc = new Vector3(0.0f, 0.0f,0.0f);

    void Start()
    {
        abc.x= MapInitializer.GetSpawnData("rx");
        abc.y= MapInitializer.GetSpawnData("ry");
        abc.z= MapInitializer.GetSpawnData("rz");
    }


    void Update()
    {
        //カメラの方向を取得
        //camera.transform.rotation  = Quaternion.identity;
        if (Input.GetKey(KeyCode.Return)) {
            abc.x = MapInitializer.GetSpawnData("rx");
            abc.y = MapInitializer.GetSpawnData("ry");
            abc.z = MapInitializer.GetSpawnData("rz");
            this.transform.rotation = Quaternion.Euler(abc);
        }
    }
}
