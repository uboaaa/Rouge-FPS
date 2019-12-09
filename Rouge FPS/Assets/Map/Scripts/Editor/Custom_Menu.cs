using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class Custom_Menu
{
    //部屋のひな型のプレハブまでのパス取得
    static string R6x6_PATH = "Assets/Map/Resources/Prefab/Rooms/Base/R6x6.prefab";
    static string R10x10_PATH = "Assets/Map/Resources/Prefab/Rooms/Base/R10x10.prefab";
    static string R12x12_PATH = "Assets/Map/Resources/Prefab/Rooms/Base/R12x12.prefab";
    static string R6x10_PATH= "Assets/Map/Resources/Prefab/Rooms/Base/R6x10.prefab";
    static string R10x12_PATH = "Assets/Map/Resources/Prefab/Rooms/Base/R10x12.prefab";

    [MenuItem("Assets/Generate_BaseRoom/R6x6")]
    static public void GenerateR6x6()
    {
        var template = AssetDatabase.LoadAssetAtPath(R6x6_PATH, typeof(GameObject)) as GameObject;

        var prefabPath = $"Assets/Map/Resources/Prefab/Rooms/R6x6";

        var current = Directory.GetCurrentDirectory();
        Debug.Log(current);

        CreatePrefab(prefabPath, template);

    }

    [MenuItem("Assets/Generate_BaseRoom/R10x10")]
    static public void GenerateR10x10()
    {
        var template = AssetDatabase.LoadAssetAtPath(R10x10_PATH, typeof(GameObject)) as GameObject;

        var prefabPath = $"Assets/Map/Resources/Prefab/Rooms/R10x10";

        CreatePrefab(prefabPath, template);

    }

    [MenuItem("Assets/Generate_BaseRoom/R12x12")]
    static public void GenerateR12x12()
    {
        var template = AssetDatabase.LoadAssetAtPath(R12x12_PATH, typeof(GameObject)) as GameObject;

        var prefabPath = $"Assets/Map/Resources/Prefab/Rooms/R12x12";

        CreatePrefab(prefabPath, template);

    }

    [MenuItem("Assets/Generate_BaseRoom/R6x10")]
    static public void GenerateR6x10()
    {
        var template = AssetDatabase.LoadAssetAtPath(R6x10_PATH, typeof(GameObject)) as GameObject;

        var prefabPath = $"Assets/Map/Resources/Prefab/Rooms/R6x10";

        CreatePrefab(prefabPath, template);

    }

    [MenuItem("Assets/Generate_BaseRoom/R10x12")]
    static public void GenerateR10x12()
    {
        var template = AssetDatabase.LoadAssetAtPath(R10x12_PATH, typeof(GameObject)) as GameObject;

        var prefabPath = $"Assets/Map/Resources/Prefab/Rooms/R10x12";

        CreatePrefab(prefabPath, template);

    }

    static public void CreatePrefab(string path,GameObject template)
    {
        int i = 1;
        var targetPath = path + ".prefab";
        while (File.Exists(targetPath))
        {
            targetPath = $"{path}({i++}).prefab";
        }

        //インスタンス化
        var game = PrefabUtility.InstantiatePrefab(template) as GameObject;

        //プレハブ作成
        var prefab = PrefabUtility.SaveAsPrefabAsset(game, targetPath);

        //リンク削除
        PrefabUtility.UnpackPrefabInstance(game, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);

        // シーンから削除  
        Object.DestroyImmediate(game);

        EditorUtility.SetDirty(prefab);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
