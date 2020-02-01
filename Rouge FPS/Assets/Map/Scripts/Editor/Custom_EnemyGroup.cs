#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(EnemyGroup))]
public class Custom_EnemyGroup : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        float[] widthes = { position.width * 0.4f, position.width * 0.6f };
        float height = position.height * 0.7f;

        if(property != null)
        {
            position.width = widthes[0];
            EditorGUI.PropertyField(position, property.FindPropertyRelative("level"), GUIContent.none); // フィールド名を指定

            position.x += position.width;
            position.width = widthes[1];
            position.height = height;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("obj"), GUIContent.none);
        }
    }
}

#endif