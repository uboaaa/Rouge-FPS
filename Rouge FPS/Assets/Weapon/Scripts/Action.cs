//=======================================================================
// ���ׂ���A�E���Ȃǂ̓���p�X�N���v�g
// FPSController�ɕt����
//=======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    private GameObject dropPrefab;      
    DropItem DIScript;
    ChangeEquip CEScript;                   // [ChangeEquip]�p�̕ϐ�
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
        // �󔠂��폜
        Destroy(Object.gameObject);

        // DropItem�𐶐�
        
    }

    void WeaponAction(GameObject Object)
    {
        // DropItemComponent���擾
        DIScript = Object.GetComponent<DropItem>();

        // �����Ă��镐��𑕔�����
        // ��dropPrefab�ɂ͗��Ƃ����킪����
        dropPrefab = CEScript.GetItem(DIScript.WeaponInfo);

        // ������������𐶐�����
        if(dropPrefab != null)
        {
            GameObject hadWeapon;      // �����Ă�������

		    hadWeapon = Instantiate<GameObject>(Object,Object.transform.position,Quaternion.identity);
            hadWeapon.name = DIScript.WeaponInfo.name;

            // GunInfo�̒��Ɏ����Ă�������̏�������
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

            // �����Ă镐��Ɏ����Ă������������
            DIScript.WeaponInfo = hadWeapon;
        }

        // �����Ă��镐����폜
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
