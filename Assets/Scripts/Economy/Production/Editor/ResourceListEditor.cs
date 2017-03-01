using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(ResourceList), true)]
public class ResourceListEditor : Editor
{

    ResourceList t;
    SerializedObject GetTarget;
    SerializedProperty List;

    Dictionary<int, bool> Foldouts;

    void OnEnable() {
        Foldouts = new Dictionary<int, bool>();
        t = (ResourceList) target;
        GetTarget = new SerializedObject(t);
        List = GetTarget.FindProperty("Resources");
    }

    public override void OnInspectorGUI() {

        GetTarget.Update();

        if(GUILayout.Button("AddItem")) {
            Resource res = new Resource();
            res.Name = "New Resource";
            t.Resources.Add(res);
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        for(int i = 0; i < List.arraySize; i++){
            
            SerializedProperty ListRef = List.GetArrayElementAtIndex(i);

            SerializedProperty Name = ListRef.FindPropertyRelative("Name");

            if(!Foldouts.ContainsKey(i)) {
                Foldouts.Add(i, false);
            }

            Foldouts[i] = EditorGUILayout.Foldout(Foldouts[i], Name.stringValue, true);
            if(Foldouts[i]) {
                SerializedProperty Ammount = ListRef.FindPropertyRelative("Ammount");
                SerializedProperty BasePrice = ListRef.FindPropertyRelative("BasePrice");

                EditorGUILayout.PropertyField(Name);
                EditorGUILayout.PropertyField(Ammount);
                EditorGUILayout.PropertyField(BasePrice);

                if(GUILayout.Button("Remove")) {
                    List.DeleteArrayElementAtIndex(i);
                    Foldouts = new Dictionary<int, bool>();
                }
            }


        }

        GetTarget.ApplyModifiedProperties();
    }

}