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
    DropGun DGScript;
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
        var IBScript = Object.GetComponent<ItemBox>();
        IBScript.Open();
    }

    void WeaponAction(GameObject Object)
    {
        // DropItemを取得
        var DIScript = Object.GetComponent<DropGun>();

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

            // 持ってた武器の情報を保持
            var RankInfo            = dropGCScript.gunRank;          // 武器ランク
            var TypeInfo            = dropGCScript.gunType;          // 武器種類
            var SlotInfo            = dropGCScript.skillSlot;        // スキルスロット数
            var MaxMagazineInfo     = dropGCScript.MagazineSize;     // １マガジンのサイズ
            var remMagazineInfo     = dropGCScript.Ammo;             // マガジン内の残弾数
            var MaxAmmoInfo         = dropGCScript.AmmoSize;         // 予備弾数サイズ
            var remAmmoInfo         = dropGCScript.remAmmo;          // 予備弾数
            var DamageInfo          = dropGCScript.Damage;           // 火力
            var shootIntervalInfo   = dropGCScript.shootInterval;    // 射撃間隔
            var reloadIntervalInfo  = dropGCScript.reloadInterval;   // リロードスピード
            var PowerInfo           = dropGCScript.bulletPower;      // 弾の飛ばす力
            var EXPInfo             = dropGCScript.GunEXP;           // 銃の経験値

            // 落とす武器に情報を入れる(dropGCScript <= getGIScript)
            CEScript.ChangeGunConInfo(dropGCScript,getGIScript,1);

            // 拾う武器に情報を入れる
            getGIScript.gunRank        = RankInfo;
            getGIScript.gunType        = TypeInfo;
            getGIScript.skillSlot      = SlotInfo;
            getGIScript.MagazineSize   = MaxMagazineInfo;
            getGIScript.remMagazine    = remMagazineInfo;
            getGIScript.ammoMax        = MaxAmmoInfo;
            getGIScript.remAmmo        = remAmmoInfo;
            getGIScript.Damage         = DamageInfo;
            getGIScript.shootInterval  = shootIntervalInfo;
            getGIScript.reloadInterval = reloadIntervalInfo;
            getGIScript.bulletPower    = PowerInfo;
            getGIScript.GunEXP         = EXPInfo;
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
