using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageEnabler : MonoBehaviour
{
    public GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Room")
        {
            effect.SetActive(true);
        }
    }
}
