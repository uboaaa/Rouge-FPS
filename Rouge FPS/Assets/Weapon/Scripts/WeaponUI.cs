using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WeaponUI : MonoBehaviour
{
    private enum OwnImage
    {
        Primary,
        Secondary
    }
    [SerializeField] OwnImage UIImage;
    ChangeEquip CEScript;
    GunController PriGCScript = null;
    GunController SecGCScript = null;
    Image PrimaryImage;
    Image SecondaryImage;
    [SerializeField] Sprite AssaultRifleSprite    = null;
    [SerializeField] Sprite HandGunSprite         = null;
    [SerializeField] Sprite LightMachineGunSprite = null;
    [SerializeField] Sprite ShotGunSprite         = null;
    [SerializeField] Sprite SubMachineGunSprite   = null;
    [SerializeField] Sprite GrenadeLauncherSprite = null;

    void Start()
    {
        GameObject anotherObject = GameObject.Find("FPSController");
        CEScript = anotherObject.GetComponent<ChangeEquip>();
        
        // Imageの取得
        GameObject PrimaryObject = GameObject.Find("PrimaryImage");
        PrimaryImage = PrimaryObject.GetComponent<Image>();

        GameObject SecondaryObject = GameObject.Find("SecondaryImage");
        SecondaryImage = SecondaryObject.GetComponent<Image>();
    }

    void Update()
    {
        if(CEScript.ownGun == 1 && UIImage == OwnImage.Primary)
        {
            PriGCScript = CEScript.PrimaryWeapon.GetComponent<GunController>();

            // 武器を更新する
            selectGunSprite(PriGCScript.gunType,PrimaryImage);

            // 明るくする
            PrimaryImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

            // 使っていない方は薄くする
            if(SecondaryImage != null)
            {
                SecondaryImage.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            }
        }
        else if(CEScript.ownGun == 2 && UIImage == OwnImage.Secondary)
        {
            SecGCScript = CEScript.SecondaryWeapon.GetComponent<GunController>();

            // 武器を更新する
            selectGunSprite(SecGCScript.gunType,SecondaryImage);

            // 明るくする
            SecondaryImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

            // 使っていない方は薄くする
            if(PrimaryImage != null)
            {
                PrimaryImage.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            }
        }

        if(CEScript.ownGun == 1)
        {
            PrimaryImage.enabled = true;
        }

        if(CEScript.ownGun == 2)
        {
            SecondaryImage.enabled = true;
        }
    }

    void selectGunSprite(GunInfo.GunType gunType,Image image)
    {
        switch(gunType)
        {
            case GunInfo.GunType.AssaultRifle:
                image.sprite = AssaultRifleSprite;
                break;
            case GunInfo.GunType.SubMachineGun:
                image.sprite = SubMachineGunSprite;
                break;
            case GunInfo.GunType.HandGun:
                image.sprite = HandGunSprite;
                break;
            case GunInfo.GunType.ShotGun:
                image.sprite = ShotGunSprite;
                break;
            case GunInfo.GunType.LightMachineGun:
                image.sprite = LightMachineGunSprite;
                break;
            case GunInfo.GunType.GrenadeLauncher:
                image.sprite = GrenadeLauncherSprite;
                break;
        }
    }
}
