using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    R6x6,R10x10,R12x12,R6x10,R10x12
}

[System.Serializable]
public class RoomType
{
    public Type rtype = Type.R6x6;
    public GameObject roomObj = null;
}

//部屋割り当てクラス
public class RoomGenerator : MonoBehaviour
{
    //部屋タイプリスト
    //***エディタ拡張で設定しやすくしておく！
    public RoomType[] m_roomTypeList;

    //マップ配置データ
    private int[,] m_map;
    //部屋リスト
    private Dictionary<int, Room> m_roomList = new Dictionary<int, Room>();

    //初期化
    void Start()
    {
        
    }

    public void GenerateRoom(ref Room _data, ref int[,] _map)
    {
        
        //ベース部屋の幅・高さを取得
        int width = _data.End.X - _data.Start.X + 1;
        int height = _data.End.Y - _data.Start.Y + 1;

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

        RoomType result = enableList.GetAtRandom();                         //リストからランダムに取得

        //サイズに応じてマップに配置、反映
        int r_width;                                            //部屋の幅・高さ
        int r_height;
        TypeParam(result.rtype, out r_width, out r_height);     //取得した部屋パラメータを取得

        //6x10,10x12のときは回転を考慮
        Quaternion rotation = new Quaternion();
        if (result.rtype==Type.R6x10 || result.rtype == Type.R10x12)
        {
            //横幅の方が長い時は90°回転、取得したパラメータを入れ替える
            if (width > height)
            {
                rotation = Quaternion.AngleAxis(90, new Vector3(0, 1, 0));
                int tmp = r_width;
                r_width = r_height;
                r_height = tmp;
            }
        }

        string s = "start(" + _data.Start.X + "," + _data.Start.Y + "),end(" + _data.End.X + "," + _data.End.Y + ")";
        Debug.Log(s);
        string w = "width:" + r_width + ",height:" + r_height;
        Debug.Log(w);

        //サイズをfor文で回して、マップデータに書き込む
        int r_posX = Utility.GetRandomInt(_data.Start.X, _data.End.X - r_width);
        int r_posY = Utility.GetRandomInt(_data.Start.Y, _data.End.Y - r_height);

        Debug.Log(r_posX);
        Debug.Log(r_posY);

        for(int x = 0; x < r_width; x++)
        {
            for(int y = 0; y < r_height; y++)
            {
                _map[r_posX + x, r_posY + y] = 1;
            }
        }

        
        //部屋オブジェクトをインスタンス化
        //***初期化コンポーネント内でするか、初期化内にリスト作成しそこに追加する
        //***削除しやすくするため！
        float r_centX = (float)r_posX + (float)(r_width - 1) / 2.0f;         //部屋の中心の座標を取得
        float r_centY = (float)r_posY + (float)(r_height - 1) / 2.0f;
        Instantiate(result.roomObj, new Vector3(r_centX * MapInitializer.MAP_SCALE, 0, r_centY * MapInitializer.MAP_SCALE), rotation);

        //部屋データを書き換える
        _data = new Room(r_posX, r_posY, r_posX + r_width - 1, r_posY + r_height - 1);

        //***曲がり角ありの通路生成

    }

    //部屋タイプを確認する関数
    //幅・高さから設定可能な部屋タイプをリストで返す
    private void TypeCheck(int _w,int _h, out List<Type> _list)
    {
        //_listをインスタンス化
        _list = new List<Type>();

        //Debug.Log(_w);
        //Debug.Log(_h);

        //R6x6
        if (_w >= 6 && _h >= 6) _list.Add(Type.R6x6);
        //R10x10
        if (_w >= 10 && _h >= 10) _list.Add(Type.R10x10);
        //R12x12
        if (_w >= 12 && _h >= 12) _list.Add(Type.R12x12);
        //R6x10(or R6x10)
        if ((_w >= 6 && _h >= 10) || (_w >= 10 && _h >= 6)) _list.Add(Type.R6x10);
        //R10x12(or R12x10)
        if ((_w >= 10 && _h >= 12) || (_w >= 12 && _h >= 10)) _list.Add(Type.R10x12);

    }

    //部屋タイプに応じた数値を返す
    private void TypeParam(Type _type,out int _w,out int _h)
    {
        //仮おき
        _w = 0;
        _h = 0;
        
        //部屋タイプごとに設定
        switch (_type)
        {
            case Type.R6x6:
                _w = 6; _h = 6;
                break;
            case Type.R10x10:
                _w = 10; _h = 10;
                break;
            case Type.R12x12:
                _w = 12; _h = 12;
                break;
            case Type.R6x10:
                _w = 6; _h = 10;
                break;
            case Type.R10x12:
                _w = 10; _h = 12;
                break;
        }
    }

}
