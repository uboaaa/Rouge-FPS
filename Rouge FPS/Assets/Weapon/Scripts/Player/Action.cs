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
    DropGun DGScript;
    ChangeEquip CEScript;                   // [ChangeEquip]�p�̕ϐ�
    LoadGunPrefab LGPScript;
    public bool actionFlg;
    GameObject ObjectInfo;
    GameObject tmp;
    public AiryUIAnimationManager UIEvent;
    bool isCalledOnce = false;

    void Start()
    {
        CEScript = GetComponent<ChangeEquip>();
        LGPScript = GetComponent<LoadGunPrefab>();
    }

    void Update()
    {
        // �E���Ƃ��̃e�L�X�g�p
        if(actionFlg && !isCalledOnce)
        {
            isCalledOnce = true;
            Debug.Log(UIEvent);
            UIEvent.ShowMenu();
        } else if(!actionFlg && isCalledOnce) {
            UIEvent.HideMenu();
            isCalledOnce = false;
        }

        if(Input.GetKeyDown(KeyCode.E) && actionFlg)
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
        var IBScript = Object.GetComponent<ItemBox>();
        IBScript.Open();
    }

    void WeaponAction(GameObject Object)
    {
        // DropItem���擾
        var DIScript = Object.GetComponent<DropGun>();

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
            var RankInfo            = dropGCScript.gunRank;          // ���탉���N
            var TypeInfo            = dropGCScript.gunType;          // ������
            var SlotInfo            = dropGCScript.skillSlot;        // �X�L���X���b�g��
            var MaxMagazineInfo     = dropGCScript.MagazineSize;     // �P�}�K�W���̃T�C�Y
            var remMagazineInfo     = dropGCScript.Ammo;             // �}�K�W�����̎c�e��
            var MaxAmmoInfo         = dropGCScript.AmmoSize;         // �\���e���T�C�Y
            var remAmmoInfo         = dropGCScript.remAmmo;          // �\���e��
            var DamageInfo          = dropGCScript.Damage;           // �Η�
            var shootIntervalInfo   = dropGCScript.shootInterval;    // �ˌ��Ԋu
            var reloadIntervalInfo  = dropGCScript.reloadInterval;   // �����[�h�X�s�[�h
            var PowerInfo           = dropGCScript.bulletPower;      // �e�̔�΂���
            var EXPInfo             = dropGCScript.GunEXP;           // �e�̌o���l

            // ���Ƃ�����ɏ�������(dropGCScript <= getGIScript)
            CEScript.ChangeGunConInfo(dropGCScript,getGIScript,1);

            // �E������ɏ�������
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
