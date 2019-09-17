using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FirstPersonGunController : MonoBehaviour
{

    public Text AmmoCheck;
    [SerializeField]
    public enum ShootMode { AUTO, SEMIAUTO }
    public enum GunType { AssaultRifle, SubMachineGun, LightMachineGun, HandGun, SniperRifle }
    public bool shootEnabled = true;
    [SerializeField]

    //ステータス設定
    GunType gunType = GunType.AssaultRifle;
    [SerializeField]
    ShootMode shootMode = ShootMode.AUTO;
    [SerializeField]
    int OneMagazine = 0;
    [SerializeField]
    int MaxAmmo = 0;
    [SerializeField]
    int damage = 1;
    [SerializeField]
    float shootInterval = 0.15f;
    [SerializeField]
    float shootRange = 50;
    [SerializeField]
    Vector3 muzzleFlashScale;
    [SerializeField]
    GameObject muzzleFlashPrefab;
    [SerializeField]
    GameObject hitEffectPrefab;
    bool shooting = false;
    int ammo;
    GameObject muzzleFlash;
    GameObject hitEffect;
    public int Ammo
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
        InitGun();
     
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
            int NowAmmo;
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