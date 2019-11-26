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
    [SerializeField] int        skillSlot   = 1;                            // スキルスロット数
    [SerializeField] int        OneMagazine = 0;                            // マガジン内の弾
    [SerializeField] int        MaxAmmo = 0;                                // 残弾数
    [SerializeField] int        Damage = 1;                                 // 火力
    [SerializeField] float      shootInterval = 0.15f;                      // 次発射までの間の時間
    [SerializeField] float      reloadInterval = 5.0f;                      // リロード終わりまでの時間
	[SerializeField] float      bulletPower = 100.0f;                       // 弾を飛ばす力

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
