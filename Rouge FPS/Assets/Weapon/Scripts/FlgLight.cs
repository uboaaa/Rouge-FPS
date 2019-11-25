//===============================================================
// ライト用スクリプト
//===============================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlgLight : MonoBehaviour
{
    [SerializeField] public enum LIGHTMODE
    {
        onMODE,                 // 常時ONモード
        offonMODE,              // OFFからONになるモード
        secondMODE              // 生成されてから何秒かにライトをつけるモード
    }
    public LIGHTMODE mode = LIGHTMODE.onMODE;
    [SerializeField] private float seconds = 0;         // 秒数
    private float count;
    private Light lightComp;
    [HideInInspector] public bool LightFlg = true;   // ライトをONOFF用

    void Start()
    {
        lightComp = GetComponent<Light>();
        if(mode == LIGHTMODE.onMODE) LightFlg = true;
    }


    void Update()
    {
        if(mode == LIGHTMODE.secondMODE)
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
         if(other.gameObject.tag == "Player" && mode == LIGHTMODE.offonMODE)
        {
            LightFlg = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //LightFlg = false;
    }
}
