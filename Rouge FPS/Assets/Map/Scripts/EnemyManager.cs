using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//***エディタ拡張を直す


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

public class EnemyManager : MonoBehaviour
{
    //敵グループ配列
    public List<EnemyGroup> m_enemyGroups;

    //出現している敵リスト
    private List<GameObject> m_popEnemyList = new List<GameObject>();

    //初期化
    void Start()
    {
       
    }

    //更新
    void Update()
    {
        
    }

    //敵オブジェクト取得処理
    //***敵コンポーネント取得して、レベル設定してから渡す
    public GameObject GetEnemyObject()
    {
        //リストからランダムに敵データを取得
        EnemyGroup _enemy = m_enemyGroups.GetAtRandom();

        //***レベル設定してから渡す

        return _enemy.obj;
    }

    //出現している敵リストへの追加処理
    public void AddEnemy(GameObject _enemy)
    {
        m_popEnemyList.Add(_enemy);
    }
}
