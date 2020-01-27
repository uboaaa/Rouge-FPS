using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMEnabler : MonoBehaviour
{
    private EnemyManager em = null;

    void Start()
    {
        em = this.gameObject.GetComponent<EnemyManager>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        //部屋がアクティブになった場合、EnemyManagerもアクティブにする
        if (other.tag == "Room")
        {
            em.enabled = true;
        }
    }
}
