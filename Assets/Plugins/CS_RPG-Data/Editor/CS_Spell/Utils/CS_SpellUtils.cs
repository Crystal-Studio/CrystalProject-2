using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

public class CS_SpellUtils
{
   

    public static void CreateSpellGraph(string WantedName)
    {
        CS_SpellGraph curGraph = ScriptableObject.CreateInstance<CS_SpellGraph>();
        if (curGraph != null)
        {
            curGraph.GraphName = WantedName;
            
            curGraph.InitGraph();

            AssetDatabase.CreateAsset(curGraph, "Assets/CS_RPG-data/Database/" + WantedName + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            CS_SpellEditorWindow curWindows = EditorWindow.GetWindow<CS_SpellEditorWindow>();
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
        CS_SpellGraph curGraph = null;
         
        string grapPath = EditorUtility.OpenFilePanel("Load Graph", Application.dataPath + "/CS_RPG-Data/Database/", "");

        int appPathLen = Application.dataPath.Length;
        string finalPath = grapPath.Substring(appPathLen - 6);
        curGraph = (CS_SpellGraph)AssetDatabase.LoadAssetAtPath(finalPath, typeof(CS_SpellGraph));

        if (curGraph != null)
        {
            CS_SpellEditorWindow curWindows = EditorWindow.GetWindow<CS_SpellEditorWindow>();
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
        CS_SpellEditorWindow curWindows = EditorWindow.GetWindow<CS_SpellEditorWindow>();
        if (curWindows != null)
        {
            curWindows.currentGraph = null;
        }
    }

    public static void ReplaceSpell(CS_SpellGraph curGraph)
    {
        int i = 0;
        int x = 0;
        int y = 0;

        while (i < curGraph.Spells.Count)
        {
            curGraph.Spells[i].SpellRect = new Rect(x, y, 100, 150);
            i = i + 1;

            x += 120;
            if (i % 5 == 0 && i != 0)
            {
                y += 170;
                x = 0;
            }
        }
    }

    public static void CreateSpell(CS_SpellGraph curGraph, Vector3 mousePosition)
    {
        if (curGraph == null)
        {
            return;
        }

        CS_SpellBase currentSpell = null;

        currentSpell = ScriptableObject.CreateInstance<CS_SpellBox>();
        currentSpell.SpellName = "Name";
        currentSpell.sprite = Resources.Load("view_bg_normal", typeof(Sprite)) as Sprite;

        if (currentSpell != null)
        {
            currentSpell.InitSpell();

            Rect charRect = GetBoxPosition(curGraph);

            currentSpell.SpellRect = charRect;

            currentSpell.ParentGraph = curGraph;
            curGraph.Spells.Add(currentSpell);

            AssetDatabase.AddObjectToAsset(currentSpell, curGraph);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

    }

    public static Rect GetBoxPosition(CS_SpellGraph curGraph)
    {
        int i = 0;
        int x = 0;
        int y = 0;

        while (i < curGraph.Spells.Count)
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

    public static void DeleteSpell(int SpellID, CS_SpellGraph curGraph)
    {
        if (curGraph != null)
        {
            if (curGraph.Spells.Count >= SpellID)
            {
                CS_SpellBase deleteDialog = curGraph.Spells[SpellID];
                if (deleteDialog != null)
                {
                    curGraph.Spells.RemoveAt(SpellID);
                    GameObject.DestroyImmediate(deleteDialog, true);

                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }
        }
    }
}
