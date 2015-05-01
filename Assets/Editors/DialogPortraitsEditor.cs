using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(DialogPortraits))]
[CanEditMultipleObjects]
public class DialogPortraitsEditor : Editor
{
    SerializedProperty iconsProp;
    private bool showContent  = true;
  

    void OnEnable()
    {
        iconsProp = serializedObject.FindProperty("icons");
    }
     public override void OnInspectorGUI()
     {
         serializedObject.Update();

         EditorGUI.indentLevel += 1;
         drawDictionary();
         EditorGUI.indentLevel -= 1;

         serializedObject.ApplyModifiedProperties();
     }

    void drawDictionary()
    {
        showContent = EditorGUILayout.Foldout(showContent, "Portraits");

        SerializedProperty keys = iconsProp.FindPropertyRelative("keys");
        SerializedProperty values = iconsProp.FindPropertyRelative("values");

        if (showContent)
        {         
            GUIStyle headerStyle = new GUIStyle();
            headerStyle.alignment = TextAnchor.MiddleCenter;
            int curremntIndent = EditorGUI.indentLevel;

            EditorGUI.indentLevel = 0;
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("ID", headerStyle, GUILayout.MaxWidth(Screen.width / 3 - 10));
            EditorGUILayout.LabelField("Texture", headerStyle, GUILayout.MaxWidth(Screen.width / 3 - 10));
            EditorGUILayout.LabelField("Sign", headerStyle, GUILayout.MaxWidth(Screen.width / 3 - 10));

            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel = curremntIndent;
           // 

            for (int i = 0; i < keys.arraySize; i++)
            {

                SerializedProperty texture = values.GetArrayElementAtIndex(i).FindPropertyRelative("texture");
                SerializedProperty sign = values.GetArrayElementAtIndex(i).FindPropertyRelative("sign");

                EditorGUILayout.BeginHorizontal();               
                EditorGUILayout.PropertyField(keys.GetArrayElementAtIndex(i), GUIContent.none);

                EditorGUI.indentLevel = 0;
                EditorGUILayout.PropertyField(texture, GUIContent.none);
                EditorGUILayout.PropertyField(sign, GUIContent.none);
                EditorGUI.indentLevel = curremntIndent;

                if (GUILayout.Button(new GUIContent("-","Remove")))
                {
                    keys.DeleteArrayElementAtIndex(i);
                    values.DeleteArrayElementAtIndex(i);
                    --i;
                    continue;
                }
                                
                EditorGUILayout.EndHorizontal();                
                
                if (keys.GetArrayElementAtIndex(i).stringValue == "")
                {
                    EditorGUILayout.HelpBox("Id cannot be empty", MessageType.Error);
                }
               
            }
           // 


            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(new GUIContent("Append"), GUILayout.Width(200)))
            {          
                int index = keys.arraySize;

                keys.InsertArrayElementAtIndex(index);
                keys.GetArrayElementAtIndex(index).stringValue = "";

                values.InsertArrayElementAtIndex(index);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();           
        }
    }
}
/*

[CustomPropertyDrawer(typeof(SignedPicture))]
public class ScaledCurveDrawer : PropertyDrawer
{
 
    public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
    {
        SerializedProperty sprite = prop.FindPropertyRelative("sprite");
        SerializedProperty sign = prop.FindPropertyRelative("sign");


        GUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(sprite, GUIContent.none);
        EditorGUILayout.PropertyField(sign, GUIContent.none);
        GUILayout.EndHorizontal();
    }
}*/