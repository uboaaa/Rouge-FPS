using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Group
{
    small,medium,large
}

[System.Serializable]
public class ObjectGroup
{
    public Group group = Group.small;
    public GameObject obj = null;
}

//マップ管理クラス
public class MapManager : MonoBehaviour
{
    public ObjectGroup[] m_enemyList;

    //仮プレイヤー
    public GameObject target = null;

    //マップ初期化クラス
    private MapInitializer initializer = null;
  
    private Selectable<int> m_nowMapID = new Selectable<int>(); //マップID
    private int[,] m_nowMap;                    //マップ配置データ
    private int m_roomId = -1;                  //現在の部屋ID(-1のときは通路のとき)
    private int m_startId = -1;                 //スタート部屋ID

    private Dictionary<int, Room> m_roomList = new Dictionary<int, Room>();         //ID・部屋リスト
    private List<GameObject> m_spawnList = new List<GameObject>();                  //ID・スポナーのリスト

    //初期化
    void Awake()
    {
        //マップ初期化クラス
        initializer = this.gameObject.GetComponent<MapInitializer>();
        
        //マップ切り替え時の処理を設定
        m_nowMapID.mChanged += value =>
        {
            //マップチップ情報を取得
            m_nowMap = MapInitializer.MAP_DATA;
            //スタート部屋IDを取得
            m_startId = initializer.StartID();
            //ID・部屋リストを取得
            initializer.GetRooms(out m_roomList);
        };
    }

    void Start()
    {
        
    }

    //更新
    void Update()
    {

        float x, y;

        //マップ選択、切替時に動くようにする
        if (MergeScenes.IsMerge())
        {
            //プレイヤー座標取得
            x = PlayerXYZ.GetPlayerPosition("px") / MapInitializer.MAP_SCALE;
            y = PlayerXYZ.GetPlayerPosition("pz") / MapInitializer.MAP_SCALE;

        }
        else
        {
            //仮プレイヤー座標取得
            x = (target.transform.position.x) / MapInitializer.MAP_SCALE;
            y = (target.transform.position.z) / MapInitializer.MAP_SCALE;
        }

        //リスト内の部屋のどれかに入ったら
        int nowId = -1;
        foreach (KeyValuePair<int, Room> room in m_roomList)
        {
            //部屋の範囲内に対象がいるとき
            //※条件で座標誤差分を足し引きしている
            if (room.Value.Start.X - 1.0f < x && room.Value.End.X + 1.0f > x)
            {
                if (room.Value.Start.Y - 1.0f < y && room.Value.End.Y + 1.0f > y)
                {
                    nowId = room.Key;
                    break;
                }
            }
        }

        //スタート部屋の場合は敵はでない
        if (nowId == m_startId) return;

        //部屋IDが違う場合のみ更新
        if (nowId != m_roomId)
        {
            m_roomId = nowId;

            // 部屋閉じる

            // 敵スポーン起動
            if (m_roomId != -1)
            {
                //面積10の数に応じて敵を生成
                int calc = m_roomList[m_roomId].calcArea();
                int num = calc / 10;
                for (int i = 0; i < num; i++)
                {
                    GameObject _enemy = SelectEnemyDifficult();
                    PopEnemy(_enemy);
                }
            }
            Debug.Log(m_roomId);
        }
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
        m_spawnList.Add(Instantiate(_enemy, new Vector3(position.X * MapInitializer.MAP_SCALE, 1, position.Y * MapInitializer.MAP_SCALE), new Quaternion()));
    }

    private GameObject SelectEnemyDifficult()
    {
        Group g = Group.small;

        List<GameObject> sameList = new List<GameObject>();
        foreach(ObjectGroup enemys in m_enemyList)
        {
            if (enemys.group == g)
            {
                sameList.Add(enemys.obj);
            }
        }

        return sameList.GetAtRandom();
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
        GameObject newSpawner = Instantiate(_enemy, new Vector3(position.X * MapInitializer.MAP_SCALE, 1, position.Y * MapInitializer.MAP_SCALE), new Quaternion());
        m_spawnList.Add(newSpawner);
    }

    public void TransNextMap()
    {
        m_nowMapID.Value += 1;
    }
}
