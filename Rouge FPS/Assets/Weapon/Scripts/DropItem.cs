//==============================================================
// ドロップアイテム用スクリプト
//==============================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] GameObject EffectPrefab;
    private GameObject Effect;
    void Start()
    {
        if(EffectPrefab != null)
        {
            // エフェクトを生成
            Effect = Instantiate<GameObject>(EffectPrefab,transform.position,transform.rotation);
        }
    }

    void Update()
    {
        Effect.transform.position = transform.position;
    }
}
