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
    ReorderableList _reorderableList;

    private void OnEnable()
    {
        //表示用リスト
        _reorderableList = new ReorderableList(serializedObject, serializedObject.FindProperty("m_enemyGroups"));
        _reorderableList.drawElementCallback += (Rect rect, int index, bool selected, bool focused) =>
        {
            SerializedProperty property = _reorderableList.serializedProperty.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(rect, property, GUIContent.none);
        };

        _reorderableList.drawHeaderCallback += rect =>
        {
            EditorGUI.LabelField(rect, "EnemyLevel | EnemyObject");
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        _reorderableList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}

#endif