using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLNovaScript : MonoBehaviour
{
    Collider Col;
    void Start()
    {
        Col = GetComponent<Collider>();
    }

    void Update()
    {
        Destroy(Col,0.1f);
    }
}
