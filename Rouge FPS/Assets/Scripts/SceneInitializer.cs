using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//シーン開始時のみ処理を行う
public class SceneInitializer : MonoBehaviour
{
    //各種マップパラメータ
    public const int MAP_SIZE_X = 30;   //X軸のマップ最大サイズ
    public const int MAP_SIZE_Y = 20;   //Y軸のマップ最大サイズ

    public const int MAX_ROOM_NUMBER = 6;   //最大部屋数（これ以下の場合もある）
    public int m_mapScale = 1;              //

    public GameObject m_player;          //プレイヤー用オブジェクト
                                        //出現座標をマップ生成後に設定するために取得

    private GameObject m_floorPrefab;     //床のオブジェクト
    private GameObject m_wallPrefab;      //壁のオブジェクト

    private int[,] m_map;                 //マップ情報用の2次配列

    // 初期化
    void Start()
    {
        GenerateDungeon();

        //SponePlayer();
    }

    
    // ダンジョンマップ生成
    private void GenerateDungeon()
    {
        //ジェネレーターから生成したデータを取得
        m_map = new DungeonGenerator().GenerateMap(MAP_SIZE_X, MAP_SIZE_Y, MAX_ROOM_NUMBER);

        //改行を入れてデータを整理
        string data = "";
        for(int y = 0; y < MAP_SIZE_Y; y++)
        {
            for(int x = 0; x < MAP_SIZE_X; x++)
            {
                data += m_map[x, y] == 0 ? " " : "1";
            }
            data += "\n";
        }
        Debug.Log(data);

        //床と壁のモデル読み込み
        m_floorPrefab = Resources.Load("Prefab/Floor") as GameObject;
        m_wallPrefab = Resources.Load("Prefab/Wall") as GameObject;


        //データからオブジェクトを配置
        for(int y = 0; y < MAP_SIZE_Y; y++)
        {
            for(int x = 0; x < MAP_SIZE_X; x++)
            {
                if (m_map[x, y] == 1)
                {
                    Instantiate(m_floorPrefab, new Vector3(x*m_mapScale, 0, y*m_mapScale), new Quaternion());
                }
                else
                {
                    Instantiate(m_wallPrefab, new Vector3(x*m_mapScale, 0, y*m_mapScale), new Quaternion());
                }
            }
        }
    }

    // プレイヤーの出現座標を設定
    private void SponePlayer()
    {
        if (!m_player)
        {
            return;
        }

        Position position;
        do
        {
            var x = Utility.GetRandomInt(0, MAP_SIZE_X - 1);
            var y = Utility.GetRandomInt(0, MAP_SIZE_Y - 1);
            position = new Position(x, y);
        } while (m_map[position.X, position.Y] != 1);

        Debug.Log(m_player.transform.position.x);
        //m_player.transform.position = new Vector3(10, 3, 10);
        m_player.transform.position = new Vector3(position.X, 3, position.Y);
        Debug.Log(m_player.transform.position.x);
    }
}
