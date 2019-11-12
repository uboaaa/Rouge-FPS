using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Enemy = null;              //出現する敵オブジェクト

    [SerializeField]
    private int m_SpawnMax = 2;                     //スポーン最大数

    [SerializeField]
    private float m_SpawnInterval = 20.0f;          //スポーン待機時間

    private List<GameObject> m_enemyList = null;    //敵リスト

    private float m_timeElapsed = 0.0f;             //出現からの経過時間
    private int m_spawnCnt = 0;                     //スポーン数
    private int m_aliveCnt = 0;                     //生存している敵の数

    // Start is called before the first frame update
    void Start()
    {
        m_enemyList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //出現数が最大数以上のときは飛ばす
        if (m_spawnCnt >= m_SpawnMax)
        {
            return;
        }

        //経過時間を加算
        m_timeElapsed += Time.deltaTime;

        //経過時間一定に達したとき、敵出現
        if (m_timeElapsed > m_SpawnInterval)
        {
            m_enemyList.Add(Instantiate(m_Enemy, this.transform.position, new Quaternion()));

            m_spawnCnt++;
            m_timeElapsed = 0.0f;
        }
    }

    private void FixedUpdate()
    {
        //生存数を更新
        m_aliveCnt = m_enemyList.Count;
    }

    //出現数取得用の関数
    public int GetEnemyNum()
    {
        return m_spawnCnt;
    }
}
