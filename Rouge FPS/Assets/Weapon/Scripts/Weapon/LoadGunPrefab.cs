using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGunPrefab : MonoBehaviour
{
    [SerializeField]public GameObject HandGun;
    [SerializeField]public GameObject LightMachineGun;
    [SerializeField]public GameObject AssaultRifle;
    [SerializeField]public GameObject SubMachineGun;
    [SerializeField]public GameObject RocketLauncher;
    [SerializeField]public GameObject ShotGun;
    [SerializeField]public GameObject FlameThrower;

    public GameObject LoadGun(string gunName)
    {
        // Prefabなのでcloneができる
        switch(gunName)
        {
            case "HandGun(Clone)":
                return HandGun; 
            case "LightMachineGun(Clone)":
                return LightMachineGun;
            case "AssaultRifle(Clone)":
                return AssaultRifle;
            case "SubMachineGun(Clone)":
                return SubMachineGun;
            case "RocketLauncher(Clone)":
                return RocketLauncher;
            case "ShotGun(Clone)":
                return ShotGun;
            case "FlameThrower(Clone)":
                return FlameThrower;
            default:
                Debug.Log("ロードする銃がありません。");
                return null;
        }
    }
}
