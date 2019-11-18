using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//拡張エディターで敵をセットしやすくする

[CustomEditor(typeof(MapAI))]
[CanEditMultipleObjects]
public class Custom_MapAI : Editor
{
    bool isInitialized = false;
    bool folding_list = false;
    bool[] foldings;

    public override void OnInspectorGUI()
    {
        //MapAIを取得後、キャスト
        MapAI mapai = target as MapAI;

        List<EnemyGroup> groupList = mapai.enemyGroup;

        //追加
        if (!isInitialized) InitializeList(groupList.Count);

        if (folding_list = EditorGUILayout.Foldout(folding_list, "List"))
        {
            EditorGUI.indentLevel++;

            for(int i = 0; i < groupList.Count; i++)
            {
                EditorGUI.indentLevel++;

                if (foldings[i] = EditorGUILayout.Foldout(foldings[i], "Enemy_" + i))
                {
                    groupList[i].group= (Group)EditorGUILayout.EnumPopup("Group", groupList[i].group);
                    groupList[i].enemyObject= (GameObject)EditorGUILayout.ObjectField("EnemyObj",groupList[i].enemyObject, typeof(GameObject), true);

                    EditorGUILayout.BeginHorizontal();

                    //いっぱいまで空白を埋める
                    GUILayout.FlexibleSpace();

                    if (GUILayout.Button("Delete"))
                    {
                        groupList.RemoveAt(i);
                        InitializeList(i, groupList.Count);
                    }

                    EditorGUILayout.EndHorizontal();
                }

                EditorGUI.indentLevel--;
            }

            if (GUILayout.Button("Add"))
            {
                groupList.Add(new EnemyGroup());
                InitializeList(-1, groupList.Count);
            }

            //インデントを減らす
            EditorGUI.indentLevel--;
        }  
    }

    //Listの長さを初期化
    void InitializeList(int count)
    {
        foldings = new bool[count];
        isInitialized = true;
    }

    //指定した番号以外をキャッシュして初期化（i=-1の時は全てキャッシュして初期化）
    void InitializeList(int i, int count)
    {
        bool[] foldings_temp = foldings;
        foldings = new bool[count];

        for (int k = 0, j = 0; k < count; k++)
        {
            if (i == j) j++;
            if (foldings_temp.Length - 1 < j) break;
            foldings[k] = foldings_temp[j++];
        }
    }
}
