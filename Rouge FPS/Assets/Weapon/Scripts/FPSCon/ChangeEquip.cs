
using UnityEngine;
using System.Collections;

public class ChangeEquip : MonoBehaviour
{
    // インスペクター関係============================================
    public GameObject PrimaryWeapon;          // プライマリ武器の情報
    public GameObject SecondaryWeapon;        // セカンダリ武器の情報

    // パラメーター関係==============================================
    [HideInInspector] public int ownGun;      // 0:持ってない 1:プライマリ 2:セカンダリ
    [HideInInspector] public bool activeFlg;  // 行動中か  

     // スクリプト関係================================================
    GunController GCPrimaryScript;                   // [GunController]用の変数
    GunController GCSecondaryScript;                 // [GunController]用の変数

    void Start()
    {
        ownGun = 1;
        if(PrimaryWeapon != null)
        {
            PrimaryWeapon.SetActive(true);
            GCPrimaryScript = PrimaryWeapon.GetComponent<GunController>();
        }

        if(SecondaryWeapon != null)
        {
            SecondaryWeapon.SetActive(false);
            GCSecondaryScript = SecondaryWeapon.GetComponent<GunController>();
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !activeFlg && SecondaryWeapon != null)
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

    // 落ちている武器を拾う
    public GameObject GetWeapon(GameObject dropItem)
    {
        GameObject aiueo;   // 情報保持用
        // プライマリ武器しか持っていないとき
        if(ownGun == 1 && SecondaryWeapon == null)
        {
            SecondaryWeapon = dropItem;

            PrimaryWeapon.SetActive(false);
            SecondaryWeapon.SetActive(true);

            GCSecondaryScript = SecondaryWeapon.GetComponent<GunController>();
            
            ownGun = 2;
            
            return null;
        }
        // プライマリ武器と交換する
        else if(ownGun == 1)
        {
            PrimaryWeapon.SetActive(false);

            aiueo = PrimaryWeapon;
            PrimaryWeapon = dropItem;
            dropItem = aiueo;

            PrimaryWeapon.SetActive(true);

            return dropItem;
        }

        // セカンダリ武器と交換する
        if(ownGun == 2)
        {
            SecondaryWeapon.SetActive(false);
            
            aiueo = SecondaryWeapon;
            SecondaryWeapon = dropItem;
            dropItem = aiueo;

            SecondaryWeapon.SetActive(true);

            return dropItem;
        }
        
        return null;
    }
}
  