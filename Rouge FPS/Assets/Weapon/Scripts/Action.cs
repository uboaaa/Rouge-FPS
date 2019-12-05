//=======================================================================
// ���ׂ���A�E���Ȃǂ̓���p�X�N���v�g
// FPSController�ɕt����
//=======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    private GameObject DropWeapon;      
    DropItem DIScript;
    ChangeEquip CEScript;                   // [ChangeEquip]�p�̕ϐ�
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
        // �󔠂��폜
        Destroy(Object.gameObject);

        // DropItem�𐶐�
        
    }

    void WeaponAction(GameObject Object)
    {
        // DropItem���擾
        var DIScript = Object.GetComponent<DropItem>();

        // �����Ă��镐��𑕔�����
        // ��DropWeapon�ɂ͎����Ă������Ƃ�����
        DropWeapon = CEScript.GetWeapon(Object);

        // ��������̏ꍇ�A��������
        if(DropWeapon != null)
        {
            // ��haveWeapon�ɂ͗����Ă���������
		    GameObject haveWeapon = Instantiate<GameObject>(Object);
            haveWeapon.name = DIScript.WeaponInfo.name;

            // �����Ă��镐����폜
            Destroy(Object.gameObject);

            // ������̌���
            GunInfo dropGIScript = haveWeapon.GetComponent<GunInfo>();
            DropItem dropDIScript = haveWeapon.GetComponent<DropItem>();

            // �����Ă镐��Ɏ����Ă������������
            //dropDIScript.WeaponInfo = LGPScript.LoadGun(DropWeapon.name);

            // �����Ă镐��̏���ێ�
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

            // ���Ƃ�����ɏ�������
            if(CEScript.ownGun == 1)
            {
                // ��񂪓����Ă��Ȃ�����������������������������������������������������������������������������
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
            // GunContoller�ɏ�������
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
            // �����Ă��镐����폜
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
