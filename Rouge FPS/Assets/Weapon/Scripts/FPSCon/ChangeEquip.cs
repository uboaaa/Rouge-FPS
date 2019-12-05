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
    public GunController GCPrimaryScript{get;private set;}                   // [GunController]用の変数
    public GunController GCSecondaryScript{get;private set;}                 // [GunController]用の変数

    void Start()
    {
        // 子オブジェクトを取得（FirstPersonCharacter）
        child = transform.FindChild("FirstPersonCharacter").gameObject;

        ownGun = 0;
        if(PrimaryWeapon != null)
        {
            PrimaryWeapon.SetActive(true);
            GCPrimaryScript = PrimaryWeapon.GetComponent<GunController>();
        }

        if(SecondaryWeapon != null)
        {
            SecondaryWeapon.SetActive(false);
            GCSecondaryScript = SecondaryWeapon.GetComponent<GunController>();
        }

    }

    void Update()
    {
        // if(PrimaryWeapon != null){Debug.Log("プライマリ座標：" + PrimaryWeapon.transform.position);}
        // Debug.Log(ownGun);
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
        var DIScript = Object.GetComponent<DropItem>().WeaponInfo;
        // GunInfoを取得
        var GIScript = Object.GetComponent<GunInfo>();

        // 何も持っていないとき
        if(ownGun == 0)
        {
            // 落ちている武器を入れる
            PrimaryWeapon = DIScript;

            // 拾った武器をFirstPersonCharacterの直下に生成する
            GameObject tmp = Instantiate(PrimaryWeapon);
            tmp.transform.parent = child.transform;
            
            // 生成したものをプライマリにセットする
            PrimaryWeapon = tmp;

            // 表示
            PrimaryWeapon.SetActive(true);

            // GunContollerを読み込む
            GCPrimaryScript = PrimaryWeapon.GetComponent<GunController>();

            // GunInfoから情報を入れる
            GCPrimaryScript.gunRank        = GIScript.gunRank;
            GCPrimaryScript.gunType        = GIScript.gunType;
            GCPrimaryScript.skillSlot      = GIScript.skillSlot;
            GCPrimaryScript.OneMagazine    = GIScript.OneMagazine;
            GCPrimaryScript.MaxAmmo        = GIScript.MaxAmmo;
            GCPrimaryScript.Damage         = GIScript.Damage;
            GCPrimaryScript.shootInterval  = GIScript.shootInterval;
            GCPrimaryScript.reloadInterval = GIScript.reloadInterval;
            GCPrimaryScript.bulletPower    = GIScript.bulletPower;
            GCPrimaryScript.GunEXP         = GIScript.GunEXP;

            // プライマリなので１に変更
            ownGun = 1;

            return null;
        }

        // プライマリ武器しか持っていないとき
        if(ownGun == 1 && SecondaryWeapon == null)
        {
            // 落ちている武器を入れる
            SecondaryWeapon = DIScript;

            // 拾った武器をFirstPersonCharacterの直下に生成する
            GameObject tmp = Instantiate(SecondaryWeapon);
            tmp.transform.parent = child.transform;

            // 生成したものをセカンダリにセットする
            SecondaryWeapon = tmp;

            // セカンダリを表示
            PrimaryWeapon.SetActive(false);
            SecondaryWeapon.SetActive(true);

            // GunContollerを読み込む
            GCSecondaryScript = SecondaryWeapon.GetComponent<GunController>();

            // GunInfoから情報を入れる
            GCSecondaryScript.gunRank        = GIScript.gunRank;
            GCSecondaryScript.gunType        = GIScript.gunType;
            GCSecondaryScript.skillSlot      = GIScript.skillSlot;
            GCSecondaryScript.OneMagazine    = GIScript.OneMagazine;
            GCSecondaryScript.MaxAmmo        = GIScript.MaxAmmo;
            GCSecondaryScript.Damage         = GIScript.Damage;
            GCSecondaryScript.shootInterval  = GIScript.shootInterval;
            GCSecondaryScript.reloadInterval = GIScript.reloadInterval;
            GCSecondaryScript.bulletPower    = GIScript.bulletPower;
            GCSecondaryScript.GunEXP         = GIScript.GunEXP;
            
            // セカンダリなので２にする
            ownGun = 2;
            
            return null;
        }

        // プライマリ武器と交換する
        else if(ownGun == 1)
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
            GameObject tmp = Instantiate(PrimaryWeapon);
            tmp.transform.parent = child.transform;

            // 生成したものをプライマリにセットする
            PrimaryWeapon = tmp;

            // GunContollerを読み込む
            GCPrimaryScript = PrimaryWeapon.GetComponent<GunController>();

            // GunInfoから情報を入れる
            GCPrimaryScript.gunRank        = GIScript.gunRank;
            GCPrimaryScript.gunType        = GIScript.gunType;
            GCPrimaryScript.skillSlot      = GIScript.skillSlot;
            GCPrimaryScript.OneMagazine    = GIScript.OneMagazine;
            GCPrimaryScript.MaxAmmo        = GIScript.MaxAmmo;
            GCPrimaryScript.Damage         = GIScript.Damage;
            GCPrimaryScript.shootInterval  = GIScript.shootInterval;
            GCPrimaryScript.reloadInterval = GIScript.reloadInterval;
            GCPrimaryScript.bulletPower    = GIScript.bulletPower;
            GCPrimaryScript.GunEXP         = GIScript.GunEXP;

            // 落としてある武器に保持した情報を入れる
            GameObject dropPrefab = dropInfo;

            // 表示
            PrimaryWeapon.SetActive(true);

            return dropPrefab;
        }

        // セカンダリ武器と交換する
        if(ownGun == 2)
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
            GameObject tmp = Instantiate(SecondaryWeapon);
            tmp.transform.parent = child.transform;
            
            // 生成したものをセカンダリにセットする
            SecondaryWeapon = tmp;

            // GunContollerを読み込む
            GCSecondaryScript = PrimaryWeapon.GetComponent<GunController>();

            // GunInfoから情報を入れる
            GCSecondaryScript.gunRank        = GIScript.gunRank;
            GCSecondaryScript.gunType        = GIScript.gunType;
            GCSecondaryScript.skillSlot      = GIScript.skillSlot;
            GCSecondaryScript.OneMagazine    = GIScript.OneMagazine;
            GCSecondaryScript.MaxAmmo        = GIScript.MaxAmmo;
            GCSecondaryScript.Damage         = GIScript.Damage;
            GCSecondaryScript.shootInterval  = GIScript.shootInterval;
            GCSecondaryScript.reloadInterval = GIScript.reloadInterval;
            GCSecondaryScript.bulletPower    = GIScript.bulletPower;
            GCSecondaryScript.GunEXP         = GIScript.GunEXP;

            
            // 落としてある武器に保持した情報を入れる
            GameObject dropPrefab = dropInfo;

            // 表示
            SecondaryWeapon.SetActive(true);

            return dropPrefab;
        }
        
        return null;
    }
}
  