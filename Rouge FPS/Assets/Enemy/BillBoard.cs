using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("FPSController");
    }

    void Update()
    {
        // ビルボード
        Vector3 p = player.gameObject.transform.position;
        p.y = transform.position.y;
        transform.LookAt(p);
    }
}
