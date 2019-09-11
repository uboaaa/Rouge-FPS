using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FirstPersonGunController : MonoBehaviour
{
    public enum ShootMode { AUTO, SEMIAUTO }
    public bool shootEnabled = true;
    [SerializeField]
    ShootMode shootMode = ShootMode.AUTO;
    [SerializeField]
    int maxAmmo = 100;
    [SerializeField]
    int damage = 1;
    [SerializeField]
    float shootInterval = 0.15f;
    [SerializeField]
    float shootRange = 50;
    [SerializeField]
    Vector3 muzzleFlashScale;
    [SerializeField]
    GameObject muzzleFlashPrefab;
    [SerializeField]
    GameObject hitEffectPrefab;
    bool shooting = false;
    int ammo;
    GameObject muzzleFlash;
    GameObject hitEffect;
    public int Ammo
    {
        set
        {
            ammo = Mathf.Clamp(value, 0, maxAmmo);
        }
        get
        {
            return ammo;
        }
    }
    void Start()
    {
        InitGun();
    }
    void Update()
    {
        if (shootEnabled & ammo > 0 & GetInput())
        {
            StartCoroutine(ShootTimer());
        }
    }
    void InitGun()
    {
        Ammo = maxAmmo;
    }
    bool GetInput()
    {
        switch (shootMode)
        {
            case ShootMode.AUTO:
                return Input.GetMouseButton(0);
            case ShootMode.SEMIAUTO:
                return Input.GetMouseButtonDown(0);
        }
        return false;
    }
    IEnumerator ShootTimer()
    {
        if (!shooting)
        {
            shooting = true;
            //マズルフラッシュON
            if (muzzleFlashPrefab != null)
            {
                if (muzzleFlash != null)
                {
                    muzzleFlash.SetActive(true);
                }
                else
                {
                    muzzleFlash = Instantiate(muzzleFlashPrefab, transform.position, transform.rotation);
                    muzzleFlash.transform.SetParent(gameObject.transform);
                    muzzleFlash.transform.localScale = muzzleFlashScale;
                }
            }
            Shoot();
            yield return new WaitForSeconds(shootInterval);
            //マズルフラッシュOFF
            if (muzzleFlash != null)
            {
                muzzleFlash.SetActive(false);
            }
            //ヒットエフェクトOFF
            if (hitEffect != null)
            {
                if (hitEffect.activeSelf)
                {
                    hitEffect.SetActive(false);
                }
            }
            shooting = false;
        }
        else
        {
            yield return null;
        }
    }
    void Shoot()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        //レイを飛ばして、ヒットしたオブジェクトの情報を得る
        if (Physics.Raycast(ray, out hit, shootRange))
        {
            //ヒットエフェクトON
            if (hitEffectPrefab != null)
            {
                if (hitEffect != null)
                {
                    hitEffect.transform.position = hit.point;
                    hitEffect.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
                    hitEffect.SetActive(true);
                }
                else
                {
                    hitEffect = Instantiate(hitEffectPrefab, hit.point, Quaternion.identity);
                }
            }
            //★ここに敵へのダメージ処理などを追加
        }
        Ammo--;
 
            //if (Ammo < 1) { Ammo = maxAmmo; }
        
    }
}