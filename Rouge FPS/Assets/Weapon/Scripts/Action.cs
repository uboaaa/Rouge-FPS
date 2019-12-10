//=======================================================================
// 調べたり、拾うなどの動作用スクリプト
// FPSControllerに付ける
//=======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    private GameObject DropWeapon;      
    DropItem DIScript;
    ChangeEquip CEScript;                   // [ChangeEquip]用の変数
    LoadGunPrefab LGPScript;
    bool actionFlg;
    GameObject ObjectInfo;
    GameObject tmp;
    void Start()
    {
        CEScript = GetComponent<ChangeEquip>();
        LGPScript = GetComponent<LoadGunPrefab>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && actionFlg)
        {
            // 宝箱処理
            if(ObjectInfo.tag == "DropBox"){BoxAction(ObjectInfo);}
            
            // 落ちてる銃の処理
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
        // DropItemを取得
        var DIScript = Object.GetComponent<DropItem>();

        // 落ちている武器を装備する
        // ※DropWeaponには持ってた武器
        DropWeapon = CEScript.GetWeapon(Object);

        // 武器交換の場合、生成する
        if(DropWeapon != null)
        {
            // ※getWeaponには手に入れる武器
		    GameObject getWeapon = Instantiate<GameObject>(Object);
            getWeapon.name = DIScript.WeaponInfo.name;

            // 武器情報の交換
            GunInfo getGIScript = getWeapon.GetComponent<GunInfo>();
            GunController dropGCScript = DropWeapon.GetComponent<GunController>();

            // 落ちている武器を削除
            Destroy(Object.gameObject);

            // 落ちてた武器の情報を保持
            var RankInfo            = getGIScript.gunRank;
            var TypeInfo            = getGIScript.gunType;
            var SlotInfo            = getGIScript.skillSlot;
            var MagazineInfo        = getGIScript.OneMagazine;
            var AmmoInfo            = getGIScript.MaxAmmo;
            var DamageInfo          = getGIScript.Damage;
            var shootIntervalInfo   = getGIScript.shootInterval;
            var reloadIntervalInfo  = getGIScript.reloadInterval;
            var PowerInfo           = getGIScript.bulletPower;
            var EXPInfo             = getGIScript.GunEXP;

            // 落とす武器の情報を入れる
            getGIScript.gunRank        = dropGCScript.gunRank;
            getGIScript.gunType        = dropGCScript.gunType;
            getGIScript.skillSlot      = dropGCScript.skillSlot;
            getGIScript.OneMagazine    = dropGCScript.OneMagazine;
            getGIScript.MaxAmmo        = dropGCScript.MaxAmmo;
            getGIScript.Damage         = dropGCScript.Damage;
            getGIScript.shootInterval  = dropGCScript.shootInterval;
            getGIScript.reloadInterval = dropGCScript.reloadInterval;
            getGIScript.bulletPower    = dropGCScript.bulletPower;
            getGIScript.GunEXP         = dropGCScript.GunEXP;

            // 持ってる武器のGunContollerに情報を入れる
            if(CEScript.ownGun == 1)
            {
                CEScript.GCPrimaryScript.gunRank        = RankInfo;
                CEScript.GCPrimaryScript.gunType        = TypeInfo;
                CEScript.GCPrimaryScript.skillSlot      = SlotInfo;
                CEScript.GCPrimaryScript.OneMagazine    = MagazineInfo;
                CEScript.GCPrimaryScript.MaxAmmo        = AmmoInfo;
                CEScript.GCPrimaryScript.Damage         = DamageInfo;
                CEScript.GCPrimaryScript.shootInterval  = shootIntervalInfo;
                CEScript.GCPrimaryScript.reloadInterval = reloadIntervalInfo;
                CEScript.GCPrimaryScript.bulletPower    = PowerInfo;
                CEScript.GCPrimaryScript.GunEXP         = EXPInfo;
            }
            else if(CEScript.ownGun == 2)
            {
                CEScript.GCSecondaryScript.gunRank        = RankInfo;
                CEScript.GCSecondaryScript.gunType        = TypeInfo;
                CEScript.GCSecondaryScript.skillSlot      = SlotInfo;
                CEScript.GCSecondaryScript.OneMagazine    = MagazineInfo;
                CEScript.GCSecondaryScript.MaxAmmo        = AmmoInfo;
                CEScript.GCSecondaryScript.Damage         = DamageInfo;
                CEScript.GCSecondaryScript.shootInterval  = shootIntervalInfo;
                CEScript.GCSecondaryScript.reloadInterval = reloadIntervalInfo;
                CEScript.GCSecondaryScript.bulletPower    = PowerInfo;
                CEScript.GCSecondaryScript.GunEXP         = EXPInfo;
            }
        } else {
            // 落ちている武器を削除
            Destroy(Object.gameObject);
        }     
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
