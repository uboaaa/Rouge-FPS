using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//シーン開始時のみ処理を行う
public class MapInitializer : MonoBehaviour
{
    //各種マップパラメータ
    [SerializeField]private int MAP_SIZE_X = 30;   //X軸のマップ最大サイズ
    [SerializeField]private int MAP_SIZE_Y = 20;   //Y軸のマップ最大サイズ

    [SerializeField]private int MAX_ROOM_NUMBER = 6;   //最大部屋数（これ以下の場合もある）
    [SerializeField]private int m_mapScale = 1;              //

    //初期座標
    //***初期座標をグローバルで返すようにする
    public static float g_spawn_posX;
    public static float g_spawn_posY;
    public static float g_spawn_posZ;
    public static float g_spawn_rotX;
    public static float g_spawn_rotY;
    public static float g_spawn_rotZ;

    //prefab
    private GameObject m_floorPrefab;     //床のオブジェクト
    private GameObject m_wallPrefab;      //壁のオブジェクト
    private GameObject m_celingPrefab;    //天井のオブジェクト


    private int[,] m_map;                 //マップ情報用の2次配列

    // 初期化
    void Start()
    {
        GenerateDungeon();
    }

    
    // ダンジョンマップ生成
    private void GenerateDungeon()
    {
        //階層数分あらかじめ生成後、テキストorCSVに出力

        //ジェネレーターから生成したデータを取得
        m_map = new DungeonGenerator().GenerateMap(MAP_SIZE_X, MAP_SIZE_Y, MAX_ROOM_NUMBER);

        //改行を入れてデータを整理
        string data = "";
        for(int y = 0; y < MAP_SIZE_Y; y++)
        {
            for(int x = 0; x < MAP_SIZE_X; x++)
            {
                data += m_map[x, y] == 0 ? "0" : m_map[x, y].ToString();
            }
            data += "\n";
        }
        Debug.Log(data);

        //床と壁のモデル読み込み
        m_floorPrefab = Resources.Load("Prefab/Floor") as GameObject;
        m_wallPrefab = Resources.Load("Prefab/Wall") as GameObject;
        m_celingPrefab = Resources.Load("Prefab/Celing") as GameObject;

        //データからオブジェクトを配置
        for(int y = 0; y < MAP_SIZE_Y; y++)
        {
            for(int x = 0; x < MAP_SIZE_X; x++)
            {
                if (m_map[x, y] >= 1)
                {
                    Instantiate(m_floorPrefab, new Vector3(x * m_mapScale, 0, y * m_mapScale), new Quaternion());
                }
                else
                {
                    Instantiate(m_wallPrefab, new Vector3(x * m_mapScale, 0, y * m_mapScale), new Quaternion());
                }

                Instantiate(m_celingPrefab, new Vector3(x * m_mapScale, 6, y * m_mapScale), new Quaternion());
            }
        }

        //プレイヤー出現座標を設定
        SponePlayer();
    }

    // プレイヤーの出現座標を設定
    private void SponePlayer()
    {

        Position position;
        do
        {
            var x = Utility.GetRandomInt(0, MAP_SIZE_X - 1);
            var y = Utility.GetRandomInt(0, MAP_SIZE_Y - 1);
            position = new Position(x, y);
        } while (m_map[position.X, position.Y] != 1);


        //***初期座標をグローバル変数に設定
        SetSpawnData(position);
    }

    private void SetSpawnData(Position pos)
    {
        g_spawn_posX = pos.X * m_mapScale;
        g_spawn_posY = 3;
        g_spawn_posZ = pos.Y * m_mapScale;
        g_spawn_rotX = 0;
        g_spawn_rotY = 0;
        g_spawn_rotZ = 0;
    }

    //初期地点取得関数
    //座標…"p"or回転…"r" + 各軸(x,y,z)を引数で指定
    //（例）x座標：px
    public static float GetSpawnData(string element)
    {
        float result = 0;
        switch (element)
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

}
