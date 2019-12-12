using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapManager : MonoBehaviour
{
    //マップチップ
    [SerializeField]
    private GameObject m_tipPrefab = null;
    //マップチップのリスト
    [SerializeField]
    private MapTip[,] m_tipArray;

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

    //マップ
    private int[,] mapdata;

    // Start is called before the first frame update
    void Start()
    {
        m_markRect = m_playerMarker.gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (m_miniMap.activeSelf)
            {
                m_miniMap.SetActive(false);
                m_playerMarker.SetActive(false);
            }
            else
            {
                m_miniMap.SetActive(true);
                m_playerMarker.SetActive(true);
            }
        } 
    }

    private void LateUpdate()
    {
        //プレイヤーの配置データを取得
        //右上がアンカーなのでマップサイズ分を引いて補正
        float x = 0, y = 0;
        Vector3 euler = new Vector3();
        if (MergeScenes.IsMerge())
        {
            x = PlayerXYZ.GetPlayerPosition("px") / MapInitializer.MAP_SCALE ;
            y = PlayerXYZ.GetPlayerPosition("pz") / MapInitializer.MAP_SCALE ;
            euler = PlayerXYZ.GetPlayerRotation();
        }
        else
        {
            x = (m_target.transform.position.x) / MapInitializer.MAP_SCALE ;
            y = (m_target.transform.position.z) / MapInitializer.MAP_SCALE ;
            euler = m_target.transform.localEulerAngles;
        }

        //プレイヤーの周囲のチップをONにする
        int tipx = (int)x;
        int tipy = (int)y;
        //if (m_tipArray[tipx, tipy] != null)
        //{
        //    m_tipArray[tipx, tipy].m_tipEnable.Value = true;
        //}

        //マーカーのサイズ分を乗算
        float posX = (x - MapInitializer.MAP_SIZE_X / 2 + 1) * 5;   //
        float posY = (y - MapInitializer.MAP_SIZE_Y / 2 + 1) * 5;

        //アンカーを基準に座標、回転を設定
        m_markRect.anchoredPosition = new Vector3(posX - m_markRect.sizeDelta.x / 2.0f, posY - m_markRect.sizeDelta.y / 2.0f);          //m_markRect.sizeDeleta.x/2.0fはズレ補正分
        m_markRect.rotation = Quaternion.Euler(0, 0, -euler.y);
    }

    //マップチップ生成
    public void CreateMapTip(int[,] map)
    {
        //マップをセット
        mapdata = map;

        //チップ配列を初期化
        m_tipArray = new MapTip[MapInitializer.MAP_SIZE_X, MapInitializer.MAP_SIZE_Y];

        for(int y = 0; y < MapInitializer.MAP_SIZE_Y; y++)
        {
            for(int x = 0; x < MapInitializer.MAP_SIZE_X; x++)
            {
                if (mapdata[x, y] == 0)
                {
                    //マップチップのオブジェクト生成
                    GameObject mapTip = Instantiate(m_tipPrefab) as GameObject;

                    //親にミニマップ用オブジェクトを設定
                    mapTip.transform.SetParent(m_miniMap.transform);

                    //マップチップの配置情報を初期化、登録
                    m_tipArray[x, y] = mapTip.GetComponent<MapTip>();
                    m_tipArray[x, y].Initialize(x, y, MapInitializer.MAP_SIZE_X / 2, MapInitializer.MAP_SIZE_Y / 2);
                }
            }
        }
    }

}
