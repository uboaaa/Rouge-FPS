using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAI : MonoBehaviour
{
    [SerializeField]
    private MapInitializer initializer = null;

    private int[,] m_nowMap;                    //マップチップ情報

    private int m_roomId = -1;                  //現在の部屋ID(-1のときは通路のとき)

    //
    void Start()
    {
        m_nowMap = initializer.GetMap();
    }

    //
    void Update()
    {
        
    }

    //
    private void FixedUpdate()
    {
        
    }

}
