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
    [SerializeField] public ShootMode  shootMode;                           // 武器の発射モード
    [SerializeField] public GunType    gunType;                             // 種類情報
    [SerializeField] public GunRank    gunRank;                             // ランク情報
    [SerializeField] public int        skillSlot;                           // スキルスロット数
    [SerializeField] public int        MagazineSize;                        // マガジンサイズ
    [SerializeField] public int        remAmmo;                             // 残弾数
    [HideInInspector] public int       remMagazine;                         // マガジン内の残弾数
    [SerializeField] public int        Damage;                              // 火力
    [SerializeField] public float      shootInterval;                       // 次発射までの間の時間
    [SerializeField] public float      reloadInterval;                      // リロード終わりまでの時間
	[SerializeField] public float      bulletPower;                         // 弾を飛ばす力
     // パラメーター関係==============================================
    public float GunEXP;                                                    // 経験値

    void Start()
    {
        // 能力値を選出する

    }


    void Update()
    {
        
    }
}
