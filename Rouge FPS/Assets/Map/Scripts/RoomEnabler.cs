using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnabler : MonoBehaviour
{
    public BoxCollider trans_coll = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trans_coll.enabled = true;
        }
    }
}
