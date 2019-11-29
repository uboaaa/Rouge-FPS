//=======================================================================
// ���ׂ���A�E���Ȃǂ̓���p�X�N���v�g
// FPSController�ɕt����
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
        dropPrefab = CEScript.GetWeapon(DIScript.WeaponInfo);

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
