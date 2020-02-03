using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadTextManager : MonoBehaviour
{
    GameObject gO;
    GunController GCScript;
    GameObject childObject;
    Text Text;

    void Start()
    {
        childObject = transform.Find("ReloadText").gameObject;
        Text = childObject.GetComponent<Text>();
    }

    void Update()
    {
        GCScript = ChangeEquip.nowWeapon().GetComponent<GunController>();

        if(GCScript.Ammo <= 0)
        {
            // 表示
            Text.enabled = true;

            // テキストを変える
            Text.text = "No Ammo";

            // 色を黄色にする
            Text.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        } else if(GCScript.Ammo < (GCScript.MagazineSize / 3)) {
            // 表示
            Text.enabled = true;

            // テキストを変える
            Text.text = "Low Ammo";

            // 色を黄色にする
            Text.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        } else {
            // 表示
            Text.enabled = false;
        }
    }
}
