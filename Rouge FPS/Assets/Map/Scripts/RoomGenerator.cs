using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    R3x3,R6x6,R10x10,R3x6,R6x10
}

public class RoomType
{
    public Type rtype = Type.R3x3;
    public GameObject roomObj = null;
}

//部屋割り当てクラス
public class RoomGenerator : MonoBehaviour
{
    //マップ
    //***エディタ拡張で設定しやすくしておく！
    [SerializeField]
    private List<RoomType> m_roomTypeList = new List<RoomType>();

    //マップ初期化コンポーネント
    private MapInitializer initializer = null;
    //マップ配置データ
    private int[,] m_map;
    //マップスケール
    private int m_scale;
    //部屋リスト
    private Dictionary<int, Room> m_roomList = new Dictionary<int, Room>();

    //初期化
    void Start()
    {
        //マップ初期化コンポーネントを取得
        initializer = this.gameObject.GetComponent<MapInitializer>();

        //マップ配置データを取得
        m_map = initializer.GetMap();
        //マップスケールを取得
        m_scale = initializer.GetScale();
        //部屋リストを取得
        initializer.GetRoomList(out m_roomList);

        //部屋生成
        foreach (Room data in m_roomList.Values)
        {
            GenerateRoom(data);
        }
    }

    void GenerateRoom(Room _data)
    {
        //部屋の幅・高さを取得
        int width = _data.End.X - _data.Start.X - 1;
        int height = _data.End.Y - _data.Start.Y - 1;
        Debug.Log(height);

        //使える部屋タイプを確認
        List<Type> types;
        TypeCheck(width, height, out types);

        //可能な部屋タイプからランダムに１つ部屋データを取得
        List<RoomType> enableList = new List<RoomType>();
        foreach(Type type in types)
        {
            foreach(RoomType roomType in m_roomTypeList)
            {
                if (roomType.rtype == type) enableList.Add(roomType);       //可能な部屋をリストにする
            }
        }

        RoomType result = enableList.GetAtRandom();     //リストからランダムに取得

        //***マップ初期化コンポーネントを改良して組み込む、拡張もする
        //***サイズに合わせて座標をランダムに設定
        //***曲がり角ありの通路生成

    }

    //部屋タイプを確認する関数
    //幅・高さから設定可能な部屋タイプをリストで返す
    private void TypeCheck(int _w,int _h, out List<Type> _list)
    {
        //_listをインスタンス化
        _list = new List<Type>();

        //R3x3
        if (_w >= 3 && _h >= 3) _list.Add(Type.R3x3);
        //R6x6
        if (_w >= 6 && _h >= 6) _list.Add(Type.R6x6);
        //R10x10
        if (_w >= 10 && _h >= 10) _list.Add(Type.R10x10);
        //R3x6(or R6x3)
        if ((_w >= 3 && _h >= 6) || (_w >= 6 && _h >= 3)) _list.Add(Type.R3x6);
        //R6x10(or R10x6)
        if ((_w >= 6 && _h >= 10) || (_w >= 10 && _h >= 6)) _list.Add(Type.R6x10);

    }

}
