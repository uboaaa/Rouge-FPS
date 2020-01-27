using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ELevel
{
    lv1,lv2,lv3
}

[System.Serializable]
public class EnemyGroup
{
    public ELevel level = ELevel.lv1;
    public GameObject obj = null;
}

//EMEnablerを自動アタッチ
[RequireComponent(typeof(EMEnabler))]

public class EnemyManager : MonoBehaviour
{
    //地上敵グループ配列
    public List<EnemyGroup> m_groundGroups;
    //空中敵グループ配列
    public List<EnemyGroup> m_airGroups;

    //スポナーのリスト
    private List<PopEnemyPoint> m_spawnList = new List<PopEnemyPoint>();
    //出現している敵リスト
    private List<GameObject> m_popEnemyList = new List<GameObject>();

    //最大出現数
    public int m_PopMax = 0;
    //出現の間隔
    public float m_PopInterval = 1.0f;
    //
    private float m_Progress = 0.0f;

    //初期化
    void Start()
    {
       //子オブジェクト内のPopEnemyPointコンポーネントを全て取得
       //その後、リストにセット
       foreach(Transform spawner in this.transform)
        {
            if (spawner.tag == "GroundSpawner" || spawner.tag == "AirSpawner")
            {
                PopEnemyPoint _point = spawner.GetComponent<PopEnemyPoint>();
                m_spawnList.Add(_point);
            }
        }

       //Enablerにアクティブにされるまで、非アクティブにする
        this.enabled = false;
    }

    //更新
    void Update()
    {
        //最大出現数を超えたら、早期リターン
        if (m_popEnemyList.Count >= m_PopMax) return;

        //経過時間を加算
        m_Progress += Time.deltaTime;
        //出現間隔を超えるまで、早期リターン
        if (m_Progress < m_PopInterval) return;

        //スポーン地点をランダムで取得
        PopEnemyPoint popPoint = m_spawnList.GetAtRandom();

        //取得したスポナーからポップ処理
        //※popPointがnullの場合は処理しない
        popPoint?.Pop();

        //経過時間をリセット
        m_Progress = 0.0f;
    }

    //敵オブジェクト取得処理
    //※敵コンポーネント取得して、レベル設定してから渡す
    public GameObject GetEnemyObject(string _posType)
    {
        //tagに応じて、地上型、空中型の敵をスポナーに返す
        EnemyGroup _enemy;
        switch (_posType)
        {
            case "GroundSpawner":
                _enemy = m_groundGroups.GetAtRandom();
                break;
            case "AirSpawner":
                _enemy = m_airGroups.GetAtRandom();
                break;
            default:
                _enemy = m_groundGroups.GetAtRandom();
                Debug.Log("Tag判定失敗");
                break;
        }

        //EnemyParameterを取得し、レベルを設定
        //※Elevelは0～なので、1を足して補正
        _enemy.obj.GetComponent<EnemyParameter>().AILevel= (int)_enemy.level + 1;
       
        return _enemy.obj;
    }

    //出現している敵リストへの追加処理
    public void AddEnemy(GameObject _enemy)
    {
        m_popEnemyList.Add(_enemy);
    } 
}
