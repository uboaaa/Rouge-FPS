using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopEnemyPoint : MonoBehaviour
{
    //敵マネージャー
    private EnemyManager enemyManager;

    //ポップ時エフェクト
    public GameObject PopEffect;

    //初期化
    void Awake()
    {
        //親のEnemyManagerコンポーネントを取得
        GameObject parentObj = transform.parent.gameObject;
        enemyManager = parentObj.GetComponent<EnemyManager>();
    }



    public void Pop()
    {
        //スポーン地点の半径15内にEnemyオブジェクトがある場合、敵はでない
        Collider[] _coll = Physics.OverlapSphere(this.transform.position, 1.5f);
        foreach(Collider c in _coll)
        {
            string _tag = c.gameObject.tag;
            //EnemyかPlayerがいる場合
            if (_tag == "Enemy" || _tag=="Player") return;
        }

        //敵データをランダム取得
        GameObject _newEnemy = enemyManager.GetEnemyObject(this.gameObject.tag);

        //nullチェック
        if (_newEnemy == null) return;

        //ポップエフェクト
        var _effect = Instantiate(PopEffect, this.transform.position, Quaternion.identity);

        //取得した敵をインスタンス化し、マネージャーのリストに追加
        enemyManager.AddEnemy(Instantiate(_newEnemy, this.transform.position, new Quaternion()));
    }
}
