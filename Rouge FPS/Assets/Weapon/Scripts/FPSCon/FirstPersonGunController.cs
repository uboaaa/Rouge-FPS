using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FirstPersonGunController : MonoBehaviour
{
  
    public Text AmmoCheck;
    [SerializeField]
    private enum ShootMode { AUTO, SEMIAUTO }
    [SerializeField]
    private enum GunType { AssaultRifle, SubMachineGun, LightMachineGun, HandGun, SniperRifle }
    [SerializeField]
    private bool shootEnabled = true;
    [SerializeField]

    //ステータス設定
    private GunType gunType = GunType.AssaultRifle;
    [SerializeField]
    private ShootMode shootMode = ShootMode.AUTO;
    [SerializeField]
    private float OneMagazine = 0;
    [SerializeField]
    private float MaxAmmo = 0;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private float shootInterval = 0.15f;
    [SerializeField]
    private float shootRange = 50;
    [SerializeField]
    private Vector3 muzzleFlashScale;
    [SerializeField]
    private GameObject muzzleFlashPrefab;
    [SerializeField]
    private GameObject hitEffectPrefab;
    private bool shooting = false;
    private float ammo;
    private GameObject muzzleFlash;
    private GameObject hitEffect;
    [SerializeField]
    private GameObject gameObject;
    private float AmmoPlus;
    private float Ammo
    {
        set
        {
            ammo = Mathf.Clamp(value, 0, OneMagazine);
        }
        get
        {
            return ammo;
        }
    }
   
    void Start()
    {
        
        AmmoPlus=SkillManagement.GetAmmoPlus(0);
        OneMagazine = OneMagazine + (OneMagazine * AmmoPlus);
        InitGun();
        MaxAmmo = MaxAmmo + (MaxAmmo * AmmoPlus);
        
    }
    void Update()
    {
        AmmoCheck.text = Ammo + "/" + MaxAmmo;
        //リロード
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        if (shootEnabled & ammo > 0 & GetInput())
        {
            StartCoroutine(ShootTimer());
        }
    }

    //初期化
    void InitGun()
    {
        Ammo = OneMagazine;
    }

    void Reload()
    {
        
        if (MaxAmmo >= OneMagazine)
        {
            MaxAmmo = MaxAmmo - (OneMagazine - Ammo);
            Ammo = OneMagazine;
        }
        else {
            float NowAmmo;
            NowAmmo = OneMagazine - Ammo;
            Debug.Log(NowAmmo);
            if (NowAmmo > MaxAmmo)
            {
                Ammo = MaxAmmo+Ammo;
                MaxAmmo = 0;

            }
            else {
                MaxAmmo = MaxAmmo - NowAmmo;
                Ammo = Ammo + NowAmmo;
            }
        }

       
    }

    //セミオートかフルオートかの判定
    bool GetInput()
    {
        switch (shootMode)
        {
            case ShootMode.AUTO:
                return Input.GetMouseButton(0);
            case ShootMode.SEMIAUTO:
                return Input.GetMouseButtonDown(0);
        }
        return false;
    }
    IEnumerator ShootTimer()
    {
        if (!shooting)
        {
            shooting = true;
            //マズルフラッシュON
            if (muzzleFlashPrefab != null)
            {
                if (muzzleFlash != null)
                {
                    muzzleFlash.SetActive(true);
                }
                else
                {
                    muzzleFlash = Instantiate(muzzleFlashPrefab, transform.position, transform.rotation);
                    muzzleFlash.transform.SetParent(gameObject.transform);
                    muzzleFlash.transform.localScale = muzzleFlashScale;
                }
            }
            Shoot();
            yield return new WaitForSeconds(shootInterval);
            //マズルフラッシュOFF
            if (muzzleFlash != null)
            {
                muzzleFlash.SetActive(false);
            }
            //ヒットエフェクトOFF
            if (hitEffect != null)
            {
                if (hitEffect.activeSelf)
                {
                    hitEffect.SetActive(false);
                }
            }
            shooting = false;
        }
        else
        {
            yield return null;
        }
    }
    //レイ判定(弾処理)
    void Shoot()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        //レイを飛ばして、ヒットしたオブジェクトの情報を得る
        if (Physics.Raycast(ray, out hit, shootRange))
        {
            //ヒットエフェクトON
            if (hitEffectPrefab != null)
            {
                if (hitEffect != null)
                {
                    hitEffect.transform.position = hit.point;
                    hitEffect.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
                    hitEffect.SetActive(true);
                }
                else
                {
                    hitEffect = Instantiate(hitEffectPrefab, hit.point, Quaternion.identity);
                }
            }
            //★ここに敵へのダメージ処理などを追加
        }

    
            Ammo--;
        
            
        
    }
}