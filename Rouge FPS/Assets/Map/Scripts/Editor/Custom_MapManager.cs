﻿#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

//拡張エディターで敵をセットしやすくする

[CustomEditor(typeof(MapManager))]
[CanEditMultipleObjects]
public class Custom_MapManager : Editor
{
    ReorderableList _reorderableList;
    //仮
    SerializedProperty _target;

    private void OnEnable()
    {
        //表示用リスト
        _reorderableList = new ReorderableList(serializedObject, serializedObject.FindProperty("m_enemyList"));
        _reorderableList.drawElementCallback += (Rect rect, int index, bool selected, bool focused) =>
        {
            SerializedProperty property = _reorderableList.serializedProperty.GetArrayElementAtIndex(index);
            // PropertyFieldを使ってよしなにプロパティの描画を行う（PropertyDrawerを使っているのでそちらに移譲されます）
            EditorGUI.PropertyField(rect, property, GUIContent.none);
        };
        _reorderableList.drawHeaderCallback += rect =>
        {
            EditorGUI.LabelField(rect, "  Group  |  Object  ");
        };

        //仮
        _target = serializedObject.FindProperty("target");
        
    }

    public override void OnInspectorGUI()
    {
        //仮
        serializedObject.Update();
        EditorGUILayout.PropertyField(_target);

        _reorderableList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}

#endif