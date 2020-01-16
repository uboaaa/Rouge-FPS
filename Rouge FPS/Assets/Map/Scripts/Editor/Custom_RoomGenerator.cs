#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

//拡張エディターで敵をセットしやすくする

[CustomEditor(typeof(RoomGenerator))]
[CanEditMultipleObjects]
public class Custom_RoomGenerator : Editor
{
    SerializedProperty _start;
    SerializedProperty _goal;
    SerializedProperty _rooms;

    ReorderableList _reorderableList;
    
    private void OnEnable()
    {
        //表示用リスト
        _reorderableList = new ReorderableList(serializedObject, serializedObject.FindProperty("m_roomTypeList"));
        _reorderableList.drawElementCallback += (Rect rect, int index, bool selected, bool focused) =>
        {
            SerializedProperty property = _reorderableList.serializedProperty.GetArrayElementAtIndex(index);
            // PropertyFieldを使ってよしなにプロパティの描画を行う（PropertyDrawerを使っているのでそちらに移譲されます）
            EditorGUI.PropertyField(rect, property, GUIContent.none);
        };
        _reorderableList.drawHeaderCallback += rect =>
        {
            EditorGUI.LabelField(rect, "  RoomType  |  RoomObject  ");
        };

        
        //部屋まとめ用
        _rooms = serializedObject.FindProperty("m_parentRoom");
    }

    public override void OnInspectorGUI()
    {
        //各シリアライズオブジェクトの内容を更新
        serializedObject.Update();

        //スタート・ゴール部屋の描画を更新
        
        EditorGUILayout.PropertyField(_rooms);

        //リストの描画を更新
        _reorderableList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}

#endif