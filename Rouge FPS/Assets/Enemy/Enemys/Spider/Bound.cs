using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour
{
    private Rigidbody rb;//バウンドさせたいオブジェクトを宣言


    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }
　  
    //sphereに触れた時のメソッド
    private void OnCollisionEnter(Collision collision){
        //Y軸方向に常に同じ力を与える
        rb.AddForce(Vector3.up*1f,ForceMode.Impulse);
    }
}
