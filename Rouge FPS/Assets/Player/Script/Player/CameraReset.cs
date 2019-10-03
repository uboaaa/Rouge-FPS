using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraReset : MonoBehaviour

{
    Vector3 cameraAngle;　//カメラの角度を代入する変数
    public new GameObject camera; //カメラオブジェクトを格納

    //オブジェクトを動かすために必要
    public float thrust;　//勢いの強さ
    Rigidbody rb;        //リジットボディー

    void Start()
    {
        rb = this.GetComponent<Rigidbody>(); //リジッドボディー参照
        thrust = 100f;　　　　　　　　　　　　　　//勢いの初期化
    }


    void FixedUpdate()
    {
        //カメラの方向を取得
     camera.transform.rotation  = Quaternion.identity;
    }
}
