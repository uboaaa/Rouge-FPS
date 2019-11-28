using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//部屋割り当てクラス
public class RoomGenerator : MonoBehaviour
{
    //マップ初期化コンポーネント
    private MapInitializer initializer = null;

    //マップ配置データ
    private int[,] m_map;
    //部屋リスト
    private Dictionary<int, Room> m_roomList = new Dictionary<int, Room>();

    //初期化
    void Start()
    {
        //マップ初期化コンポーネントを取得
        initializer = this.gameObject.GetComponent<MapInitializer>();

        //マップ配置データを取得
        m_map = initializer.GetMap();
        //部屋リストを取得
        initializer.GetRoomList(out m_roomList);

        //部屋生成
        GenerateRoom();
    }

    void GenerateRoom()
    {

    }

}
