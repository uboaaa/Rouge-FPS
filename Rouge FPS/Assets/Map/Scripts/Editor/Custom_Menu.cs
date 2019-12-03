using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class Custom_Menu
{
    //部屋のひな型のプレハブまでのパス取得
    static string TEMPLATE_PATH = "Assets/Map/Resources/Prefab/Rooms/BaseRoom.prefab";

    [MenuItem("Assets/Generate_BaseRoom")]
    static public void GenerateBaseRoom()
    {
        var template = AssetDatabase.LoadAssetAtPath(TEMPLATE_PATH, typeof(GameObject)) as GameObject;

        var prefabPath = $"Assets/Map/Resources/Prefab/Rooms/Room.prefab";

        //インスタンス化
        var game = PrefabUtility.InstantiatePrefab(template) as GameObject;

        //プレハブ作成
        var prefab = PrefabUtility.SaveAsPrefabAsset(game, prefabPath);

        //リンク削除
        PrefabUtility.UnpackPrefabInstance(game, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);

        // シーンから削除  
        Object.DestroyImmediate(game);

        EditorUtility.SetDirty(prefab);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    } 
}
