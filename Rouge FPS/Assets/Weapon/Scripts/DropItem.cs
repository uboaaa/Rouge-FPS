//==============================================================
// ドロップアイテム用スクリプト
//==============================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
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
    public Sprite RocketLauncherSprite  = null;
    public Sprite FlameThrowerSprite    = null;
    public Sprite LaserGunSprite        = null;

    public GameObject WeaponInfo;
    GameObject Obj;

    void Start()
    {
        GIScript = GetComponent<GunInfo>();

        gunSprite = GetComponent<SpriteRenderer>();

        // 銃の種類を判別する
        switch(GIScript.gunType)
        {
            case GunInfo.GunType.AssaultRifle:
                gunSprite.sprite = AssaultRifleSprite;
                break;
            case GunInfo.GunType.HandGun:
                gunSprite.sprite = HandGunSprite;
                break;
            case GunInfo.GunType.LightMachineGun:
                gunSprite.sprite = LightMachineGunSprite;
                break;
            case GunInfo.GunType.ShotGun:
                gunSprite.sprite = ShotGunSprite;
                break;
            case GunInfo.GunType.SubMachineGun:
                gunSprite.sprite = SubMachineGunSprite;
                break;
            case GunInfo.GunType.RocketLauncher:
                gunSprite.sprite = RocketLauncherSprite;
                break;
            case GunInfo.GunType.FlameThrower:
                gunSprite.sprite = FlameThrowerSprite;
                break;
            case GunInfo.GunType.LaserGun:
                gunSprite.sprite = LaserGunSprite;
                break;
        }

        // 銃のランクを判別する
        switch(GIScript.gunRank)
        {
            case GunInfo.GunRank.Rank1:
                // エフェクトを生成
                Obj = Instantiate<GameObject>(Runk1EffectPrefab,transform.position,Quaternion.identity);
		        Obj.transform.parent = this.transform;
                break;
            case GunInfo.GunRank.Rank2:
                // エフェクトを生成
                Obj = Instantiate<GameObject>(Runk2EffectPrefab,transform.position,Quaternion.identity);
		        Obj.transform.parent = this.transform;
                break;
            case GunInfo.GunRank.Rank3:
                // エフェクトを生成
                Obj = Instantiate<GameObject>(Runk3EffectPrefab,transform.position,Quaternion.identity);
		        Obj.transform.parent = this.transform;
                break;
        }
    }

    void Update()
    {
        if(runkEffect != null)
        {
            runkEffect.transform.position = transform.position;
        }
    }
}
