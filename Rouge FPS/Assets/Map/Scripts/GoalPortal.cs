using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MapFade))]
public class GoalPortal : MonoBehaviour
{
    //マップ初期化コンポーネント
    private MapInitializer m_MapInitializer;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject initObj = GameObject.Find("MapController");
        m_MapInitializer = initObj.GetComponent<MapInitializer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            MapFade.FadeOut();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if(MapFade.FadeProgress() < 1.0f)
            {
                return;
            }

            //階層の更新
            FloorCount.UpFloors();
            //次の階層を生成、移動
            m_MapInitializer.MoveNextMap();
            //オブジェクトを非アクティブにする
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            MapFade.FadeIn();
        }
    }
}
