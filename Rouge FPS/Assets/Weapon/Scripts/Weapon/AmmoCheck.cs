using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCheck : MonoBehaviour
{
    Text myText;
    ChangeEquip CEScript;
    GunController GCScript;
    void Start()
    {
        // 残弾数テキスト用
        myText = GetComponent<Text>();


        GameObject FPSCon = GameObject.Find("FPSController");
        CEScript = FPSCon.GetComponent<ChangeEquip>();
    }
    
    void Update()
    {
        switch(CEScript.ownGun)
        {
            case 0:             // 武器を持っていない時
                myText.text = "";
                break;
            case 1:             // プライマリ武器を持っている時
                GCScript = CEScript.PrimaryWeapon.GetComponent<GunController>();
                myText.text = GCScript.Ammo + "/" + GCScript.remAmmo;
                break;
            case 2:             // セカンダリ武器を持っている時
                GCScript = CEScript.SecondaryWeapon.GetComponent<GunController>();
                myText.text = GCScript.Ammo + "/" + GCScript.remAmmo;
                break;
            default:
                break;
        }
    }
}
