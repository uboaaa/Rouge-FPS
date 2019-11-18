using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Group
{
    easy,normal,hard
}

public class EnemyGroup
{
    public Group group = Group.easy;
    public GameObject enemyObject = null;
}

//マップ管理AIクラス
public class MapAI : MonoBehaviour
{
    public List<EnemyGroup> m_enemyList = new List<EnemyGroup>();

    private MapInitializer initializer = null;

    private int[,] m_nowMap;                    //マップチップ情報
    private int m_mapScale;                     //マップスケール
    private int m_roomId = -1;                  //現在の部屋ID(-1のときは通路のとき)

    //private GameObject m_spawner_Lizard;        //リザード

    private Dictionary<int, Room> m_roomList = null;        //ID・部屋リスト
    private Dictionary<int, GameObject> m_spawnList = null; //ID・スポナーのリスト

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

        //リストに初期敵を設定
        m_enemyList.Add(new EnemyGroup());
    }

    //
    void Update()
    {
        
        //
    }

    //
    private void FixedUpdate()
    {
        //リスト内の部屋のどれかに入ったら
        foreach(KeyValuePair<int,Room> room in m_roomList)
        {

        }

        //部屋閉じる

        //敵スポーン起動

    }

    //敵ランダムポップ関数
    //※引数：設定する敵
    private void PopEnemy(GameObject _enemy)
    {

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

        GameObject newSpawner = Instantiate(_enemy, new Vector3(position.X * m_mapScale, 1, position.Y * m_mapScale), new Quaternion());
    }

    //Inspector拡張用getter/setter
    public List<EnemyGroup> enemyGroup { get { return m_enemyList; } set { m_enemyList = value; } }

}
