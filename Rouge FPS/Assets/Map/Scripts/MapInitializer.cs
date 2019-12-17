using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RoomGenerator))]
[RequireComponent(typeof(MapManager))]

//マップ生成クラス
//シーン開始時のみ処理を行う
public class MapInitializer : MonoBehaviour
{
    //static設定用の変数
    [SerializeField] private int INS_MAP_SIZE_X = 30;   //X軸のマップ最大サイズ
    [SerializeField] private int INS_MAP_SIZE_Y = 20;   //Y軸のマップ最大サイズ
    [SerializeField] private int INS_MAP_SCALE = 1;     //

    //各種マップパラメータ
    [SerializeField] private int MAX_ROOM_NUMBER = 6;   //最大部屋数（これ以下の場合もある）
    [SerializeField] private int MAX_MAP_NUMBER = 3;    //最大マップ数

    public static int MAP_SIZE_X = 30;
    public static int MAP_SIZE_Y = 20;
    public static int MAP_SCALE = 1;
    public static int[,] MAP_DATA;

    //初期座標
    //***初期座標をグローバルで返すようにする
    private static float g_spawn_posX = 0;
    private static float g_spawn_posY = 0;
    private static float g_spawn_posZ = 0;
    private static float g_spawn_rotX = 0;
    private static float g_spawn_rotY = 0;
    private static float g_spawn_rotZ = 0;

    //prefab
    private GameObject m_floorPrefab;     //床のオブジェクト
    private GameObject m_wallPrefab;      //壁のオブジェクト
    private GameObject m_celingPrefab;    //天井のオブジェクト

    //component/class
    private DungeonGenerator DG = null;
    private RoomGenerator RG = null;
    private MapManager MM = null;

    private int[,] m_map;                   //マップ配置データの2次配列
    private int m_startId = -1;             //スタート地点の部屋ID
    private Selectable<int> m_mapID = new Selectable<int>();


    private void Awake()
    {
        //static変数を設定
        MAP_SIZE_X = INS_MAP_SIZE_X;
        MAP_SIZE_Y = INS_MAP_SIZE_Y;
        MAP_SCALE = INS_MAP_SCALE;
        MAP_DATA = new int[MAP_SIZE_X, MAP_SIZE_Y];

        //ダンジョン生成クラス取得
        DG = new DungeonGenerator();
        //部屋生成コンポーネント取得
        RG = this.gameObject.GetComponent<RoomGenerator>();
        MM = this.gameObject.GetComponent<MapManager>();

        //マップ切り替え時の処理を設定
        m_mapID.mChanged += value =>
        {
            //マップ基盤生成
            GenerateBaseMap();
            //部屋生成
            GenerateRooms();
            //通路生成
            GeneratePass();
            //オブジェクト配置
            GenerateObject();
            //マネージャー設定
            ReloadManager();
        };
    }

    // 初期化
    void Start()
    {
        //マップ1生成
        m_mapID.Value = 1;

        //***初期スタート部屋IDが他コンポーネント内スタートで取得しているので、Update内で行う
        //***ステージ切替時のみ関数使用するようにする
    }

    //更新
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    //ダンジョンマップ生成
    private void GenerateBaseMap()
    {
        //階層数分あらかじめ生成後、テキストorCSVに出力

        //ダンジョンジェネレータークラスを取得
        DG = new DungeonGenerator();

        //ベースマップ（部屋のみのマップ）を取得
        m_map = DG.GenerateBaseMap(MAP_SIZE_X, MAP_SIZE_Y, MAX_ROOM_NUMBER);
        
    }

    //部屋生成
    private void GenerateRooms()
    {
        //ID・部屋のリストを取得
        var dic = DG.GetRooms();
        //配列にコピー
        int[] rooms_Key = new int[dic.Keys.Count];
        Room[] rooms_Value = new Room[dic.Values.Count];
        dic.Keys.CopyTo(rooms_Key, 0);
        dic.Values.CopyTo(rooms_Value, 0);

        //用意された部屋を決定
        for (int i=0;i<rooms_Value.Length; i++)
        {
            RG.GenerateRoom(ref rooms_Value[i], ref m_map);
        }

        //
        Dictionary<int, Room> result = new Dictionary<int, Room>(rooms_Key.Length);
        for(int i=0;i<rooms_Key.Length;i++)
        {
            result[rooms_Key[i]] = rooms_Value[i];
        }

        //
        DG.SetRooms(result);

        //確認用
        string data = "";
        for (int i = 0; i < MAP_SIZE_X; i++)
        {
            for (int j = 0; j < MAP_SIZE_Y; j++)
            {
                data += m_map[i, j] == 0 ? "0" : m_map[i, j].ToString();
            }
            data += "\n";
        }
        Debug.Log(data);
    }

    //通路生成
    private void GeneratePass()
    {
        m_map = DG.CompPassMap(m_map);

        //改行を入れてデータを整理
        string data = "";
        for (int y = 0; y < MAP_SIZE_Y; y++)
        {
            for (int x = 0; x < MAP_SIZE_X; x++)
            {
                data += m_map[x, y] == 0 ? "0" : m_map[x, y].ToString();
            }
            data += "\n";
        }
        Debug.Log(data);

    }

    // オブジェクト配置
    private void GenerateObject()
    {
        //床と壁のモデル読み込み
        m_wallPrefab = Resources.Load("Prefab/DungeonParts/Wall") as GameObject;
        m_floorPrefab = Resources.Load("Prefab/DungeonParts/Floor") as GameObject;
        m_celingPrefab = Resources.Load("Prefab/DungeonParts/Celing") as GameObject;

        //余分な壁データを削除
        for(int y = 1;y<MAP_SIZE_Y - 1; y++)
        {
            for (int x = 1; x < MAP_SIZE_X - 1; x++)
            {
                if(m_map[x-1,y] <= 0 && m_map[x+1,y] <= 0 && m_map[x,y-1] <= 0 && m_map[x, y + 1] <= 0)
                {
                    m_map[x, y] = -1;
                }
            }
        }

        //データからオブジェクトを配置
        for (int y = 0; y < MAP_SIZE_Y; y++)
        {
            for (int x = 0; x < MAP_SIZE_X; x++)
            {
                //囲い部分の壁を削除
                if (x == 0 || y == 0) m_map[x, y] = -1;
                if (x == MAP_SIZE_X - 1 || y == MAP_SIZE_Y - 1) m_map[x, y] = -1;

                //オブジェクト生成
                if (m_map[x, y] == 2)
                {
                    //通路
                    Instantiate(m_floorPrefab, new Vector3(x * MAP_SCALE, 0, y * MAP_SCALE), new Quaternion());
                }

                if (m_map[x, y] >= 1)
                {
                    //天井
                    Instantiate(m_celingPrefab, new Vector3(x * MAP_SCALE, 6, y * MAP_SCALE), new Quaternion());
                }
                
                if (m_map[x, y] == 0)
                {
                    //壁
                    Instantiate(m_wallPrefab, new Vector3(x * MAP_SCALE, 5, y * MAP_SCALE), new Quaternion());
                }

                //グローバルのマップデータを更新
                MAP_DATA[x, y] = m_map[x, y];
            }
        }

        //プレイヤー出現座標を設定
        SpawnPlayer();
    }

    

    // プレイヤーの出現座標を設定
    private void SpawnPlayer()
    {

        Position position;
        //最小部屋とそのIDを取得
        Room small = null;
        m_startId = DG.SmallistRoom(out small);

        do
        {
            var x = Utility.GetRandomInt(small.Start.X, small.End.X - 1);
            var y = Utility.GetRandomInt(small.Start.Y, small.End.Y - 1);
            position = new Position(x, y);
        } while (m_map[position.X, position.Y] != 1);


        //初期座標をグローバル変数に設定
        g_spawn_posX = position.X * MAP_SCALE;
        g_spawn_posY = 1;
        g_spawn_posZ = position.Y * MAP_SCALE;
        g_spawn_rotX = 0;
        g_spawn_rotY = 0;
        g_spawn_rotZ = 0;
    }

    //マネージャーを更新
    private void ReloadManager()
    {

        MM.TransNextMap();
    }

    
    //初期地点取得関数
    //座標…"p"or回転…"r" + 各軸(x,y,z)を引数で指定
    //（例）x座標：px
    public static float GetSpawnData(string _element)
    {
        float result = 0;
        switch (_element)
        {
            case "px":
                result = g_spawn_posX;
                break;
            case "py":
                result = g_spawn_posY;
                break;
            case "pz":
                result = g_spawn_posZ;
                break;
            case "rx":
                result = g_spawn_rotX;
                break;
            case "ry":
                result = g_spawn_rotY;
                break;
            case "rz":
                result = g_spawn_rotZ;
                break;
            default:
                break;
        }

        return result;
    }

    public int[,] GetMap()
    {
        return m_map;
    }

    public int GetScale()
    {
        return MAP_SCALE;
    }

    public int GetSizeX()
    {
        return MAP_SIZE_X;
    }

    public int GetSizeY()
    {
        return MAP_SIZE_Y;
    }

    public int StartID()
    {
        return m_startId;
    }

    public void GetRooms(out Dictionary<int,Room> _roomList)
    {
        _roomList = new Dictionary<int, Room>();
        _roomList.AddRange(DG.GetRooms());
    }

    public void MoveNextMap()
    {
        m_mapID.Value += 1;
    }

}
