using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(GetDialogFromFile))]
public class GetDialogFromFileEditor : Editor
{
    GetDialogFromFile t;
    SerializedObject GetTarget;
    SerializedProperty ThisList;
    int ListSize;

    SerializedProperty value;

    string[] popup;

    void OnEnable()
    {
        t = (GetDialogFromFile)target;
        GetTarget = new SerializedObject(t);
        ThisList = GetTarget.FindProperty("MyList");
    }

    public override void OnInspectorGUI()
    {
        GetTarget.Update();

        t.value = EditorGUILayout.ObjectField("File :", t.value, typeof(TextAsset), true) as TextAsset;

        if (t.value != null)
        {
            t.data = JsonUtility.FromJson<DialogFile>(t.value.ToString());

            Debug.Log(t.value.ToString());
            
            EditorGUILayout.LabelField("Dialog : ");

            popup = new string[t.data.dialog.Length];
            for (int i = 0; i < t.data.dialog.Length; i++)
            {
                popup[i] = t.data.dialog[i].key;
            }

            t.index = EditorGUILayout.Popup(t.index, popup);

            for (int i = 0; i < t.data.dialog[t.index].fr.Length; i++)
            {
                EditorGUILayout.HelpBox((i + 1) + ". " + t.data.dialog[t.index].fr[i], MessageType.None, true);
            }
        }
    }
}

