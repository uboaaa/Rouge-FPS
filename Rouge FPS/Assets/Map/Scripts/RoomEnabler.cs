using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnabler : MonoBehaviour
{
    public BoxCollider collider = null;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            collider.enabled = true;
        }
    }
}
