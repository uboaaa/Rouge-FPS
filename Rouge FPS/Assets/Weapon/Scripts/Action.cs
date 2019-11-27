//=======================================================================
// 調べたり、拾うなどの動作用スクリプト
// FPSControllerに付ける
//=======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
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
        dropPrefab = CEScript.GetItem(DIScript.WeaponInfo);

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

            Debug.Log(GIScript);

            GIScript.gunRank = GunInfo.GunRank.Rank1;
            GIScript.gunType = GunInfo.GunType.HandGun;
            // GIScript.skillSlot = ;
            // GIScript.OneMagazine = ;
            // GIScript.MaxAmmo = ;
            // GIScript.Damage = ;
            // GIScript.shootInterval = ;
            // GIScript.reloadInterval = ;
            // GIScript.bulletPower = ;
            // GIScript.GunEXP = ;

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
