using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
 
public class EXPSlider : MonoBehaviour
{
    // インスペクター関係============================================
    // ※無理やり取得しているため変更予定
    public GameObject PrimaryObject;           // プライマリ武器の情報
    public GameObject SecondaryObject;         // セカンダリ武器の情報

    // パラメーター関係==============================================
    float maxEXP = 5000;                    // 最大経験値必要数（取得経験値は武器が保持している）
    // スクリプト関係================================================
    GunController PrimaryScript;            // プライマリ武器のスクリプト用
    GunController SecondaryScript;          // セカンダリ武器のスクリプト用
    ChangeEquip CEScript;                   // [ChangeEquip]用の変数
    Slider expSlider;                       // スライダー用
 
    void Start()
    {
        expSlider = GetComponent<Slider>();

        GameObject FPSCon = GameObject.Find("FPSController");
        CEScript = FPSCon.GetComponent<ChangeEquip>();       

        //GameObject anotherObject = GameObject.Find("HandGun");
        PrimaryScript = PrimaryObject.GetComponent<GunController>();

        //GameObject SecondaryObject = GameObject.Find(CEScript.Weapon2.name);
        SecondaryScript = SecondaryObject.GetComponent<GunController>();     

        // スライダーの最大値の設定
        expSlider.maxValue = maxEXP;
    }
 
    void Update()
    {
        // 持っている銃の経験値を入れる
        if(ChangeEquip.ownGun == 1)
        {   
            expSlider.value = PrimaryScript.GunEXP;
        }
        else if(ChangeEquip.ownGun == 2)
        {   
            expSlider.value = SecondaryScript.GunEXP;
        }

        
        
        if(maxEXP <= PrimaryScript.GunEXP)
        {
            PrimaryScript.GunEXP = PrimaryScript.GunEXP - maxEXP;
        }
        if(maxEXP <= SecondaryScript.GunEXP)
        {
            SecondaryScript.GunEXP = SecondaryScript.GunEXP - maxEXP;
        }

    }
 
 
}