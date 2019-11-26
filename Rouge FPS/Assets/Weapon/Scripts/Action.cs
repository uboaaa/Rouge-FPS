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
    void Start()
    {
        CEScript = GetComponent<ChangeEquip>();

        // ランダムで取ってくる
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && actionFlg)
        {
            if(ObjectInfo.tag == "DropBox"){BoxAction(ObjectInfo);}
            
            if(ObjectInfo.tag == "DropGun"){WeaponAction(ObjectInfo);}

            actionFlg = false;
        }
        Debug.Log(dropPrefab);
        Debug.Log(actionFlg);
    }

    void BoxAction(GameObject Object)
    {
        // 宝箱を削除
        Destroy(Object.gameObject);

        // DropItemを生成
        
    }

    void WeaponAction(GameObject Object)
    {
        GameObject dropWeapon;
        // DropItemComponentを取得
        DIScript = Object.GetComponent<DropItem>();

        // 落ちている武器を装備する
        dropPrefab = CEScript.GetItem(DIScript.WeaponInfo);

        // 交換した武器を生成する
        if(dropPrefab != null)
        {
            dropWeapon = Instantiate<GameObject>(Object,Object.transform.position,Quaternion.identity);
            // GunInfo  の中に情報を入れなおす
            // DropItemのWeaponInfoにdropPrefabを入れる
        }

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
