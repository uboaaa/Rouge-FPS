
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
    GameObject child;

     // スクリプト関係================================================
    GunController GCPrimaryScript;                   // [GunController]用の変数
    GunController GCSecondaryScript;                 // [GunController]用の変数

    void Start()
    {
        // 子オブジェクトを取得（FirstPersonCharacter）
        child = transform.FindChild("FirstPersonCharacter").gameObject;

        ownGun = 0;
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
        Debug.Log(ownGun);
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
        GameObject dropInfo;   // 情報保持用

        // 何も持っていないとき
        if(ownGun == 0)
        {
            PrimaryWeapon = dropItem;
            GameObject tmp = Instantiate(PrimaryWeapon,PrimaryWeapon.transform.position,PrimaryWeapon.transform.rotation);
            tmp.transform.parent = child.transform;
            
            PrimaryWeapon = tmp;

            PrimaryWeapon.SetActive(true);

            GCPrimaryScript = PrimaryWeapon.GetComponent<GunController>();

            ownGun = 1;

            return null;
        }

        // プライマリ武器しか持っていないとき
        if(ownGun == 1 && SecondaryWeapon == null)
        {
            SecondaryWeapon = dropItem;
            GameObject tmp = Instantiate(SecondaryWeapon,SecondaryWeapon.transform.position,SecondaryWeapon.transform.rotation);
            tmp.transform.parent = child.transform;

            SecondaryWeapon = tmp;

            PrimaryWeapon.SetActive(false);
            SecondaryWeapon.SetActive(true);

            GCSecondaryScript = SecondaryWeapon.GetComponent<GunController>();
            
            ownGun = 2;
            
            return null;
        }
        // プライマリ武器と交換する
        else if(ownGun == 1)
        {
            // 拾う武器種をすでに持っている場合
            // if(PrimaryWeapon.name != dropItem.name + "(clone)" && SecondaryWeapon.name == dropItem.name + "(clone)")
            // {
            // }

            PrimaryWeapon.SetActive(false);

            dropInfo = PrimaryWeapon;

            Destroy(PrimaryWeapon);

            PrimaryWeapon = dropItem;
            GameObject tmp = Instantiate(PrimaryWeapon,SecondaryWeapon.transform.position,SecondaryWeapon.transform.rotation);
            tmp.transform.parent = child.transform;

            PrimaryWeapon = tmp;

            dropItem = dropInfo;

            PrimaryWeapon.SetActive(true);

            return dropItem;
        }

        // セカンダリ武器と交換する
        if(ownGun == 2)
        {
            // 拾う武器種をすでに持っている場合
            // if(PrimaryWeapon.name == dropItem.name + "(clone)" && SecondaryWeapon.name != dropItem.name + "(clone)")
            // {
            // }

            SecondaryWeapon.SetActive(false);
            
            dropInfo = SecondaryWeapon;

            Destroy(SecondaryWeapon);
            SecondaryWeapon = dropItem;
            GameObject tmp = Instantiate(SecondaryWeapon,SecondaryWeapon.transform.position,SecondaryWeapon.transform.rotation);
            tmp.transform.parent = child.transform;

            SecondaryWeapon = tmp;
            
            dropItem = dropInfo;

            SecondaryWeapon.SetActive(true);

            return dropItem;
        }
        return null;
    }
}
  