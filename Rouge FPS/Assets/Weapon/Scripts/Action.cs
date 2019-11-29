//=======================================================================
// 調べたり、拾うなどの動作用スクリプト
// FPSControllerに付ける
//=======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    [SerializeField] GameObject handgun;
    [SerializeField] GameObject LightMachineGun;
    [SerializeField] GameObject AssaultRifle;
    [SerializeField] GameObject SubMachineGun;
    [SerializeField] GameObject RocketLauncher;
    [SerializeField] GameObject ShotGun;
    [SerializeField] GameObject FlameThrower;

    private GameObject dropPrefab;      
    DropItem DIScript;
    ChangeEquip CEScript;                   // [ChangeEquip]用の変数
    bool actionFlg;
    GameObject ObjectInfo;
    GameObject tmp;
    void Start()
    {
        CEScript = GetComponent<ChangeEquip>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && actionFlg)
        {
            if(ObjectInfo.tag == "DropBox"){BoxAction(ObjectInfo);}
            
            if(ObjectInfo.tag == "DropGun"){WeaponAction(ObjectInfo);}

            actionFlg = false;
        }
    }

    void BoxAction(GameObject Object)
    {
        // 宝箱を削除
        Destroy(Object.gameObject);

        // DropItemを生成
        
    }

    void WeaponAction(GameObject Object)
    {
        // DropItemComponentを取得
        DIScript = Object.GetComponent<DropItem>();

        // 落ちている武器を装備する
        // ※dropPrefabには落とす武器が入る
        dropPrefab = CEScript.GetWeapon(DIScript.WeaponInfo);

        // 交換した武器を生成する
        if(dropPrefab != null)
        {
            GameObject hadWeapon;      // 持っていた武器

		    hadWeapon = Instantiate<GameObject>(Object,Object.transform.position,Quaternion.identity);
            hadWeapon.name = DIScript.WeaponInfo.name;

            // GunInfoの中に持っていた武器の情報を入れる
            GameObject childObject = transform.Find("FirstPersonCharacter/" + dropPrefab.name).gameObject;
            GunController GCScript = childObject.GetComponent<GunController>();
            GunInfo GIScript = hadWeapon.GetComponent<GunInfo>();

            GIScript.gunRank = GCScript.gunRank;

            GIScript.gunType = GCScript.gunType;

            GIScript.skillSlot = GCScript.skillSlot;

            GIScript.OneMagazine = GCScript.OneMagazine;

            GIScript.MaxAmmo = GCScript.MaxAmmo;

            GIScript.Damage = GCScript.Damage;

            GIScript.shootInterval = GCScript.shootInterval;

            GIScript.reloadInterval = GCScript.reloadInterval;

            GIScript.bulletPower = GCScript.bulletPower;

            GIScript.GunEXP = GCScript.GunEXP;

            // 落ちてる武器に持っていた武器を入れる
            DIScript.WeaponInfo = hadWeapon;
        }

        // 落ちている武器を削除
        Destroy(Object.gameObject);
    }

    
    void OnTriggerEnter(Collider other)
    {
        if( other.gameObject.tag == "DropBox" ||
            other.gameObject.tag == "DropGun")
        {
            ObjectInfo = other.gameObject;
            actionFlg = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        actionFlg = false;
    }
}
