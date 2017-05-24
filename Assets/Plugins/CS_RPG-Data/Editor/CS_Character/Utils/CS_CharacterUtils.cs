using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

public class CS_CharacterUtils
{
   

    public static void CreateCharacterGraph(string WantedName)
    {
        CS_CharacterGraph curGraph = ScriptableObject.CreateInstance<CS_CharacterGraph>();
        if (curGraph != null)
        {
            curGraph.GraphName = WantedName;
            
            curGraph.InitGraph();

            AssetDatabase.CreateAsset(curGraph, "Assets/CS_RPG-data/Database/" + WantedName + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            CS_CharacterEditorWindow curWindows = EditorWindow.GetWindow<CS_CharacterEditorWindow>();
            if (curWindows != null)
            {
                curWindows.currentGraph = curGraph;
            }
        }
        else
        {
            EditorUtility.DisplayDialog("Dialog Message", "Unable to create new graph, please see your friendly programmer!", "OK");
        }
    }

    public static void LoadGraph()
    {
        CS_CharacterGraph curGraph = null;
         
        string grapPath = EditorUtility.OpenFilePanel("Load Graph", Application.dataPath + "/CS_RPG-Data/Database/", "");

        int appPathLen = Application.dataPath.Length;
        string finalPath = grapPath.Substring(appPathLen - 6);
        curGraph = (CS_CharacterGraph)AssetDatabase.LoadAssetAtPath(finalPath, typeof(CS_CharacterGraph));

        if (curGraph != null)
        {
            CS_CharacterEditorWindow curWindows = EditorWindow.GetWindow<CS_CharacterEditorWindow>();
            if (curWindows != null)
            {
                curWindows.currentGraph = curGraph;
            }
        }
        else
        {
            EditorUtility.DisplayDialog("Dialog Message", "Unable to load selected graph!", "ok");
        }
    }

    public static void UnloadGraph()
    {
        CS_CharacterEditorWindow curWindows = EditorWindow.GetWindow<CS_CharacterEditorWindow>();
        if (curWindows != null)
        {
            curWindows.currentGraph = null;
        }
    }

    public static void ReplaceCharacter(CS_CharacterGraph curGraph)
    {
        int i = 0;
        int x = 0;
        int y = 0;

        while (i < curGraph.Characters.Count)
        {
            curGraph.Characters[i].CharacterRect = new Rect(x, y, 100, 150);
            i = i + 1;

            x += 120;
            if (i % 5 == 0 && i != 0)
            {
                y += 170;
                x = 0;
            }
        }
    }

    public static void CreateCharacter(CS_CharacterGraph curGraph, Vector3 mousePosition)
    {
        if (curGraph == null)
        {
            return;
        }

        CS_CharacterBase currentCharacter = null;

        currentCharacter = ScriptableObject.CreateInstance<CS_CharacterBox>();
        currentCharacter.CharacterName = "Name";
        currentCharacter.sprite = Resources.Load("view_bg_normal", typeof(Sprite)) as Sprite;

        if (currentCharacter != null)
        {
            currentCharacter.InitCharacter();

            Rect charRect = GetBoxPosition(curGraph);

            currentCharacter.CharacterRect = charRect;

            currentCharacter.ParentGraph = curGraph;
            curGraph.Characters.Add(currentCharacter);

            AssetDatabase.AddObjectToAsset(currentCharacter, curGraph);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

    }

    public static Rect GetBoxPosition(CS_CharacterGraph curGraph)
    {
        int i = 0;
        int x = 0;
        int y = 0;

        while (i < curGraph.Characters.Count)
        {
            x += 120;
            if (i % 5 == 0 && i != 0)
            {
                y += 170;
                x = 0;
            }
            i = i + 1;
        }

        return new Rect(x, y, 100, 150);
    }

    public static void DeleteCharacter(int characterID, CS_CharacterGraph curGraph)
    {
        if (curGraph != null)
        {
            if (curGraph.Characters.Count >= characterID)
            {
                CS_CharacterBase deleteDialog = curGraph.Characters[characterID];
                if (deleteDialog != null)
                {
                    curGraph.Characters.RemoveAt(characterID);
                    GameObject.DestroyImmediate(deleteDialog, true);

                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }
        }
    }
}
