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
            // �󔠏���
            if(ObjectInfo.tag == "DropBox"){BoxAction(ObjectInfo);}
            
            // �����Ă�e�̏���
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
        // ��DropWeapon�ɂ͎����Ă�����
        DropWeapon = CEScript.GetWeapon(Object);

        // ��������̏ꍇ�A��������
        if(DropWeapon != null)
        {
            // ��getWeapon�ɂ͎�ɓ���镐��
		    GameObject getWeapon = Instantiate<GameObject>(Object);
            getWeapon.name = DIScript.WeaponInfo.name;

            // ������̌���
            GunInfo getGIScript = getWeapon.GetComponent<GunInfo>();
            GunController dropGCScript = DropWeapon.GetComponent<GunController>();

            // �����Ă��镐����폜
            Destroy(Object.gameObject);

            // �����Ă�����̏���ێ�
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

            // ���Ƃ�����̏�������
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

            // �����Ă镐���GunContoller�ɏ�������
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
