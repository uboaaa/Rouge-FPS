using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//部屋タイプ
public enum Type
{
    R6x6,R10x10,R12x12,R6x10,R10x12,START,GOAL
}

//部屋タイプとオブジェクトの組み合わせ
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

    
    //部屋の親オブジェクト
    public GameObject m_parentRoom;

    //スタート部屋prefab
    public GameObject m_startPrefab;

    //ゴール部屋prefab
    public GameObject m_goalPrefab;

    //スタート、ゴール設定フラグ
    private bool m_setStart = false;
    private bool m_setGoal = false;

    //スタート部屋保存用オブジェクト
    private GameObject m_StartRoom;

    //初期化
    void Start()
    {
        Debug.Log("部屋タイプの数：" + m_roomTypeList.Length);
    }

    //==============================================================
    //部屋リセット処理
    //※前ステージがあった場合のみ削除する
    //===============================================================
    public void ResetRoom()
    {
        //子オブジェクトがあるかを確認
        if (m_parentRoom.transform.childCount > 0)
        {
            //あった場合、子オブジェクトのみ削除
            foreach(Transform childTrans in m_parentRoom.transform)
            {
                Destroy(childTrans.gameObject);
            }
        }

        //スタート、ゴール部屋のフラグもリセット
        m_setStart = false;
        m_setGoal = false;
    }

    //=============================================================
    //部屋生成処理
    // _data … 部屋データ参照渡し用、　_map … マップデータ参照渡し用
    //==============================================================
    public void GenerateRoom(ref Room _data, ref int[,] _map)
    {
        
        //ベース部屋の幅・高さを取得
        int width = _data.End.X - _data.Start.X + 1;
        int height = _data.End.Y - _data.Start.Y + 1;

        //使える部屋タイプを確認、リストにする
        List<Type> types;
        TypeCheck(width, height, out types);

        //可能な部屋タイプからランダムに１つ部屋データを取得
        List<RoomType> enableList = new List<RoomType>();
        foreach(Type type in types)
        {
            foreach (RoomType roomType in m_roomTypeList)
            {
                if (roomType.rtype == type) enableList.Add(roomType);       //可能な部屋をリストにする
            }
        }

        RoomType result = new RoomType();

        //スタート、ゴールが設定されている場合のみ
        if (m_setStart && m_setGoal) result = enableList.GetAtRandom_Bias();      //リストからランダムに取得

        //スタート、ゴール部屋を設定していない場合は優先して設定
        if (!m_setStart)
        {
            //スタート部屋を設定
            result.rtype = Type.START;
            result.roomObj = m_startPrefab;
            //startフラグも更新
            m_setStart = true;
        }
        else if(!m_setGoal)
        {
            //ゴール部屋を設定
            result.rtype = Type.GOAL;
            result.roomObj = m_goalPrefab;
            //goalフラグも更新
            m_setGoal = true;
        }

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

        //部屋の座標をランダムで設定
        int r_posX = Utility.GetRandomInt(_data.Start.X, _data.End.X - r_width);
        int r_posY = Utility.GetRandomInt(_data.Start.Y, _data.End.Y - r_height);

        Debug.Log(r_posX);
        Debug.Log(r_posY);

        //サイズをfor文で回して、マップデータに書き込む
        for (int x = 0; x < r_width; x++)
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
        GameObject _newRoom = Instantiate(result.roomObj, new Vector3(r_centX * MapInitializer.MAP_SCALE, 0, r_centY * MapInitializer.MAP_SCALE), rotation);

        //スタート部屋の場合は初期座標のために保存
        if (result.rtype == Type.START) m_StartRoom = _newRoom;

        //生成したオブジェクトをparentRoomの子オブジェクトにして、まとめる
        _newRoom.transform.SetParent(m_parentRoom.transform);

        //部屋データを書き換える
        _data = new Room(r_posX, r_posY, r_posX + r_width - 1, r_posY + r_height - 1);

    }

    //========================================================
    //部屋タイプを確認する関数
    //幅・高さから設定可能な部屋タイプをリストで返す
    //========================================================
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

    //===============================================
    //部屋タイプに応じた数値を返す処理
    //===============================================
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
            case Type.START:
                _w = 6;  _h = 6;
                break;
            case Type.GOAL:
                _w = 6;  _h = 6;
                break;
        }
    }

    //========================================
    //スタート地点ゲッター
    //========================================
    public Transform GetStartPosition()
    {
        //スタート地点を設定した子オブジェクトを取得
        Transform sPos = m_StartRoom.transform.Find("StartPosition");

        return sPos;
    }

}
