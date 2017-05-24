using System;
using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CS_DialogBase : ScriptableObject
{
    public string DialogName;
    public Rect DialogRect;
    public CS_DialogGraph ParentGraph;
    public DialogType DialogType;
    public bool IsSelected;
    public GUISkin DialogSkin { get; set; }
    public bool canMove = false;

    [Serializable]
    public class CS_DialogInput
    {
        public bool isOccupied;
        public CS_DialogBase inputDialog;
    }

    [Serializable]
    public class CS_DialogOutput
    {
        public bool isOccupied;
        public CS_DialogBase outputDialog;
    }

    public virtual void InitDialog()
    {

    }
#if UNITY_EDITOR
    public virtual void UpdateDialog(Event e, Rect viewRect)
    {
        ProcessEvents(e, viewRect);

        EditorUtility.SetDirty(this);
    }


    public virtual void UpdateDialogGUI(Event e, Rect viewRect, GUISkin guiSkin)
    {
        ProcessEvents(e, DialogRect);

        string currentStyle = IsSelected ? "EditorSelected" : "EditorDefault";
        GUI.Box(DialogRect, DialogName, guiSkin.GetStyle(currentStyle));

        EditorUtility.SetDirty(this);
    }

    public virtual void DrawDialogProperties(Rect viewRect)
    {

    }
#endif

    private void ProcessEvents(Event e, Rect viewRect)
    {
        if (IsSelected)
        {
            if (e.type == EventType.MouseDrag && canMove)
            {
                var rect = DialogRect;

                rect.x += e.delta.x;
                rect.y += e.delta.y;

                DialogRect = rect;
            }
        }
    }
}

