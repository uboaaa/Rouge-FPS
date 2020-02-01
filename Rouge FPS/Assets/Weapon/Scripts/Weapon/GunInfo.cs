using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;


public class GunInfo : MonoBehaviour
{
    // インスペクター関係============================================
    // 種類
    public enum GunType {
                        AssaultRifle,           // アサルトライフル
                        SubMachineGun,          // サブマシンガン
                        LightMachineGun,        // ライトマシンガン
                        HandGun,                // ハンドガン
                        GrenadeLauncher,        // グレネードランチャー
                        ShotGun,                // ショットガン
                        LaserGun,               // レーザーガン
                        FlameThrower            // 火炎放射器
                        }                                                   // 武器種類
    public enum GunRank { Rank1, Rank2, Rank3 }                             // ランク
    [SerializeField] public enum ShootMode { AUTO, SEMIAUTO }               // 単発が連射か(武器依存)
    [SerializeField] public ShootMode  shootMode;                           // 武器の発射モード
    [SerializeField] public GunType    gunType;                             // 種類情報(ランダム)
    [SerializeField] public GunRank    gunRank;                             // ランク情報(ランダム)
    [SerializeField] public int        skillSlot;                           // スキルスロット数(ランダム)
    [SerializeField] public int        MagazineSize;                        // マガジンサイズ(武器依存)
    [HideInInspector] public int       remAmmo;                             // 残弾数(武器依存)
    [SerializeField] public int        ammoMax;                             // 予備弾数MAX(武器依存)
    [HideInInspector] public int       remMagazine;                         // マガジン内の残弾数(武器依存)
    [SerializeField] public int        Damage;                              // 火力(武器依存　+-ランダム)
    [SerializeField] public float      shootInterval;                       // 次発射までの間の時間(武器依存)
    [SerializeField] public float      reloadInterval;                      // リロード終わりまでの時間(武器依存)
	[SerializeField] public float      bulletPower;                         // 弾を飛ばす力(武器依存)
    // パラメーター関係==============================================
    public float GunEXP = 0;                                                // 経験値
    LoadGunPrefab LGPScript;
    GameObject GunObj;

    void Start()
    {
        LGPScript = GetComponent<LoadGunPrefab>();

        switch(gunType)
        {
            case GunInfo.GunType.AssaultRifle:
                GunObj = LGPScript.AssaultRifle;
                break;
            case GunInfo.GunType.HandGun:
                GunObj = LGPScript.HandGun;
                break;
            case GunInfo.GunType.LightMachineGun:
                GunObj = LGPScript.LightMachineGun;
                break;
            case GunInfo.GunType.ShotGun:
                GunObj = LGPScript.ShotGun;
                break;
            case GunInfo.GunType.SubMachineGun:
                GunObj = LGPScript.SubMachineGun;
                break;
            case GunInfo.GunType.GrenadeLauncher:
                GunObj = LGPScript.GrenadeLauncher;
                break;
            case GunInfo.GunType.FlameThrower:
                GunObj = LGPScript.FlameThrower;
                break;
        }

        // ※※※※※※※※※※※※※※※※※※※※※※※※※※※※
        // 修正した方がいいかも～
        // ※※※※※※※※※※※※※※※※※※※※※※※※※※※※
        MagazineSize   = GunObj.GetComponent<GunController>().MagazineSize;
        ammoMax        = GunObj.GetComponent<GunController>().remAmmo;
        Damage         = GunObj.GetComponent<GunController>().Damage;
        shootInterval  = GunObj.GetComponent<GunController>().shootInterval;
        reloadInterval = GunObj.GetComponent<GunController>().reloadInterval;
        bulletPower    = GunObj.GetComponent<GunController>().bulletPower;
        GunEXP         = GunObj.GetComponent<GunController>().GunEXP;
    }
}
