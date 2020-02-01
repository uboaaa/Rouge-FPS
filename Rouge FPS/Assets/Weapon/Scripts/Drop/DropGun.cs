//==============================================================
// ドロップアイテム用スクリプト
//==============================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGun : MonoBehaviour
{
    [SerializeField] GameObject Runk1EffectPrefab;      // Rank1用のエフェクト
    [SerializeField] GameObject Runk2EffectPrefab;      // Rank2用のエフェクト
    [SerializeField] GameObject Runk3EffectPrefab;      // Rank3用のエフェクト
    GunInfo GIScript;
    public GameObject runkEffect{get;set;}
    SpriteRenderer gunSprite;
    public Sprite AssaultRifleSprite    = null;
    public Sprite HandGunSprite         = null;
    public Sprite LightMachineGunSprite = null;
    public Sprite ShotGunSprite         = null;
    public Sprite SubMachineGunSprite   = null;
    public Sprite GrenadeLauncherSprite  = null;
    public Sprite FlameThrowerSprite    = null;
    public Sprite LaserGunSprite        = null;

    public GameObject WeaponInfo{get;private set;}
    GameObject Obj;
    LoadGunPrefab LGPScript;

    void Start()
    {
        GIScript = GetComponent<GunInfo>();
        gunSprite = GetComponent<SpriteRenderer>();
        LGPScript = GetComponent<LoadGunPrefab>();

        // 子オブジェクトをリセットしてからパーティクルを出す
        foreach ( Transform n in this.transform )
        {
            GameObject.Destroy(n.gameObject);
        }

        // 銃の種類を判別する
        switch(GIScript.gunType)
        {
            case GunInfo.GunType.AssaultRifle:
                // gunTypeを元にPrefabを取ってくる
                WeaponInfo = LGPScript.LoadGun("AssaultRifle(Clone)");
                // spriteをセット
                gunSprite.sprite = AssaultRifleSprite;
                break;
            case GunInfo.GunType.HandGun:
                // gunTypeを元にPrefabを取ってくる
                WeaponInfo = LGPScript.LoadGun("HandGun(Clone)");
                // spriteをセット
                gunSprite.sprite = HandGunSprite;
                break;
            case GunInfo.GunType.LightMachineGun:
                // gunTypeを元にPrefabを取ってくる
                WeaponInfo = LGPScript.LoadGun("LightMachineGun(Clone)");
                // spriteをセット
                gunSprite.sprite = LightMachineGunSprite;
                break;
            case GunInfo.GunType.ShotGun:
                // gunTypeを元にPrefabを取ってくる
                WeaponInfo = LGPScript.LoadGun("ShotGun(Clone)");
                // spriteをセット
                gunSprite.sprite = ShotGunSprite;
                break;
            case GunInfo.GunType.SubMachineGun:
                // gunTypeを元にPrefabを取ってくる
                WeaponInfo = LGPScript.LoadGun("SubMachineGun(Clone)");
                // spriteをセット
                gunSprite.sprite = SubMachineGunSprite;
                break;
            case GunInfo.GunType.GrenadeLauncher:
                // gunTypeを元にPrefabを取ってくる
                WeaponInfo = LGPScript.LoadGun("GrenadeLauncher(Clone)");
                // spriteをセット
                gunSprite.sprite = GrenadeLauncherSprite;
                break;
            case GunInfo.GunType.FlameThrower:
                // gunTypeを元にPrefabを取ってくる
                WeaponInfo = LGPScript.LoadGun("FlameThrower(Clone)");
                // spriteをセット
                gunSprite.sprite = FlameThrowerSprite;
                break;
        }

        // 銃のランクを判別する
        switch(GIScript.gunRank)
        {
            case GunInfo.GunRank.Rank1:
                // エフェクトを生成
                if(runkEffect == null)
                {
                    runkEffect = Instantiate<GameObject>(Runk1EffectPrefab,transform.position,Quaternion.identity);
		            runkEffect.transform.parent = this.transform;
                }
                break;
            case GunInfo.GunRank.Rank2:
                // エフェクトを生成
                if(runkEffect == null)
                {
                    runkEffect = Instantiate<GameObject>(Runk2EffectPrefab,transform.position,Quaternion.identity);
		            runkEffect.transform.parent = this.transform;
                }
                break;
            case GunInfo.GunRank.Rank3:
                // エフェクトを生成
                if(runkEffect == null)
                {
                    runkEffect = Instantiate<GameObject>(Runk3EffectPrefab,transform.position,Quaternion.identity);
		            runkEffect.transform.parent = this.transform;
                }
                break;
        }

        // 名前を変更する
        this.name = "Drop" + WeaponInfo.name;
    }

    void Update()
    {
        if(runkEffect != null)
        {
            runkEffect.transform.position = transform.position;
        }
    }
}
