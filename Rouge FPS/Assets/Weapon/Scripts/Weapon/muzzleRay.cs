using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class muzzleRay : MonoBehaviour
{

    CameraRay RSSCript;
    void Start() 
    {
        // 9. 別オブジェクトにアタッチしているスクリプトをオブジェクト名で参照する
        GameObject anotherObject = GameObject.Find("FirstPersonCharacter");
        RSSCript = anotherObject.GetComponent<CameraRay>();
    }

    void Update()
    {
        Vector3 RayHitPos = RSSCript.hit.point;

        this.transform.LookAt(RayHitPos);
    }
}
