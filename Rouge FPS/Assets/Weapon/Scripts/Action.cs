using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    private bool ActionFlg;

    private GameObject dropPrefab;
    void Start()
    {
        // 中身を取ってくる
        dropPrefab = GameObject.Find("DropItem");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && ActionFlg)
        {
            Destroy(this.gameObject);

            // DropItemを生成
            GameObject dropItem = Instantiate<GameObject>(dropPrefab, this.transform.position, this.transform.rotation);
        }
    }

    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            ActionFlg = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        ActionFlg = false;
    }
}
