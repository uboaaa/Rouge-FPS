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
        // DropItemを取得
        var DIScript = Object.GetComponent<DropItem>();

        // 落ちている武器を装備する
        // ※DropWeaponには持ってた→落とす武器
        DropWeapon = CEScript.GetWeapon(Object);

        // 武器交換の場合、生成する
        if(DropWeapon != null)
        {
            // ※haveWeaponには落ちてた→持つ武器
		    GameObject haveWeapon = Instantiate<GameObject>(Object);
            haveWeapon.name = DIScript.WeaponInfo.name;

            // 落ちている武器を削除
            Destroy(Object.gameObject);

            // 武器情報の交換
            GunInfo dropGIScript = haveWeapon.GetComponent<GunInfo>();
            DropItem dropDIScript = haveWeapon.GetComponent<DropItem>();

            // 落ちてる武器に持っていた武器を入れる
            //dropDIScript.WeaponInfo = LGPScript.LoadGun(DropWeapon.name);

            // 持ってる武器の情報を保持
            var RankInfo            = dropGIScript.gunRank;
            var TypeInfo            = dropGIScript.gunType;
            var SlotInfo            = dropGIScript.skillSlot;
            var MagazineInfo        = dropGIScript.OneMagazine;
            var AmmoInfo            = dropGIScript.MaxAmmo;
            var DamageInfo          = dropGIScript.Damage;
            var shootIntervalInfo   = dropGIScript.shootInterval;
            var reloadIntervalInfo  = dropGIScript.reloadInterval;
            var PowerInfo           = dropGIScript.bulletPower;
            var EXPInfo             = dropGIScript.GunEXP;

            // 落とす武器に情報を入れる
            if(CEScript.ownGun == 1)
            {
                // 情報が入っていない※※※※※※※※※※※※※※※※※※※※※※※※※※※※※※※※※※※※※※
                dropGIScript.gunRank        = CEScript.GCPrimaryScript.gunRank;
                dropGIScript.gunType        = CEScript.GCPrimaryScript.gunType;
                dropGIScript.skillSlot      = CEScript.GCPrimaryScript.skillSlot;
                dropGIScript.OneMagazine    = CEScript.GCPrimaryScript.OneMagazine;
                dropGIScript.MaxAmmo        = CEScript.GCPrimaryScript.MaxAmmo;
                dropGIScript.Damage         = CEScript.GCPrimaryScript.Damage;
                dropGIScript.shootInterval  = CEScript.GCPrimaryScript.shootInterval;
                dropGIScript.reloadInterval = CEScript.GCPrimaryScript.reloadInterval;
                dropGIScript.bulletPower    = CEScript.GCPrimaryScript.bulletPower;
                dropGIScript.GunEXP         = CEScript.GCPrimaryScript.GunEXP;
            } 
            else if(CEScript.ownGun == 2)
            {
                dropGIScript.gunRank        = CEScript.GCSecondaryScript.gunRank;
                dropGIScript.gunType        = CEScript.GCSecondaryScript.gunType;
                dropGIScript.skillSlot      = CEScript.GCSecondaryScript.skillSlot;
                dropGIScript.OneMagazine    = CEScript.GCSecondaryScript.OneMagazine;
                dropGIScript.MaxAmmo        = CEScript.GCSecondaryScript.MaxAmmo;
                dropGIScript.Damage         = CEScript.GCSecondaryScript.Damage;
                dropGIScript.shootInterval  = CEScript.GCSecondaryScript.shootInterval;
                dropGIScript.reloadInterval = CEScript.GCSecondaryScript.reloadInterval;
                dropGIScript.bulletPower    = CEScript.GCSecondaryScript.bulletPower;
                dropGIScript.GunEXP         = CEScript.GCSecondaryScript.GunEXP;
            }
            // GunContollerに情報を入れる
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
