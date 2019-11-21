//===============================================================
// ライト用スクリプト
//===============================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlgLight : MonoBehaviour
{
    [SerializeField] private bool onMode = true;        // 常時ONモード
    [SerializeField] private bool offonMode = false;    // OFFからONになるモード
    [SerializeField] private bool secondMode = false;   // 生成されてから何秒かにライトをつけるモード
    [SerializeField] private float seconds = 0;         // 秒数
    private float count;
    private Light lightComp;
    [HideInInspector] public bool LightFlg = true;   // ライトをONOFF用

    void Start()
    {
        lightComp = GetComponent<Light>();
        if(onMode) LightFlg = true;
    }


    void Update()
    {
        if(secondMode)
        {
            count += Time.deltaTime;
            if(count >= seconds)
            {
                LightFlg = true;
            }
        }

        if(LightFlg)
        {
            lightComp.enabled = true;
        } else if (!LightFlg) {
            lightComp.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 検出範囲に入った時の処理
         if(other.gameObject.tag == "Player" && offonMode)
        {
            LightFlg = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //LightFlg = false;
    }
}
