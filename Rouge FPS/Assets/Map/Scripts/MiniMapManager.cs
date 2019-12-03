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
        var x = PlayerXYZ.GetPlayerPosition("px") / m_mapScale - m_mapSizeX;
        var y = PlayerXYZ.GetPlayerPosition("pz") / m_mapScale - m_mapSizeY;
        //マーカーのサイズ分を乗算
        x *= m_markRect.sizeDelta.x;
        y *= m_markRect.sizeDelta.y;
        //アンカーを基準に座標を設定
        m_markRect.anchoredPosition = new Vector3(x, y);
    }

    //マップチップ生成
    public void CreateMapTip(int[,] mapdata,int sizeX,int sizeY,int scale)
    {
        m_mapSizeX = sizeX;
        m_mapSizeY = sizeY;

        for(int y = 0; y < m_mapSizeY; y++)
        {
            for(int x = 0; x < m_mapSizeX; x++)
            {
                if (mapdata[x, y] == 0)
                {
                    //マップチップのオブジェクト生成
                    GameObject mapTip = Instantiate(m_tipPrefab) as GameObject;

                    //親にミニマップ用オブジェクトを設定
                    mapTip.transform.parent = m_miniMap.transform;

                    //マップチップの配置情報を初期化
                    MapTip tipComp = mapTip.GetComponent<MapTip>();
                    tipComp.Initialize(x, y, m_mapSizeX, m_mapSizeY);
                }
            }
        }
    }

}
