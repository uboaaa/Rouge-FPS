using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapManager : MonoBehaviour
{
    //マップチップ
    [SerializeField]
    private GameObject tipPrefab = null;

    //ミニマップ用オブジェクト
    public GameObject miniMap = null;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //マップチップ生成
    public void CreateMapTip(int[,] mapdata)
    {
        for(int y = 0; y < 30; y++)
        {
            for(int x = 0; x < 30; x++)
            {
                if (mapdata[x, y] == 0)
                {
                    //マップチップのオブジェクト生成
                    GameObject mapTip = Instantiate(tipPrefab) as GameObject;

                    //親にミニマップ用オブジェクトを設定
                    mapTip.transform.parent = miniMap.transform;

                    //マップチップの配置情報を初期化
                    MapTip tipComp = mapTip.GetComponent<MapTip>();
                    tipComp.Initialize(x, y);
                }
            }
        }
    }

}
