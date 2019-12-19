// 動かないものに使える
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObject : MonoBehaviour
{
    public float swingPow;
    private float position;
    public int  Range = 2;

    void Start()
    {
        position = transform.position.y;
    }
    
    // Sin波を使い上下に揺らす
    void Update () 
    {
        transform.position = new Vector3(transform.position.x, position + Mathf.Sin (Time.frameCount * swingPow ) / Range,transform.position.z);   
    }
}
