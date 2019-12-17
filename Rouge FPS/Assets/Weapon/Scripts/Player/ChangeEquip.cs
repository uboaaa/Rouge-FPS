using UnityEngine;
using System.Collections;

public class ChangeEquip : MonoBehaviour
{
    // インスペクター関係============================================
    public GameObject PrimaryWeapon;          // プライマリ武器の情報
    public GameObject SecondaryWeapon;        // セカンダリ武器の情報

    // パラメーター関係==============================================
    [HideInInspector] public int ownGun;      // 0:持ってない 1:プライマリ 2:セカンダリ
    [HideInInspector] public bool activeFlg;  // 行動中か  
    GameObject child;

     // スクリプト関係================================================
    public GunController GCPrimaryScript{get;set;}                   // [GunController]用の変数
    public GunController GCSecondaryScript{get;set;}                 // [GunController]用の変数

    void Start()
    {
        // 子オブジェクトを取得（FirstPersonCharacter）
        child = transform.FindChild("FirstPersonCharacter").gameObject;

        ownGun = 0;
        if(PrimaryWeapon != null)
        { 
            // 拾った武器をFirstPersonCharacterの直下に生成する
            GameObject tmp = Instantiate(PrimaryWeapon,child.transform,false);
            
            // 生成したものをプライマリにセットする
            PrimaryWeapon = tmp;

            PrimaryWeapon.SetActive(true);
            GCPrimaryScript = PrimaryWeapon.GetComponent<GunController>();
        }

        if(SecondaryWeapon != null)
        {
            // 拾った武器をFirstPersonCharacterの直下に生成する
            GameObject tmp = Instantiate(SecondaryWeapon,child.transform,false);
            
            // 生成したものをプライマリにセットする
            PrimaryWeapon = tmp;

            SecondaryWeapon.SetActive(false);
            GCSecondaryScript = SecondaryWeapon.GetComponent<GunController>();
        }

    }

    void Update()
    {
        //Debug.Log(ownGun);
        if (Input.GetKeyDown(KeyCode.E) && !activeFlg && SecondaryWeapon != null)
        {
            GCPrimaryScript.shooting = false;
            GCSecondaryScript.shooting = false;
            activeFlg = true;
            ChangeWeapon();
        }
    }

    void ChangeWeapon()
    {
        if (SecondaryWeapon.activeSelf)
        {
            ownGun = 1;
            PrimaryWeapon.SetActive(true);
            SecondaryWeapon.SetActive(false);
        }
        else
        {
            ownGun = 2;
            PrimaryWeapon.SetActive(false);
            SecondaryWeapon.SetActive(true);
        }

    }

    // 落ちている武器をセットする
    public GameObject GetWeapon(GameObject Object)
    {
        GameObject dropInfo;   // 情報保持用

        // DropItemを取得
        var DIScript = Object.GetComponent<DropGun>().WeaponInfo;
        // GunInfoを取得
        var GIScript = Object.GetComponent<GunInfo>();

        // 何も持っていないとき
        if(ownGun == 0)
        {
            // プライマリなので１に変更
            ownGun = 1;

            // 落ちている武器を入れる
            PrimaryWeapon = DIScript;

            // 拾った武器をFirstPersonCharacterの直下に生成する
            PrimaryWeapon = Instantiate(PrimaryWeapon,child.transform,false);

            // 表示
            PrimaryWeapon.SetActive(true);

            // GunContollerを読み込む
            GCPrimaryScript = PrimaryWeapon.GetComponent<GunController>();

            // GunInfoから情報を入れる
            ChangeGunConInfo(GCPrimaryScript,GIScript,1);

            return null;
        }

        // プライマリ武器しか持っていないとき
        else if(ownGun == 1 && SecondaryWeapon == null)
        {
            // セカンダリなので２にする
            ownGun = 2;

            // 落ちている武器を入れる
            SecondaryWeapon = DIScript;

            // 拾った武器をFirstPersonCharacterの直下に生成する
            SecondaryWeapon = Instantiate(SecondaryWeapon,child.transform,false);

            // セカンダリを表示
            PrimaryWeapon.SetActive(false);
            SecondaryWeapon.SetActive(true);

            // GunContollerを読み込む
            GCSecondaryScript = SecondaryWeapon.GetComponent<GunController>();

            // GunInfoから情報を入れる
            ChangeGunConInfo(GCSecondaryScript,GIScript,1);
            
            return null;
        }

        // プライマリ武器と交換する
        else if(ownGun == 1 && PrimaryWeapon != null && SecondaryWeapon != null)
        {
            // 一旦、非表示
            PrimaryWeapon.SetActive(false);

            // 情報保持用変数に武器の情報を入れる
            dropInfo = PrimaryWeapon;
        
            // 武器をヒエラルキーから削除
            Destroy(PrimaryWeapon);

            // 落ちている武器を入れる
            PrimaryWeapon = DIScript;

            // 拾った武器をFirstPersonCharacterの直下に生成する
            PrimaryWeapon = Instantiate(PrimaryWeapon,child.transform,false);

            // GunContollerを読み込む
            GCPrimaryScript = PrimaryWeapon.GetComponent<GunController>();

            // GunInfoから情報を入れる
            ChangeGunConInfo(GCPrimaryScript,GIScript,1);

            // 表示
            PrimaryWeapon.SetActive(true);

            return dropInfo;
        }

        // セカンダリ武器と交換する
        else if(ownGun == 2 && PrimaryWeapon != null && SecondaryWeapon != null)
        {
            // 一旦、非表示
            SecondaryWeapon.SetActive(false);
            
            // 情報保持用変数に武器の情報を入れる
            dropInfo = SecondaryWeapon;

            // 武器をヒエラルキーから削除
            Destroy(SecondaryWeapon);

            // 落ちている武器を入れる
            SecondaryWeapon = DIScript;

            // 拾った武器をFirstPersonCharacterの直下に生成する
            SecondaryWeapon = Instantiate(SecondaryWeapon,child.transform,false);

            // GunContollerを読み込む
            GCSecondaryScript = SecondaryWeapon.GetComponent<GunController>();

            // GunInfoから情報を入れる
            ChangeGunConInfo(GCSecondaryScript,GIScript,1);

            // 表示
            SecondaryWeapon.SetActive(true);
            

            return dropInfo;
        }
        
        return null;
    }

    // GunInfoから情報を入れる
    public void ChangeGunConInfo(GunController Guncon,GunInfo Guninfo,int mode)
    {
        // GunContoller　←　GunInfo
        if(mode == 1)
        {
            Guncon.gunRank        = Guninfo.gunRank;
            Guncon.gunType        = Guninfo.gunType;
            Guncon.skillSlot      = Guninfo.skillSlot;
            Guncon.MagazineSize   = Guninfo.MagazineSize;
            Guncon.Ammo           = Guninfo.remMagazine;
            Guncon.remAmmo        = Guninfo.remAmmo;
            Guncon.Damage         = Guninfo.Damage;
            Guncon.shootInterval  = Guninfo.shootInterval;
            Guncon.reloadInterval = Guninfo.reloadInterval;
            Guncon.bulletPower    = Guninfo.bulletPower;
            Guncon.GunEXP         = Guninfo.GunEXP;
        }
        
        // GunInfo　←　GunContoller
        if(mode == 2)
        {
            Guninfo.gunRank        = Guncon.gunRank;
            Guninfo.gunType        = Guncon.gunType;
            Guninfo.skillSlot      = Guncon.skillSlot;
            Guninfo.MagazineSize   = Guncon.MagazineSize;
            Guninfo.remMagazine    = Guncon.Ammo;
            Guninfo.remAmmo        = Guncon.remAmmo;
            Guninfo.Damage         = Guncon.Damage;
            Guninfo.shootInterval  = Guncon.shootInterval;
            Guninfo.reloadInterval = Guncon.reloadInterval;
            Guninfo.bulletPower    = Guncon.bulletPower;
            Guninfo.GunEXP         = Guncon.GunEXP;
        }
    }

    // 持っている武器を返す関数
    public GameObject nowWeapon()
    {
        if(ownGun == 1)
        {
            return PrimaryWeapon;
        }
        else if(ownGun == 2)
        {
            return SecondaryWeapon;
        }

        Debug.Log("何も持っていない");
        return null;
    }
}
  