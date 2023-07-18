#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Linq;

[CustomEditor(typeof(ArtifactData))]
public class ArtifactDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        ArtifactData ArtifactData = (ArtifactData)target;
        SerializedProperty artifactDefaults = serializedObject.FindProperty("artifactDefaults");

        EditorGUI.BeginChangeCheck();

        ArtifactData.artifactDefaults = ArtifactData.artifactDefaults.OrderBy(x => x.name).ToList();

        for (int i = 0; i < ArtifactData.artifactDefaults.Count; i++)
        {
            EditorGUILayout.PropertyField(artifactDefaults.GetArrayElementAtIndex(i), new GUIContent(ArtifactData.artifactDefaults[i].name.ToString()), true);
        }

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
        }

        GUILayout.Space(10);
        
        if (GUILayout.Button("Add New Artifact"))
        {
            ArtifactData.artifactDefaults.Add(new ArtifactData.ParameterWithKey());
        }

        GUILayout.Space(10);
        
        if (GUILayout.Button("Reset"))
        {
            ArtifactData.Reset();
        }
    }
}
#endif