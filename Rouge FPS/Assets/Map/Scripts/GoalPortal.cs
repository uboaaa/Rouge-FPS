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
            //スキル関連の関数追加の予定
            UIManager.SetUIFlag(true);
        
            //階層の更新
            FloorCount.UpFloors();

            //次の階層を生成、移動
            //↓この関数をスキルの方に移動する
            MapInitializer.MoveNextMap();

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
