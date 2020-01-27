#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(EnemyManager))]
[CanEditMultipleObjects]
public class Custom_EnemyManager : Editor
{
    ReorderableList _groundList;
    ReorderableList _airList;

    SerializedProperty _popCount;
    SerializedProperty _popInterval;

    private void OnEnable()
    {
        //地上用リスト
        _groundList = new ReorderableList(serializedObject, serializedObject.FindProperty("m_groundGroups"));
        _groundList.drawElementCallback += (Rect rect, int index, bool selected, bool focused) =>
        {
            SerializedProperty property = _groundList.serializedProperty.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(rect, property, GUIContent.none);
        };
        _groundList.drawHeaderCallback += rect =>
        {
            EditorGUI.LabelField(rect, "地上の敵リスト");
        };

        //空中用リスト
        _airList = new ReorderableList(serializedObject, serializedObject.FindProperty("m_airGroups"));
        _airList.drawElementCallback += (Rect rect, int index, bool selected, bool focused) =>
        {
            SerializedProperty property = _airList.serializedProperty.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(rect, property, GUIContent.none);
        };
        _airList.drawHeaderCallback += rect =>
        {
            EditorGUI.LabelField(rect, "空中の敵リスト");
        };

        //出現数
        _popCount = serializedObject.FindProperty("m_PopMax");

        //出現間隔
        _popInterval = serializedObject.FindProperty("m_PopInterval");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_popCount);
        EditorGUILayout.PropertyField(_popInterval);

        _groundList.DoLayoutList();
        _airList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}

#endif