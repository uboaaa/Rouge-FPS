using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Group
{
    easy,normal,hard
}

[System.Serializable]
public class ObjectGroup
{
    public Group group = Group.easy;
    public GameObject obj = null;
}

//マップ管理AIクラス
public class MapAI : MonoBehaviour
{
    public ObjectGroup[] m_enemyList;

    //仮プレイヤー
    public GameObject target = null;

    private MapInitializer initializer = null;

    private int[,] m_nowMap;                    //マップチップ情報
    private int m_mapScale;                     //マップスケール
    private int m_roomId = -1;                  //現在の部屋ID(-1のときは通路のとき)

    //private GameObject m_spawner_Lizard;        //リザード

    private Dictionary<int, Room> m_roomList = null;        //ID・部屋リスト
    private List<GameObject> m_spawnList = null; //ID・スポナーのリスト

    //
    void Start()
    {
        //マップ
        initializer = this.GetComponent<MapInitializer>();

        //マップチップ情報を取得
        m_nowMap = initializer.GetMap();
        //マップスケールを取得
        m_mapScale = initializer.GetScale();
        //ID・部屋リストを取得
        initializer.GetRoomList(out m_roomList);
    }

    //
    void Update()
    {
        
        //
    }

    //
    private void FixedUpdate()
    {
        //仮プレイヤー座標取得
        var x = target.transform.position.x / 4.0f;
        var y = target.transform.position.z / 4.0f;

        //IDリセット
        m_roomId = -1;

        //リスト内の部屋のどれかに入ったら
        foreach (KeyValuePair<int,Room> room in m_roomList)
        {
            //部屋の範囲内に対象がいるとき
            if(room.Value.Start.X < x && room.Value.End.X > x)
            {
                if(room.Value.Start.Y < y && room.Value.End.Y > y)
                {
                    m_roomId = room.Key;
                    break;
                }
            }
        }

        if (m_roomId != -1)
        {
            Debug.Log(m_roomId);
        }

        //部屋閉じる

        //敵スポーン起動

    }

    //敵ランダムポップ関数
    //※引数：設定する敵
    private void PopEnemy(GameObject _enemy)
    {
        Position position;
        do
        {
            var x = Utility.GetRandomInt(m_roomList[m_roomId].Start.X, m_roomList[m_roomId].End.X - 1);
            var y = Utility.GetRandomInt(m_roomList[m_roomId].Start.Y, m_roomList[m_roomId].End.Y - 1);
            position = new Position(x, y);
        } while (m_nowMap[position.X, position.Y] != 1);

        //リストに追加
        GameObject newEnemy = Instantiate(_enemy, new Vector3(position.X * m_mapScale, 1, position.Y * m_mapScale), new Quaternion());
        m_spawnList.Add(newEnemy);
    }

    // 敵スポナーの設置関数
    //※引数：設定する敵
    private void SetSpawner(GameObject _enemy)
    {
        Position position;
        do
        {
            var x = Utility.GetRandomInt(m_roomList[m_roomId].Start.X, m_roomList[m_roomId].End.X - 1);
            var y = Utility.GetRandomInt(m_roomList[m_roomId].Start.Y, m_roomList[m_roomId].End.Y - 1);
            position = new Position(x, y);
        } while (m_nowMap[position.X, position.Y] != 1);

        //リストに追加
        GameObject newSpawner = Instantiate(_enemy, new Vector3(position.X * m_mapScale, 1, position.Y * m_mapScale), new Quaternion());
        m_spawnList.Add(newSpawner);
    }
    
}
