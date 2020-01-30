using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RoomGenerator))]

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
 

    public static int MAP_SIZE_X = 30;
    public static int MAP_SIZE_Y = 20;
    public static int MAP_SCALE = 1;
    public static int[,] MAP_DATA;

    //初期座標
    //***初期座標をグローバルで返すようにする
    private static bool g_spawn_enable = true;
    private static float g_spawn_posX = 0;
    private static float g_spawn_posY = 0;
    private static float g_spawn_posZ = 0;
    private static float g_spawn_rotX = 0;
    private static float g_spawn_rotY = 0;
    private static float g_spawn_rotZ = 0;

    //prefab
    //ダンジョンパーツの親オブジェクト
    //※リセット時に子オブジェクトを消す
    private GameObject m_parentParts;
    private GameObject m_floorPrefab;     //床のオブジェクト
    private GameObject m_wallPrefab;      //壁のオブジェクト
    private GameObject m_celingPrefab;    //天井のオブジェクト
    private GameObject m_cagePrefab;

    private GameObject m_bossRoomPrefab;

    //component/class
    private DungeonGenerator DG = null;
    private RoomGenerator RG = null;

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

        //親を取得
        m_parentParts = GameObject.Find("DungeonParts");

        //マップ切り替え時の処理を設定
        m_mapID.mChanged += value =>
        {
            //リセット
            ResetMap();
            if (m_mapID.Value % 5 != 0)
            {
                //マップ基盤生成
                GenerateBaseMap();
                //部屋生成
                GenerateRooms();
                //通路生成
                GeneratePass();
                //オブジェクト配置
                GenerateObject();
            }
            else
            {
                //ボス部屋生成
                GenerateBossMap();
            }
            //マネージャー設定
            //ReloadManager();
        };
    }

    // 初期化
    void Start()
    {
        //マップ1生成
        m_mapID.Value = 1;
    }

    //更新
    void Update()
    {
        ////簡易マップ移動
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    MoveNextMap();
        //}
    }

    private void FixedUpdate()
    {

    }

    //マップリセット処理
    private void ResetMap()
    {
        //==================
        //パーツを削除
        //==================
        //パーツの親オブジェクトを取得
        //m_parentParts = GameObject.Find("DungeonParts");

        //子オブジェクトがあったら全削除
        if (m_parentParts.transform.childCount > 0)
        {
            //***取得して削除する！
            foreach(Transform childTrans in m_parentParts.transform)
            {
                Destroy(childTrans.gameObject);
            }
        }

        //====================
        //部屋を削除
        //====================
        RG.ResetRoom();
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

    //ボス部屋用マップ生成
    private void GenerateBossMap()
    {
        //ボス用prefabを取得し、インスタス化
        m_bossRoomPrefab = Resources.Load("Prefab/Rooms/BossMap") as GameObject;
        GameObject _bossRoom = Instantiate(m_bossRoomPrefab, new Vector3(0, 0, 0), new Quaternion());
        //ダンジョンパーツ用親オブジェクトを親に設定
        _bossRoom.transform.SetParent(m_parentParts.transform);

        //初期座標用のオブジェクトを取得し、座標をセット
        Transform trans = _bossRoom.transform.Find("StartRoom/StartPosition");
        g_spawn_posX = trans.position.x;
        g_spawn_posY = trans.position.y;
        g_spawn_posZ = trans.position.z;
        g_spawn_rotX = 0;
        g_spawn_rotY = 0;
        g_spawn_rotZ = 0;

        //初期地点更新フラグ
        g_spawn_enable = true;

        //フェードイン開始
        MapFade.FadeIn();

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
        List<int> elementList = new List<int>();            //要素数リスト
        for(int i = 0; i < rooms_Value.Length; i++)         //※要素数をランダムで使用して、スタートとゴールが近くに生成される確率を減らす
        {
            elementList.Add(i);
        }

        do
        {
            //要素数リストが0の場合、部屋生成をやめる
            if (elementList.Count == 0) break;

            //要素数をランダムで取得
            int element = elementList.GetAtRandom();

            //取得した要素数の部屋を生成する
            RG.GenerateRoom(ref rooms_Value[element], ref m_map);

            //要素数リストから使用した要素数を削除
            elementList.Remove(element);

        } while (true);

        
        //２つの配列をDictionaryにまとめる
        Dictionary<int, Room> result = new Dictionary<int, Room>(rooms_Key.Length);
        for (int i = 0; i < rooms_Key.Length; i++)
        {
            result[rooms_Key[i]] = rooms_Value[i];
        }

        //Generatorに部屋データを反映させる
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

    }

    // オブジェクト配置
    private void GenerateObject()
    {
        //パーツの親オブジェクトを取得
        //m_parentParts = GameObject.Find("DungeonParts");

        //床と壁のモデル読み込み
        m_wallPrefab = Resources.Load("Prefab/DungeonParts/Wall") as GameObject;
        m_floorPrefab = Resources.Load("Prefab/DungeonParts/Floor") as GameObject;
        m_celingPrefab = Resources.Load("Prefab/DungeonParts/Celing") as GameObject;
        m_cagePrefab = Resources.Load("Prefab/DungeonParts/Cage") as GameObject;

        //余分な壁データを削除
        for (int y = 1; y < MAP_SIZE_Y - 1; y++)
        {
            for (int x = 1; x < MAP_SIZE_X - 1; x++)
            {
                if (m_map[x - 1, y] <= 0 && m_map[x + 1, y] <= 0 && m_map[x, y - 1] <= 0 && m_map[x, y + 1] <= 0)
                {
                    m_map[x, y] = -1;
                }
            }
        }

        //非同期前にマップデータを作る
        for (int y = 0; y < MAP_SIZE_Y; y++)
        {
            for (int x = 0; x < MAP_SIZE_X; x++)
            {
                //囲い部分の壁を削除
                if (x == 0 || y == 0) m_map[x, y] = -1;
                if (x == MAP_SIZE_X - 1 || y == MAP_SIZE_Y - 1) m_map[x, y] = -1;

                //グローバルのマップデータを更新
                MAP_DATA[x, y] = m_map[x, y];
            }
        }

        //非同期ロード版
        InstatiateObjects(m_floorPrefab, m_celingPrefab, m_wallPrefab);

        //プレイヤー出現座標を設定
        SpawnPlayer();
    }

    public void InstatiateObjects(GameObject floor, GameObject celling, GameObject wall)
    {
        StartCoroutine(InstatiateObjectsInternal(floor, celling, wall));
    }

    private IEnumerator InstatiateObjectsInternal(GameObject floor, GameObject celling, GameObject wall)
    {
        
        //10msecで一度次のフレームへ
        float goNextFlameTime = Time.realtimeSinceStartup + 0.01f;
        

        //データからオブジェクトを配置
        for (int y = 0; y < MAP_SIZE_Y; y++)
        {
            for (int x = 0; x < MAP_SIZE_X; x++)
            {
                if (Time.realtimeSinceStartup >= goNextFlameTime)
                {
                    yield return null;
                    goNextFlameTime = Time.realtimeSinceStartup + 0.01f;
                }


                //オブジェクト生成
                if (m_map[x, y] == 3)
                {
                    GameObject _newCage = Instantiate(m_cagePrefab, new Vector3(x * MAP_SCALE, 0, y * MAP_SCALE), new Quaternion());
                    _newCage.transform.SetParent(m_parentParts.transform);
                }
                if (m_map[x, y] == 2)
                {
                    //通路
                    GameObject _newFloor = Instantiate(m_floorPrefab, new Vector3(x * MAP_SCALE, 0, y * MAP_SCALE), new Quaternion());
                    _newFloor.transform.SetParent(m_parentParts.transform);
                }

                if (m_map[x, y] == 0)
                {
                    //壁
                    GameObject _newWall = Instantiate(m_wallPrefab, new Vector3(x * MAP_SCALE, 5, y * MAP_SCALE), new Quaternion());
                    _newWall.transform.SetParent(m_parentParts.transform);
                }
            }
        }

        //天井
        GameObject _newCeling = Instantiate(m_celingPrefab, new Vector3((MAP_SIZE_X / 2) * MAP_SCALE, 8, (MAP_SIZE_Y / 2) * MAP_SCALE), new Quaternion());
        _newCeling.transform.localScale = new Vector3(MAP_SIZE_X * MAP_SCALE, 1, MAP_SIZE_Y * MAP_SCALE);
        _newCeling.transform.SetParent(m_parentParts.transform);

        //フェードイン開始
        MapFade.FadeIn();
    }

    // プレイヤーの出現座標を設定
    private void SpawnPlayer()
    {
        Transform trans = RG.GetStartPosition();
        g_spawn_posX = trans.position.x;
        g_spawn_posY = trans.position.y;
        g_spawn_posZ = trans.position.z;
        g_spawn_rotX = 0;
        g_spawn_rotY = 0;
        g_spawn_rotZ = 0;
        
        //初期地点更新フラグ
        g_spawn_enable = true;
    }

    //マネージャーを更新
    private void ReloadManager()
    {

    }

    //初期地点の更新フラグの取得関数
    public static bool GetSpawnEnable()
    {
        bool spawn = g_spawn_enable;
        if (g_spawn_enable == true)
        {
            g_spawn_enable = false;
        }
        return spawn;
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
