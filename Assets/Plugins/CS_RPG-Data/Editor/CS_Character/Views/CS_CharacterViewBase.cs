using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;

[Serializable]
public class CS_CharacterViewBase
{

    public string viewTitle;
    public Rect viewRect;
    public bool showProperties = false;

    protected GUISkin viewSkin;
    protected CS_CharacterGraph currentGraph;

    public CS_CharacterViewBase(string title)
    {
        viewTitle = title;
        GetEditorSkin();
    }

    public virtual void UpdateView(Rect editorRect, Rect percentageRect, Event e, CS_CharacterGraph CurrentGraph)
    {
        if (viewSkin == null)
        {
            GetEditorSkin();
        }

        currentGraph = CurrentGraph;

        if (currentGraph != null)
        {
            viewTitle = currentGraph.GraphName;
        }
        else
        {
            viewTitle = "No Graph!";
        }

        if (currentGraph != null)
        {
            viewTitle = currentGraph.GraphName;
        }
        else
        {
            viewTitle = "No Graph!";
        }

        viewRect = new Rect(editorRect.x * percentageRect.x,
                            editorRect.y * percentageRect.y,
                            editorRect.width * percentageRect.width,
                            editorRect.height * percentageRect.height);
    }

    public virtual void ProcessEvents(Event e) {}

    protected void GetEditorSkin()
    {
        viewSkin = (GUISkin)Resources.Load("GUISkins/EditorSkins/EditorDarkSkin");
    }
}
