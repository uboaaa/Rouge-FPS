//=============================================
// カメラ用スクリプト
//=============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour 
{
    // パラメーター関係==============================================
    float duration;             // 揺れ時間

    public void Shake(float magnitude,bool loopFlg)
    {
        duration = 0;
        if(loopFlg) StartCoroutine( DoShake(magnitude) );
    }

    private IEnumerator DoShake(float magnitude)
    {
        var pos = transform.localPosition;

        var elapsed = 0f;

        duration += Time.deltaTime;

        while ( elapsed < duration )
        {
            var x = pos.x + Random.Range( -1f, 1f ) * magnitude;
            var y = pos.y + Random.Range( -1f, 1f ) * magnitude;

            transform.localPosition = new Vector3( x, y, pos.z );

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = pos;
    }
}