using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System;
using System.Collections;
using System.Collections.Generic;


[Serializable]
public class CS_DialogGraph : ScriptableObject
{
    public GraphType GraphType;
    public string GraphName = "New graph";
    public CS_DialogBase selectedDialog;
    public List<CS_DialogBase> Dialogs;
    public bool ShowProperties;

    public bool WantsConnection;
    public CS_DialogBase ConnectionDialog;

    private void OnEnable()
    {
        if (Dialogs == null)
        {
            Dialogs = new List<CS_DialogBase>();
        }
    }

    public void InitGraph()
    {
        if (Dialogs.Count > 0)
        {
            foreach (var Dialog in Dialogs)
            {
                Dialog.InitDialog();
            }
        }
    }

    public void UpdateGraph()
    {

    }

#if UNITY_EDITOR

    public void UpdateGraphGUI(Event e, Rect viewRect, GUISkin guiSkin)
    {
        if (Dialogs.Count > 0)
        {
            ProcessEvents(e, viewRect);
            foreach (var Dialog in Dialogs)
            {
                Dialog.UpdateDialogGUI(e, viewRect, guiSkin);
            }
        }

        if (WantsConnection && ConnectionDialog != null)
        {
            DrawConnectionToMouse(e.mousePosition);
        }

        if (e.type == EventType.Layout && selectedDialog != null)
        {
            ShowProperties = true;
        }

        EditorUtility.SetDirty(this);
    }

    private void ProcessEvents(Event e, Rect viewRect)
    {
        if (viewRect.Contains(e.mousePosition))
        {
            if (e.button == 0 && e.type == EventType.MouseDown)
            {
                DeselectAllDialogs();
                ShowProperties = false;
                bool setDialog = false;
                WantsConnection = false;
                selectedDialog = null;
                
                for (int i = 0; i < Dialogs.Count; i++)
                {
                    if (Dialogs[i].DialogRect.Contains(e.mousePosition))
                    {
                        Dialogs[i].IsSelected = true;
                        selectedDialog = Dialogs[i];
                        setDialog = true;
                    }
                }

                if (!setDialog)
                {
                    DeselectAllDialogs();
                }

            }
        }
    }

    private void DeselectAllDialogs()
    {
        for (int i = 0; i < Dialogs.Count; i++)
        {
            Dialogs[i].IsSelected = false;

        }
    }

    private void DrawConnectionToMouse(Vector2 mousePosition)
    {
        Handles.BeginGUI();

        Handles.color = Color.yellow;
        Handles.DrawLine(new Vector2(ConnectionDialog.DialogRect.x + ConnectionDialog.DialogRect.width * 0.5f,
                                     ConnectionDialog.DialogRect.y + ConnectionDialog.DialogRect.height + 10f), 
                                     mousePosition);

        Handles.EndGUI();
    }

#endif
}