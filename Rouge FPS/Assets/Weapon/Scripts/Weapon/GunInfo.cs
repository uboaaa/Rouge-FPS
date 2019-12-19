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
                        RocketLauncher,         // ロケットランチャー
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
    [SerializeField] public int        remAmmo;                             // 残弾数(武器依存)
    [HideInInspector] public int       remMagazine;                         // マガジン内の残弾数(武器依存)
    [SerializeField] public int        Damage;                              // 火力(武器依存　+-ランダム)
    [SerializeField] public float      shootInterval;                       // 次発射までの間の時間(武器依存)
    [SerializeField] public float      reloadInterval;                      // リロード終わりまでの時間(武器依存)
	[SerializeField] public float      bulletPower;                         // 弾を飛ばす力(武器依存)
    // パラメーター関係==============================================
    public float GunEXP = 0;                                                // 経験値

    void Start()
    {
        // 能力値を選出する
        // ※※※※※※※※※※※※※
        // 後から変更しないといけない
        // ※※※※※※※※※※※※※
        int random =　UnityEngine.Random.Range(0, 2);
        if(random == 0)
        {
            gunType = GunInfo.GunType.HandGun;
        }
        else if(random == 1)
        {
            gunType = GunInfo.GunType.LightMachineGun;
        }

        random =　UnityEngine.Random.Range(0, 3);
        if(random == 0)
        {
            gunRank = GunInfo.GunRank.Rank1;
        }
        else if(random == 1)
        {
            gunRank = GunInfo.GunRank.Rank2;
        }
        else if(random == 2)
        {
            gunRank = GunInfo.GunRank.Rank3;
        }

        skillSlot = UnityEngine.Random.Range(1, 4);     // 1~3個

        //Damage
    }
}
