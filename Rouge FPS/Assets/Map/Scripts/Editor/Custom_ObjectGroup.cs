#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//拡張エディターで敵をセットしやすくする

[CustomPropertyDrawer(typeof(ObjectGroup))]
public class Custom_ObjectGroup : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // 表示幅
        float[] widthes = { position.width * 0.4f, position.width * 0.5f};

        if (property != null)
        {
            position.width = widthes[0];
            EditorGUI.PropertyField(position, property.FindPropertyRelative("group"), GUIContent.none); // フィールド名を指定

            position.x += position.width;
            position.width = widthes[1];
            EditorGUI.PropertyField(position, property.FindPropertyRelative("obj"), GUIContent.none);
        }
    }
}

#endif