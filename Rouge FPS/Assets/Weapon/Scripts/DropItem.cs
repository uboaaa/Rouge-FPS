//==============================================================
// ドロップアイテム用スクリプト
//==============================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] GameObject EffectPrefab;
    void Start()
    {
        if(EffectPrefab != null)
        {
            // エフェクトを生成
            GameObject Effect = Instantiate<GameObject>(EffectPrefab,transform.position,transform.rotation);
        }
    }

    void Update()
    {
        
    }
}
