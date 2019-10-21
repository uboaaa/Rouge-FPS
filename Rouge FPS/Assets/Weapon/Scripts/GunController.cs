//===========================================================================
// 銃用スクリプト
// （銃の情報を入れる）
//===========================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GunController : MonoBehaviour
{
    [SerializeField] public enum ShootMode { AUTO, SEMIAUTO }
    public enum GunType { AssaultRifle, SubMachineGun, LightMachineGun, HandGun, RocketLauncher, ShotGun, LaserGun, FlameThrower }
    public enum GunRank { Rank1, Rank2, Rank3 }
    public bool shootEnabled = true;
    [SerializeField] GunType    gunType     = GunType.AssaultRifle;
    [SerializeField] ShootMode  shootMode   = ShootMode.AUTO;
    [SerializeField] GunRank    gunRank     = GunRank.Rank1;
    [SerializeField] int        skillSlot   = 1;
    [SerializeField] int OneMagazine = 0;
    [SerializeField] public int MaxAmmo = 0;
    [SerializeField] int damage = 1;
    [SerializeField] float shootInterval = 0.15f;
    [SerializeField] Transform muzzle;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject muzzleFlashPrefab;
    [SerializeField] Vector3 muzzleFlashScale = new Vector3(1.0f,1.0f,1.0f);
	[SerializeField] float bulletPower = 100.0f;
    GameObject bullet;
    bool shooting = false;
    int ammo;
    public Text AmmoCheck;
    GameObject muzzleFlash;
    GameObject hitEffect;
    GunAnimation gunAnim;
    
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

        gunAnim = GetComponent<GunAnimation>();
    }
    void Update()
    {   
        AmmoCheck.text = Ammo + "/" + MaxAmmo;
        
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

    // リロード処理
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

    // セミオートかフルオートかの判定
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

            if (muzzleFlashPrefab != null)
            {
                // マズルフラッシュの生成
                muzzleFlash = Instantiate<GameObject>(muzzleFlashPrefab,muzzle.position,muzzle.rotation);
                muzzleFlash.transform.localScale = muzzleFlashScale;
                Destroy(muzzleFlash,1.0f);
            }

            if (bulletPrefab != null)
            {
                // 弾の生成
		        bullet = Instantiate<GameObject>(bulletPrefab, muzzle.position, muzzle.rotation);
		        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletPower);
		        Destroy(bullet, 5.0f);
            }

             // 弾を減らす
            ammo--;

            // 連射速度の調整
            yield return new WaitForSeconds(shootInterval);

            shooting = false;
        }
        else
        {
            yield return null;
        }
    }
}