using System;
using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;


public class SpellPopupWindow : EditorWindow
{
    private static SpellPopupWindow currentPopupWindow;
    private string wantedName = "Database Name";

    public static void InitSpellPopup()
    {
        currentPopupWindow = GetWindow<SpellPopupWindow>();
        currentPopupWindow.titleContent.text = "Create Spell Database";
    }

    private void OnGUI()
    {
        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        GUILayout.Space(20);

        GUILayout.BeginVertical();

        EditorGUILayout.LabelField("Create New Spell Database", EditorStyles.boldLabel);

        wantedName = EditorGUILayout.TextField("Enter Name: ", wantedName);

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Create Spell Database", GUILayout.Height(40f)))
        {
            if (!String.IsNullOrEmpty(wantedName) && wantedName != "Enter a name...")
            {
                CS_SpellUtils.CreateSpellGraph(wantedName);
                currentPopupWindow.Close();
            }
            else
            {
                EditorUtility.DisplayDialog("Dialog Message: ", "Please enter a valid graph name!", "OK");
            }
        }

        if (GUILayout.Button("Cancel", GUILayout.Height(40f)))
        {
            currentPopupWindow.Close();
        }

        GUILayout.EndHorizontal();

        GUILayout.EndVertical();

        GUILayout.Space(20);
        GUILayout.EndHorizontal();
        GUILayout.Space(20);
    }
}
#endif