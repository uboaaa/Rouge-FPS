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
            //部屋有効化の判定用コライダーをtrueにする
            trans_coll.enabled = true;
            //役目を終えたのでこのオブジェクトをfalseにする
            this.gameObject.SetActive(false);
        }
    }
}
