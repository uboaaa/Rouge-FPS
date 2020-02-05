using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDelete : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        Destroy(this.gameObject,20.0f);
    }
}
