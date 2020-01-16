using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPortal : MonoBehaviour
{
    //マップ初期化コンポーネント
    private MapInitializer mapInitializer;

    // Start is called before the first frame update
    void Start()
    {
        GameObject initObj = GameObject.Find("MapController");
        mapInitializer = initObj.GetComponent<MapInitializer>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            mapInitializer.MoveNextMap();
        }
    }
}
