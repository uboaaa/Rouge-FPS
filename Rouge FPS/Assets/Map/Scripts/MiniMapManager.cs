using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapManager : MonoBehaviour
{
    //マップチップ
    [SerializeField]
    private GameObject m_tipPrefab = null;

    //ミニマップ用オブジェクト
    [SerializeField]
    private GameObject m_miniMap = null;

    //マーカー用オブジェクト
    [SerializeField]
    private GameObject m_playerMarker = null;
    private RectTransform m_markRect = null;

    //確認用プレイヤーオブジェクト
    [SerializeField]
    private GameObject m_target = null;

    //マップサイズ
    private int m_mapSizeX, m_mapSizeY, m_mapScale;

    // Start is called before the first frame update
    void Start()
    {
        m_markRect = m_playerMarker.gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        //プレイヤーの配置データを取得
        //右上がアンカーなのでマップサイズ分を引いて補正
        float x = 0, y = 0;
        Vector3 euler = new Vector3();
        if (MergeScenes.IsMerge())
        {
            x = PlayerXYZ.GetPlayerPosition("px") / MapInitializer.MAP_SCALE - MapInitializer.MAP_SIZE_X;
            y = PlayerXYZ.GetPlayerPosition("pz") / MapInitializer.MAP_SCALE - MapInitializer.MAP_SIZE_Y;

            //***オイラー角orQuaternionごと渡してもらう
            //rot = PlayerXYZ.GetPlayerPosition("ry");
            
        }
        else
        {
            x = (m_target.transform.position.x) / MapInitializer.MAP_SCALE - MapInitializer.MAP_SIZE_X;
            y = (m_target.transform.position.z) / MapInitializer.MAP_SCALE - MapInitializer.MAP_SIZE_Y;
            euler = m_target.transform.localEulerAngles;
        }
        //マーカーのサイズ分を乗算
        x *= 5; // m_markRect.sizeDelta.x;
        y *= 5; // m_markRect.sizeDelta.y;
        //アンカーを基準に座標、回転を設定
        m_markRect.anchoredPosition = new Vector3(x - m_markRect.sizeDelta.x / 2.0f, y - m_markRect.sizeDelta.y / 2.0f);          //m_markRect.sizeDeleta.x/2.0fはズレ補正分
        m_markRect.rotation = Quaternion.Euler(0, 0, -euler.y);
    }

    //マップチップ生成
    public void CreateMapTip(int[,] mapdata,int sizeX,int sizeY,int scale)
    {
        m_mapSizeX = sizeX;
        m_mapSizeY = sizeY;
        m_mapScale = scale;

        for(int y = 0; y < m_mapSizeY; y++)
        {
            for(int x = 0; x < m_mapSizeX; x++)
            {
                if (mapdata[x, y] >= 1)
                {
                    //マップチップのオブジェクト生成
                    GameObject mapTip = Instantiate(m_tipPrefab) as GameObject;

                    //親にミニマップ用オブジェクトを設定
                    mapTip.transform.SetParent(m_miniMap.transform);

                    //マップチップの配置情報を初期化
                    MapTip tipComp = mapTip.GetComponent<MapTip>();
                    tipComp.Initialize(x, y, m_mapSizeX, m_mapSizeY);
                }
            }
        }
    }

}
