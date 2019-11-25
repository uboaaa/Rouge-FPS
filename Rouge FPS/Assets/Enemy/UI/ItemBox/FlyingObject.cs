using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObject : MonoBehaviour
{
    public float FlyPow;
    public float posisionY;
    
    // Sin波を使い上下に揺らす
    void Update () {
        transform.position = new Vector3(transform.position.x
        , posisionY + Mathf.Sin (Time.frameCount * FlyPow ) / 2,transform.position.z);
    }
}
