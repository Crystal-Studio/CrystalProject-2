using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

using System;
using System.Collections;
using System.Collections.Generic;

public class CS_SpellWorkView : CS_SpellViewBase
{
    Vector2 mousePos;
    int deleteNoteID = 0;

    public CS_SpellWorkView() : base("Work View ...") { }

    public override void UpdateView(Rect editorRect, Rect percentageRect, Event e, CS_SpellGraph currentGraph)
    {
        base.UpdateView(editorRect, percentageRect, e, currentGraph);
        
        GUI.Box(viewRect, viewTitle, viewSkin.GetStyle("ViewBG"));

        GUILayout.BeginArea(viewRect);
        if (currentGraph != null)
        {
            currentGraph.UpdateGraphGUI(e, viewRect, viewSkin);
        }
        GUILayout.EndArea();

        ProcessEvents(e);
    }

    public override void ProcessEvents(Event e)
    {
        base.ProcessEvents(e);
        

        if (viewRect.Contains(e.mousePosition))
        {
            if (e.button == 0)
            {
                if (e.type == EventType.MouseDown)
                {

                }

                if (e.type == EventType.MouseDrag)
                {

                }

                if (e.type == EventType.MouseUp)
                {

                }
            }

            if (e.button == 1)
            {
                if (e.type == EventType.mouseDown)
                {
                    mousePos = e.mousePosition;
                    bool overDialog = false;
                    deleteNoteID = 0;
                    if (currentGraph != null)
                    {
                        if (currentGraph.Spells.Count > 0)
                        {
                            for (int i = 0; i < currentGraph.Spells.Count; i++)
                            {
                                if (currentGraph.Spells[i].SpellRect.Contains(mousePos))
                                {
                                    deleteNoteID = i;
                                    overDialog = true;
                                }
                            }
                        }
                    }
                    
                    if (!overDialog)
                    {
                        ProcessContextMenu(e, 0);
                    }
                    else
                    {
                        ProcessContextMenu(e, 1);
                    }
                }
            }
        }
    }

    void ProcessContextMenu(Event e, int contextID)
    {
        GenericMenu menu = new GenericMenu();

        if (contextID == 0)
        {
            menu.AddItem(new GUIContent("Create Database"), false, ContextCallBack, "0");
            menu.AddItem(new GUIContent("Load Database"), false, ContextCallBack, "1");

            if (currentGraph != null)
            {
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Unload Database"), false, ContextCallBack, "2");

                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Create Object"), false, ContextCallBack, "3");
            }
        }
        
        if (contextID == 1)
        {
            menu.AddItem(new GUIContent("Delete Box"), false, ContextCallBack, "5");
        }

        menu.ShowAsContext();
        e.Use();
    }
    

    void ContextCallBack(object obj)
    {
        switch (obj.ToString())
        {
            case "0":
                SpellPopupWindow.InitSpellPopup();
                break;
            case "1":
                 CS_SpellUtils.LoadGraph();
                break;
            case "2":
                  CS_SpellUtils.UnloadGraph();
                break;
            case "3":
                CS_SpellUtils.CreateSpell(currentGraph, mousePos);
                CS_SpellUtils.ReplaceSpell(currentGraph);
                break;
            case "5":
                CS_SpellUtils.DeleteSpell(deleteNoteID, currentGraph);
                CS_SpellUtils.ReplaceSpell(currentGraph);
                break;
            default:
                break;
        }
    }

}
