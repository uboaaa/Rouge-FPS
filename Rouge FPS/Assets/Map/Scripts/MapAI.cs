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

    //ミニマップ用オブジェクト
    public GameObject miniMap = null;

    //マップ初期化クラス
    private MapInitializer initializer = null;

    private int[,] m_nowMap;                    //マップチップ情報
    private int m_mapScale;                     //マップスケール
    private int m_roomId = -1;                  //現在の部屋ID(-1のときは通路のとき)

    private Dictionary<int, Room> m_roomList = new Dictionary<int, Room>();         //ID・部屋リスト
    private List<GameObject> m_spawnList = new List<GameObject>();                  //ID・スポナーのリスト

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
        var x = (target.transform.position.x) / 4.0f;
        var y = (target.transform.position.z) / 4.0f;

        //リスト内の部屋のどれかに入ったら
        int nowId = -1;
        foreach (KeyValuePair<int,Room> room in m_roomList)
        {
            //部屋の範囲内に対象がいるとき
            //※条件で座標誤差分を足し引きしている
            if(room.Value.Start.X - 1.0f < x && room.Value.End.X + 1.0f > x )
            {
                if(room.Value.Start.Y - 1.0f < y && room.Value.End.Y + 1.0f > y )
                {
                    nowId = room.Key;
                    break;
                }
            }
        }

        //部屋IDが違う場合のみ更新
        if (nowId != m_roomId)
        {
            m_roomId = nowId;

            // 部屋閉じる

            // 敵スポーン起動
            if (m_roomId != -1)
            {
                PopEnemy(m_enemyList[0].obj);
            }

            Debug.Log(m_roomId);
        }

    }

    private void LateUpdate()
    {
        //ミニマップ生成
        //画像34*34のマップチップで作成？
        //マップサイズに応じて変更？

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
        m_spawnList.Add(Instantiate(_enemy, new Vector3(position.X * m_mapScale, 1, position.Y * m_mapScale), new Quaternion()));
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
