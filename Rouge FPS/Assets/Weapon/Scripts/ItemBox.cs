using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField] GameObject ItemPrefab;
    [SerializeField] GameObject openEffectPrefab;                                       // オープンエフェクトのPrefab
    [SerializeField] Vector3    openEffectScale = new Vector3(1.0f,1.0f,1.0f);          // オープンエフェクトの大きさ変更用
    Transform boxTrans;
    void Start()
    {
        boxTrans = this.transform;
    }
    void Update(){}

    public void Open()
    {
        // 中身を表示
        Instantiate(ItemPrefab,boxTrans.position,Quaternion.identity);

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
