#if UNITY_EDITOR

// using UnityEditor;
// using UnityEngine;
// using System.Linq;

// [CustomEditor(typeof(SkillData))]
// public class SkillDataEditor : Editor
// {
    // public override void OnInspectorGUI()
    // {
    //     serializedObject.Update();

    //     SkillData skillData = (SkillData)target;
    //     SerializedProperty skillDefaults = serializedObject.FindProperty("skillDefaults");

    //     EditorGUI.BeginChangeCheck();

    //     skillData.skillDefaults = skillData.skillDefaults.OrderBy(x => x.name).ToList();

    //     for (int i = 0; i < skillData.skillDefaults.Count; i++)
    //     {
    //         EditorGUILayout.BeginHorizontal();
            
    //         EditorGUILayout.PropertyField(skillDefaults.GetArrayElementAtIndex(i), new GUIContent(skillData.skillDefaults[i].name.ToString()), true);

    //         if (GUILayout.Button("X", GUILayout.MaxWidth(20f)))
    //         {
    //             skillData.skillDefaults.RemoveAt(i);
    //         }

    //         EditorGUILayout.EndHorizontal();
    //     }

    //     if (EditorGUI.EndChangeCheck())
    //     {
    //         serializedObject.ApplyModifiedProperties();
    //     }

    //     GUILayout.Space(10);

    //     if (GUILayout.Button("Add New Skill"))
    //     {
    //         skillData.skillDefaults.Add(new SkillData.ParameterWithKey());
    //     }

    //     GUILayout.Space(10);

    //     if (GUILayout.Button("Reset"))
    //     {
    //         skillData.Reset();
    //     }
    // }
// }

#endif