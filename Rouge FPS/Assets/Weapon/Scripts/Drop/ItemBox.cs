using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField] GameObject ItemPrefab;
    [SerializeField] GameObject openEffectPrefab;                                       // オープンエフェクトのPrefab
    [SerializeField] Vector3    openEffectScale = new Vector3(1.0f,1.0f,1.0f);          // オープンエフェクトの大きさ変更用
    Transform       boxTrans;
    GunInfo         GIScript;
    int random;
    
    void Start()
    {
        boxTrans = this.transform;

        GIScript = ItemPrefab.GetComponent<GunInfo>();

        // 中身の抽選
        random =　UnityEngine.Random.Range(0, 6);
    }

    public void Open()
    {
        // 中身を表示
        Instantiate(ItemPrefab,new Vector3(boxTrans.position.x,boxTrans.position.y + 0.3f,boxTrans.position.z),Quaternion.identity);
        switch(random)
        {
            case 0:
                GIScript.gunType = GunInfo.GunType.HandGun;
                break;
            case 1:
                GIScript.gunType = GunInfo.GunType.AssaultRifle;
                break;
            case 2:
                GIScript.gunType = GunInfo.GunType.SubMachineGun;
                break;
            case 3:
                GIScript.gunType = GunInfo.GunType.ShotGun;
                break;
            case 4:
                GIScript.gunType = GunInfo.GunType.LightMachineGun;
                break;
            case 5:
                GIScript.gunType = GunInfo.GunType.GrenadeLauncher;
                break;
            default:
                Debug.Log("抽選失敗");
                break;
        }
        //表示するときの演出
        if (openEffectPrefab != null)
        {
            // オープンエフェクトの生成
	        GameObject openEffect = Instantiate<GameObject>(openEffectPrefab,new Vector3(boxTrans.position.x,boxTrans.position.y - 0.3f,boxTrans.position.z),openEffectPrefab.transform.rotation);
            openEffect.transform.localScale = openEffectScale;
		    Destroy(openEffect, 1.0f);
        }
    }
}
