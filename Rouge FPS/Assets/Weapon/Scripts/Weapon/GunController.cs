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
    // インスペクター関係============================================
    // 種類
    [SerializeField] public enum ShootMode { AUTO, SEMIAUTO }                       // 単発が連射か
    [SerializeField] public ShootMode  shootMode = ShootMode.AUTO;                  // 武器の発射モード
    [SerializeField] public GunInfo.GunType    gunType     = GunInfo.GunType.AssaultRifle;  // 種類情報
    [SerializeField] public GunInfo.GunRank    gunRank     = GunInfo.GunRank.Rank1;         // ランク情報
    [SerializeField] public int        skillSlot;                              // スキルスロット数
    [SerializeField] public int        MagazineSize;                           // マガジンサイズ
    [HideInInspector]public int        AmmoSize;                               // 予備弾数MAX
    [SerializeField] public int        remAmmo;                                // 残弾数
    [SerializeField] public int        Damage;                                 // 火力
    [SerializeField] public float      shootInterval;                          // 次発射までの間の時間
    [SerializeField] public float      reloadInterval;                         // リロード終わりまでの時間
	[SerializeField] public float      bulletPower;                            // 弾を飛ばす力
    [SerializeField] Transform  muzzle;                                             // マズル取得用
    [SerializeField] GameObject bulletPrefab;                                       // 弾のPrefab
    [SerializeField] Vector3    bulletScale = new Vector3(1.0f,1.0f,1.0f);          // 弾の大きさ変更用
    [SerializeField] GameObject muzzleFlashPrefab;                                  // マズルフラッシュのPrefab
    [SerializeField] Vector3    muzzleFlashScale = new Vector3(1.0f,1.0f,1.0f);     // マズルフラッシュの大きさ変更用
    [SerializeField] float cameraShakePow;                                          // カメラ揺らし用

    // パラメーター関係==============================================
    public float GunEXP;                                    // 経験値
    [HideInInspector] public bool shootEnabled = true;      // 撃てる状態か判定用
    [HideInInspector] public bool shooting = false;         // 射撃中か判定用
    bool        reloading = false;                          // リロード中か判定用
    public bool        equipping{get;private set;}          // 装備切り替え中か判定用
    int         ammo;                                       // マガジンに入っている弾の数

    public int Ammo
    {
        get { return ammo;}
        set { ammo = Mathf.Clamp(value, 0, MagazineSize);}
    }
    // シェーダパラメータ管理用
    int propID_h = 0;   // Hue
    int propID_s = 0;   // Saturation
    int propID_b = 0;   // Brightness
    int propID_c = 0;   // Contrast

    // スクリプト関係================================================
    ChangeEquip CEScript;                   // [ChangeEquip]用の変数
    CameraShake cameraScript;               // [CameraShake]用の変数
    Animator animator;                      // [Animator]用の変数
    GunInfo GIScript;                       // [GunInfo]用の変数
    muzzleShot MSScript;                    // [muzzleShot]用の変数
    AnimatorStateInfo animatorInfo;         // Animatorの情報を入れる
    GameObject FPSCon;
    Material material;                      // マテリアル

    public AudioClip shotSound;             // 撃つときのサウンド
    public AudioClip noShotSound;           // 弾がないときのサウンド
    public AudioClip reloadStartSound;      // リロード始めのサウンド
    public AudioClip reloadSound;           // リロード終わりのサウンド
    AudioSource audioSource;                // オーディオ用

    
    void Start()
    {
        FPSCon = GameObject.Find("FPSController");
        CEScript = FPSCon.GetComponent<ChangeEquip>();

        animator = GetComponent<Animator>();

        // 上の階層のオブジェクトにアタッチしているスクリプトを参照する
        cameraScript = GetComponentInParent<CameraShake>();

        // マテリアル
        material = this.gameObject.GetComponent<SpriteRenderer>().material;

        // マテリアルシェーダの設定
        propID_h = Shader.PropertyToID("_Hue");
        propID_s = Shader.PropertyToID("_Saturation");
        propID_b = Shader.PropertyToID("_Brightness");
        propID_c = Shader.PropertyToID("_Contrast");

        // 武器ごとのAUDIOを取得
        audioSource = GetComponent<AudioSource>();

        // ランクごと武器種ごとにダメージを変化
        if(gunRank == GunInfo.GunRank.Rank1)
        {
            // ランク１はそのまま
        }
        else if(gunRank == GunInfo.GunRank.Rank2)
        {
            Damage = Damage * 3;
        }
        else if(gunRank == GunInfo.GunRank.Rank3)
        {
            Damage = Damage * 5;
        }
    }

    void Update()
    {
        // ポーズ中動作しないようにする
        if(!PauseScript.pause()){}
        if(!SkillManagement.GetTimeStop()){}

        // 武器レベルに応じてのカラーにする
        switch(gunRank)
        {
            case GunInfo.GunRank.Rank1:
                material.SetFloat(propID_h, 0.0f);
                material.SetFloat(propID_s, 0.5f);
                material.SetFloat(propID_b, 0.5f);
                material.SetFloat(propID_c, 0.5f);
                break;

            case GunInfo.GunRank.Rank2:
                material.SetFloat(propID_h, 0.1f);
                material.SetFloat(propID_s, 0.0f);
                material.SetFloat(propID_b, 0.7f);
                material.SetFloat(propID_c, 0.9f);
                break;

            case GunInfo.GunRank.Rank3:
                material.SetFloat(propID_h, 0.078f);
                material.SetFloat(propID_s, 1.0f);
                material.SetFloat(propID_b, 0.8f);
                material.SetFloat(propID_c, 1.0f);
                break;

            default:
                Debug.Log("マテリアルが付いていない");
                break;
        }

        if(remAmmo > AmmoSize)
        {
            remAmmo = AmmoSize;
        }

        animatorInfo = animator.GetCurrentAnimatorStateInfo(0);

        // アニメーションが”Get”状態の時、フラグを受け取る
        if(animatorInfo.shortNameHash == Animator.StringToHash("Get"))
        {
            // 武器交換中
            equipping = true;
            CEScript.activeFlg = equipping;
            shooting = false;
        } else if(animatorInfo.shortNameHash == Animator.StringToHash("Reload")) 
        {
            equipping = true;
            CEScript.activeFlg = equipping;
        } else if(animatorInfo.shortNameHash == Animator.StringToHash("Shot")) 
        {
            equipping = true;
            CEScript.activeFlg = equipping;
        } else {
            CEScript.activeFlg = equipping;
            equipping = false;
        }

        // アニメーションが終了
        if(animatorInfo.normalizedTime > 1.0f)
        {
            // 単発武器の射撃
            if(GetInput(shootMode) && Ammo > 0 && !shooting  && !reloading && shootMode == ShootMode.SEMIAUTO && !equipping)
            {
                StartCoroutine(ShootTimer());
                animator.SetBool("ShootFlg",true);
            } else if(Input.GetMouseButtonDown(0) && Ammo == 0) {
                // 射撃音を鳴らす
                audioSource.PlayOneShot(noShotSound);
            }

            // リロード
            if(Input.GetKeyDown(KeyCode.R) && remAmmo > 0 && Ammo != MagazineSize && !shooting  && !reloading && !equipping)
            {
                reloading = true;
                animator.SetBool("ReloadFlg",true);
                StartCoroutine(ReloadTimer());
            }
        } else {
            animator.SetBool("ShootFlg",false);
            animator.SetBool("ReloadFlg",false);
        }

        // 連射武器の射撃
        if(GetInput(ShootMode.AUTO) && Ammo > 0 && !shooting && !reloading && shootMode == ShootMode.AUTO && !equipping)
        {
            StartCoroutine(ShootTimer());
            animator.SetBool("ShootFlg",true);
        
            // 弾が0発になったらアニメーション終了
            if(Ammo == 0)
            {
                animator.SetBool("ShootFlg",false);
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            animator.SetBool("ShootFlg",false);
        }

        cameraScript.Shake(cameraShakePow,shooting);  // 画面を揺らす
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

    // 射撃処理
    IEnumerator ShootTimer()
    {
        if (!shooting)
        {
            // 射撃音を鳴らす
            audioSource.PlayOneShot(shotSound);

            // 射撃中は追加で撃てないようにする
            shooting = true;
            shootEnabled = false;

            if (muzzleFlashPrefab != null)
            {
                // マズルフラッシュの生成
                GameObject  muzzleFlash = Instantiate<GameObject>(muzzleFlashPrefab,muzzle.position,muzzle.rotation);
                //muzzleFlash.transform.parent = this.transform;
                muzzleFlash.transform.localScale = muzzleFlashScale;
                Destroy(muzzleFlash,1.0f);
            }

            if (bulletPrefab != null)
            {
                // 弾の生成
		        GameObject bullet = Instantiate<GameObject>(bulletPrefab, muzzle.position, muzzle.rotation);
                bullet.transform.localScale = bulletScale;
		        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletPower);
		        Destroy(bullet, 10.0f);

                // ショットガンの場合
                // 子オブジェクトにアタッチしているスクリプトを参照する
                if(GunInfo.GunType.ShotGun == gunType)
                for(int i = 1; i < 5;i++)
                {
                    GameObject childObject = transform.Find("muzzle" + i).gameObject;
                    MSScript = childObject.GetComponent<muzzleShot>();
                    MSScript.Shot();
                }
            }

             // 弾を減らす
            ammo--;

            // 連射速度の調整
            yield return new WaitForSeconds(shootInterval);

            shooting = false;
            shootEnabled = true;
        } else {
            yield return null;
        }
    }

    // リロード処理
    IEnumerator ReloadTimer()
    {
        if(shootEnabled)
        {
            // リロード音を鳴らす
            audioSource.PlayOneShot(reloadStartSound);

            // 連射速度の調整
            yield return new WaitForSeconds(reloadInterval);

            audioSource.Stop(); 
            // リロード音を鳴らす
            audioSource.PlayOneShot(reloadSound);
            
            // リロードできる弾の数なら
            if (remAmmo >= MagazineSize)
            {
                remAmmo = remAmmo - (MagazineSize - Ammo);
                Ammo = MagazineSize;
                reloading = false;
            } else {
                int NowAmmo = MagazineSize - Ammo;
                if (NowAmmo > remAmmo)
                {
                    Ammo = remAmmo+Ammo;
                    remAmmo = 0;
                } else {
                    remAmmo = remAmmo - NowAmmo;
                    Ammo = Ammo + NowAmmo;
                }

                reloading = false;
            }
        } else {
            yield return null;
        }
    }
}