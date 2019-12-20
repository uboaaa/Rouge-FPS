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
    [SerializeField]
    private GameObject m_wholeMap = null;
    [SerializeField]
    private GameObject m_raderCamera = null;
    [SerializeField]
    private GameObject m_raderPlayer = null;

    //確認用プレイヤーオブジェクト
    [SerializeField]
    private GameObject m_target = null;

    //マップ
    private int[,] mapdata;

    // Start is called before the first frame update
    void Start()
    {
        m_wholeMap.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //レーダ表示マップ
        float x = 0, z = 0;
        Vector3 euler = new Vector3();
        if (MergeScenes.IsMerge())
        {
            x = PlayerXYZ.GetPlayerPosition("px");
            z = PlayerXYZ.GetPlayerPosition("pz");
            euler = PlayerXYZ.GetPlayerRotation();
        }
        else
        {
            x = m_target.transform.position.x;
            z = m_target.transform.position.z;
            euler = m_target.transform.localEulerAngles;
        }
        m_raderPlayer.transform.position = new Vector3(x, 10, z);               //プレイヤーの座標を更新
        m_raderPlayer.transform.rotation = Quaternion.Euler(90, 0, -euler.y);   //プレイヤーの回転を更新
        m_raderCamera.transform.position = new Vector3(x, 200, z);              //プレイヤーの真上にカメラをセット
        m_raderCamera.transform.rotation = Quaternion.Euler(90, euler.y, 0);    //カメラを真下に向ける

        //TabキーでマップON/OFF
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (m_miniMap.activeSelf)
            {
                m_miniMap.SetActive(false);
                m_wholeMap.SetActive(true);

            }
            else
            {
                m_miniMap.SetActive(true);
                m_wholeMap.SetActive(false);
            }
        } 
    }

    private void LateUpdate()
    {

    }
}
