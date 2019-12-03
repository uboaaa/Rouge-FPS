using System.Collections;
using System.Collections.Generic;
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
                        }
    public enum GunRank { Rank1, Rank2, Rank3 }                             // ランク
    [SerializeField] public enum ShootMode { AUTO, SEMIAUTO }               // 単発が連射か
    [SerializeField] public ShootMode  shootMode = ShootMode.AUTO;          // 武器の発射モード
    [SerializeField] public GunType    gunType     = GunType.AssaultRifle;  // 種類情報
    [SerializeField] public GunRank    gunRank     = GunRank.Rank1;         // ランク情報
    [SerializeField] public int        skillSlot{get;set;}                  // スキルスロット数
    [SerializeField] public int        OneMagazine{get;set;}                // マガジン内の弾
    [SerializeField] public int        MaxAmmo{get;set;}                    // 残弾数
    [SerializeField] public int        Damage{get;set;}                     // 火力
    [SerializeField] public float      shootInterval{get;set;}              // 次発射までの間の時間
    [SerializeField] public float      reloadInterval{get;set;}             // リロード終わりまでの時間
	[SerializeField] public float      bulletPower{get;set;}                // 弾を飛ばす力

     // パラメーター関係==============================================
    public float GunEXP;                                    // 経験値

    void Start()
    {
        // 能力値を選出する

    }


    void Update()
    {
        
    }
}
