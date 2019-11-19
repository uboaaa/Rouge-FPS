
using UnityEngine;
using System.Collections;

public class ChangeEquip : MonoBehaviour
{
    // インスペクター関係============================================
    public GameObject PrimaryWeapon;          // プライマリ武器の情報
    public GameObject SecondaryWeapon;        // セカンダリ武器の情報

    // パラメーター関係==============================================
    [HideInInspector] public int ownGun;      // 0:持ってない 1:プライマリ 2:セカンダリ
    [HideInInspector] public bool activeFlg;  // 武器切り替え中か  

     // スクリプト関係================================================
    GunController GCPrimaryScript;                   // [GunController]用の変数
    GunController GCSecondaryScript;                 // [GunController]用の変数

    void Start()
    {
        ownGun = 1;
        PrimaryWeapon.SetActive(true);
        SecondaryWeapon.SetActive(false);

        GCPrimaryScript = SecondaryWeapon.GetComponent<GunController>();
        GCSecondaryScript = SecondaryWeapon.GetComponent<GunController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !activeFlg)
        {
            GCPrimaryScript.shooting = false;
            GCSecondaryScript.shooting = false;
            activeFlg = true;
            ChangeWeapon();
        }
    }

    void ChangeWeapon()
    {
        if (SecondaryWeapon.activeSelf)
        {
            ownGun = 1;
            PrimaryWeapon.SetActive(true);
            SecondaryWeapon.SetActive(false);
        }
        else
        {
            ownGun = 2;
            PrimaryWeapon.SetActive(false);
            SecondaryWeapon.SetActive(true);
        }

    }
}
  