using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPortal : MonoBehaviour
{
    //マップ初期化コンポーネント
    private MapInitializer mapInitializer;

    //経過時間
    private float m_Progress = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject initObj = GameObject.Find("MapController");
        mapInitializer = initObj.GetComponent<MapInitializer>();
    }
    
    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Player")
        {
            if(m_Progress < 2.0f)
            {
                m_Progress += Time.deltaTime;
                return;
            }

            m_Progress = 0.0f;

            //階層の更新
            FloorCount.UpFloors();
            //次の階層を生成、移動
            mapInitializer.MoveNextMap();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_Progress = 0.0f;
        }
    }
}
