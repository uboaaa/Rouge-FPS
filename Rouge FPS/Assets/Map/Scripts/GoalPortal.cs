using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MapFade))]
public class GoalPortal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
