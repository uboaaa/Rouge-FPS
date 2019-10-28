using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObject : MonoBehaviour
{
    public float i;
    
    // Sin波を使い上下に揺らす
    void Update () {
        transform.position = new Vector3(transform.position.x
        , 0.5f + Mathf.Sin (Time.frameCount * i ) / 2,transform.position.z);
    }
}
