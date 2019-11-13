using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
 
public class EXPSlider : MonoBehaviour
{
    Slider expSlider;
    GunController PrimaryScript;
    GunController SecondaryScript;
    ChangeEquip CEScript;

    public GameObject PrimaryObject;
    public GameObject SecondaryObject;
    float maxEXP = 5000;
 
    void Start()
    {
        expSlider = GetComponent<Slider>();

        GameObject FPSCon = GameObject.Find("FPSController");
        CEScript = FPSCon.GetComponent<ChangeEquip>();       

        //GameObject anotherObject = GameObject.Find("HandGun");
        PrimaryScript = PrimaryObject.GetComponent<GunController>();

        //GameObject SecondaryObject = GameObject.Find(CEScript.Weapon2.name);
        SecondaryScript = SecondaryObject.GetComponent<GunController>();     

        //スライダーの最大値の設定
        expSlider.maxValue = maxEXP;
    }
 
    void Update()
    {
        // 持っている銃の経験値を入れる
        if(CEScript.ownGun == 1)
        {   
            expSlider.value = PrimaryScript.GunEXP;
        }
        else if(CEScript.ownGun == 2)
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