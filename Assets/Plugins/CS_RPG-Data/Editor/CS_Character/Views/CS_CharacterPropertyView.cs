﻿using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

using System;
using System.Collections;
using System.Collections.Generic;

public class CS_CharacterPropertyView : CS_CharacterViewBase
{

    public CS_CharacterPropertyView() : base("Property View ...") { }

    public override void UpdateView(Rect editorRect, Rect percentageRect, Event e, CS_CharacterGraph currentGraph)
    {
        base.UpdateView(editorRect, percentageRect, e, currentGraph);
        GUI.Box(viewRect, viewTitle, viewSkin.GetStyle("ViewBG"));

        GUILayout.BeginArea(viewRect);

        GUILayout.Space(60);
        GUILayout.BeginHorizontal();
        GUILayout.Space(30);
        if (currentGraph == null || currentGraph.ShowProperties == false)
        {
            EditorGUILayout.LabelField("None Dialog Selected.");
        }
        else
        {
            currentGraph.selectedCharacter.DrawCharacterProperties(viewRect);
        }
        GUILayout.Space(30);
        GUILayout.EndHorizontal();

        GUILayout.EndArea();

        ProcessEvents(e);
    }

    public override void ProcessEvents(Event e)
    {
        base.ProcessEvents(e);
        if (viewRect.Contains(e.mousePosition))
        {

        }
        if (e.type == EventType.MouseUp)
        {
            
        }
    }
}
