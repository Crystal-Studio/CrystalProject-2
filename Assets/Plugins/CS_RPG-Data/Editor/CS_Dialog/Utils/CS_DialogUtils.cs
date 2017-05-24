using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

public class CS_DialogUtils
{
   

    public static void CreateDialogGraph(string WantedName)
    {
        CS_DialogGraph curGraph = ScriptableObject.CreateInstance<CS_DialogGraph>();
        if (curGraph != null)
        {
            curGraph.GraphName = WantedName;
            
            curGraph.InitGraph();

            Debug.Log(Application.dataPath + "/Database/Dialog/" + WantedName + ".asset");
            AssetDatabase.CreateAsset(curGraph, "Assets/Database/Dialog/" + WantedName + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            CS_DialogEditorWindow curWindows = EditorWindow.GetWindow<CS_DialogEditorWindow>();
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
        CS_DialogGraph curGraph = null;
         
        string grapPath = EditorUtility.OpenFilePanel("Load Graph", Application.dataPath + "/CS_RPG-Data/Database/", "");

        int appPathLen = Application.dataPath.Length;
        string finalPath = grapPath.Substring(appPathLen - 6);
        curGraph = (CS_DialogGraph)AssetDatabase.LoadAssetAtPath(finalPath, typeof(CS_DialogGraph));

        if (curGraph != null)
        {
            CS_DialogEditorWindow curWindows = EditorWindow.GetWindow<CS_DialogEditorWindow>();
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
        CS_DialogEditorWindow curWindows = EditorWindow.GetWindow<CS_DialogEditorWindow>();
        if (curWindows != null)
        {
            curWindows.currentGraph = null;
        }
    }

    public static void ReplaceDialog(CS_DialogGraph curGraph)
    {
        int i = 0;
        int x = 0;
        int y = 0;

        while (i < curGraph.Dialogs.Count)
        {
            curGraph.Dialogs[i].DialogRect = new Rect(x, y, 100, 150);
            i = i + 1;

            x += 120;
            if (i % 5 == 0 && i != 0)
            {
                y += 170;
                x = 0;
            }
        }
    }

    public static void CreateDialog(CS_DialogGraph curGraph, Vector3 mousePosition)
    {
        if (curGraph == null)
        {
            return;
        }

        CS_DialogBase currentDialog = null;

        currentDialog = ScriptableObject.CreateInstance<CS_DialogBox>();
        currentDialog.DialogName = "Name";

        if (currentDialog != null)
        {
            currentDialog.InitDialog();

            Rect charRect = GetBoxPosition(curGraph);

            currentDialog.DialogRect = charRect;

            currentDialog.ParentGraph = curGraph;
            curGraph.Dialogs.Add(currentDialog);

            AssetDatabase.AddObjectToAsset(currentDialog, curGraph);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

    }

    public static Rect GetBoxPosition(CS_DialogGraph curGraph)
    {
        int i = 0;
        int x = 0;
        int y = 0;

        while (i < curGraph.Dialogs.Count)
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

    public static void DeleteDialog(int DialogID, CS_DialogGraph curGraph)
    {
        if (curGraph != null)
        {
            if (curGraph.Dialogs.Count >= DialogID)
            {
                CS_DialogBase deleteDialog = curGraph.Dialogs[DialogID];
                if (deleteDialog != null)
                {
                    curGraph.Dialogs.RemoveAt(DialogID);
                    GameObject.DestroyImmediate(deleteDialog, true);

                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }
        }
    }
}
