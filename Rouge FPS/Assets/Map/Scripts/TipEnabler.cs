using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipEnabler : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spRenderer = null;

    void Start()
    {
        //spRenderer = this.GetComponent<SpriteRenderer>();
        //spRenderer.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MiniMap")
        {
            spRenderer.enabled = true;
        }
        
        if(other.tag == "Room")
        {
            spRenderer.enabled = true;
        }
    }
}
