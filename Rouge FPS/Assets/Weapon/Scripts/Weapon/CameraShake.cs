﻿//=============================================
// カメラ用スクリプト
//=============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour 
{
    // パラメーター関係==============================================
    float duration;             // 揺れ時間

    public void Shake(float magnitude,bool looping)
    {
        duration = 0;
        if(looping) StartCoroutine( DoShake(magnitude) );
    }

    private IEnumerator DoShake(float magnitude)
    {
        var pos = transform.localPosition;

        var elapsed = 0f;

        float min = -1.0f;
        float max = 1.0f;

        duration += Time.deltaTime;

        while ( elapsed < duration )
        {
            //var x = pos.x + Random.Range( -0.2f, 0.2f )* magnitude;
            var y = pos.y + Random.Range( min, max ) * magnitude;

            transform.localPosition = new Vector3( pos.x, y, pos.z );

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = pos;
    }
}