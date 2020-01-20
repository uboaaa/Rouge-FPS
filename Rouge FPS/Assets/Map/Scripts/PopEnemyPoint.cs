using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopEnemyPoint : MonoBehaviour
{
    //出現する敵の数
    [SerializeField]
    private int m_PopMax=5;

    //出現する間隔
    [SerializeField]
    private float m_PopInterval = 20.0f;

    //出現している敵の数
    private int m_PopCount = 0;

    //敵マネージャー
    private EnemyManager enemyManager;

    //出現からの経過時間
    private float m_TimeElapsed = 0.0f;

    //更新開始フラグ
    private bool m_EnableUpdate = false;

    //敵頭上フラグ
    private bool m_OnEnemy = false;

    //初期化
    void Awake()
    {
        //必ず非active状態で始める
        this.gameObject.SetActive(false);

        //親のEnemyManagerコンポーネントを取得
        GameObject parentObj = transform.parent.gameObject;
        enemyManager = parentObj.GetComponent<EnemyManager>();
    }

    void Start()
    {
        
    }

    //更新
    void Update()
    {
        //部屋にプレイヤーが入ってきたら、更新を開始
        if (!m_EnableUpdate) return;

        //スポーン地点の上に敵がいる場合は出現しない
        if (m_OnEnemy) return;

        //出現数が最大のとき
        if (m_PopCount > m_PopMax) return;

        //経過時間を更新
        m_TimeElapsed += Time.deltaTime;

        //経過時間が一定に達した時に敵出現
        if(m_TimeElapsed > m_PopInterval)
        {
            //敵データをランダム取得
            GameObject _newEnemy = enemyManager.GetEnemyObject();

            //取得した敵をインスタンス化し、マネージャーのリストに追加
            enemyManager.AddEnemy(Instantiate(_newEnemy, this.transform.position, new Quaternion()));

            //出現数を加算
            m_PopCount++;

            //経過時間リセット
            m_TimeElapsed = 0.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Room")
        {
            m_EnableUpdate = true;
        }

        if ((other.tag == "Enemy"))
        {
            m_OnEnemy = true;
        }
    }
}
