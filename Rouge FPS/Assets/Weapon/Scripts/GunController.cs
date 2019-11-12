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
    public enum GunType { AssaultRifle, SubMachineGun, LightMachineGun, HandGun, RocketLauncher, ShotGun, LaserGun, FlameThrower }
    public enum GunRank { Rank1, Rank2, Rank3 }
    [SerializeField] public enum ShootMode { AUTO, SEMIAUTO }
    [SerializeField] public ShootMode  shootMode = ShootMode.AUTO;
    [SerializeField] GunType    gunType     = GunType.AssaultRifle;
    [SerializeField] GunRank    gunRank     = GunRank.Rank1;
    public float GunEXP;
    [SerializeField] int        skillSlot   = 1;
    [SerializeField] int        OneMagazine = 0;
    public int oneMagazine
    {
        get{return OneMagazine;}
    }
    [SerializeField] int        MaxAmmo = 0;
    public int maxAmmo
    {
        get{return MaxAmmo;}
    }
    [SerializeField] int        Damage = 1;
    public int damage
    {
        get{return Damage;}
    }
    [SerializeField] float      shootInterval = 0.15f;
	[SerializeField] float      bulletPower = 100.0f;
    [SerializeField] Transform  muzzle;
    // 弾情報
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Vector3    bulletScale = new Vector3(1.0f,1.0f,1.0f);
    // マズル情報
    [SerializeField] GameObject muzzleFlashPrefab;
    [SerializeField] Vector3    muzzleFlashScale = new Vector3(1.0f,1.0f,1.0f);
    GameObject  bullet;
    public bool shootEnabled = true;
    bool        shooting = false;
    bool        reloading = false;
    int         ammo;
    public int Ammo
    {
        get { return ammo;}
        set { ammo = Mathf.Clamp(value, 0, OneMagazine);}
    }
    public Text AmmoCheck;
    GameObject  muzzleFlash;
    GameObject  hitEffect;
    CameraShake shakeScript;
    [SerializeField] float shakePow;
    Animator animator;
    AnimatorStateInfo animatorInfo;
   
    void Start()
    {
        InitGun();
        animator = GetComponent<Animator>();

        // 上の階層のオブジェクトにアタッチしているスクリプトを参照する
        shakeScript = GetComponentInParent<CameraShake>();
    }
    void Update()
    {   
        AmmoCheck.text = Ammo + "/" + MaxAmmo;

        animatorInfo = animator.GetCurrentAnimatorStateInfo(0);
        // アニメーションが終了
        if(animatorInfo.normalizedTime > 1.0f)
        {
            // 単発武器の射撃
            if(GetInput(shootMode) && Ammo > 0 && !shooting  && !reloading && shootMode == ShootMode.SEMIAUTO)
            {
                StartCoroutine(ShootTimer());
                animator.SetBool("ShootFlg",true);
            }

            // リロード
            if(Input.GetKeyDown(KeyCode.R) && maxAmmo > 0 && Ammo != oneMagazine && !shooting  && !reloading)
            {
                reloading = true;
                Invoke("Reload",0.5f);
                animator.SetBool("ReloadFlg",true);
            }
        }
        else
        {
            animator.SetBool("ShootFlg",false);
            animator.SetBool("ReloadFlg",false);
        }

        // 連射武器の射撃
        if(GetInput(ShootMode.AUTO) && Ammo > 0 && !reloading && shootMode == ShootMode.AUTO)
        {
            StartCoroutine(ShootTimer());
            animator.SetBool("ShootFlg",true);
        }

        // 射撃中画面を揺らす
        shakeScript.Shake(shakePow,shooting);

        Debug.Log("animatorInfo" + animatorInfo.normalizedTime);
    }

    //初期化
    void InitGun()
    {
        Ammo = OneMagazine;
    }

    // リロード処理
    void Reload()
    {
        if(shootEnabled)
        {
            if (MaxAmmo >= OneMagazine)
            {
                MaxAmmo = MaxAmmo - (OneMagazine - Ammo);
                Ammo = OneMagazine;
                reloading = false;
            }
            else 
            {
                int NowAmmo;
                NowAmmo = OneMagazine - Ammo;
                if (NowAmmo > MaxAmmo)
                {
                    Ammo = MaxAmmo+Ammo;
                    MaxAmmo = 0;
                }
                else 
                {
                    MaxAmmo = MaxAmmo - NowAmmo;
                    Ammo = Ammo + NowAmmo;
                }

                reloading = false;
            }
        }
    }

    // セミオートかフルオートかの判定
    public bool GetInput(ShootMode shootMode)
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
            // 射撃中は追加で撃てないようにする
            shooting = true;
            shootEnabled = false;

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
                bullet.transform.localScale = bulletScale;
		        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletPower);
		        Destroy(bullet, 5.0f);
            }

             // 弾を減らす
            ammo--;

            // 連射速度の調整
            yield return new WaitForSeconds(shootInterval);

            shooting = false;
            shootEnabled = true;
        }
        else
        {
            yield return null;
        }
    }
}