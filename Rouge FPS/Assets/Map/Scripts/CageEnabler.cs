using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageEnabler : MonoBehaviour
{
    //壁コライダー
    public BoxCollider coll;

    //壁エフェクト
    public GameObject effect;

    //探索済み判定フラグ
    private bool m_exploreded = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Explored")
        {
            m_exploreded = true;
            coll.enabled = false;
            effect.SetActive(false);
        }

        if (other.tag == "Room")
        {
            if (m_exploreded) return;
            coll.enabled = true;
            effect.SetActive(true);
        }
    }
}
