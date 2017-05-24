using System;
using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CS_SpellBase : ScriptableObject
{
    public string SpellName;
    public Sprite sprite;
    public Rect SpellRect;
    public CS_SpellGraph ParentGraph;
    public bool IsSelected;
    public GUISkin SpellSkin { get; set; }

    public virtual void InitSpell()
    {

    }
#if UNITY_EDITOR
    public virtual void UpdateSpell(Event e, Rect viewRect)
    {
        ProcessEvents(e, viewRect);

        EditorUtility.SetDirty(this);
    }


    public virtual void UpdateSpellGUI(Event e, Rect viewRect, GUISkin guiSkin)
    {
        ProcessEvents(e, SpellRect);

        string currentStyle = IsSelected ? "EditorSelected" : "EditorDefault";


        GUI.Box(SpellRect, SpellName, guiSkin.GetStyle(currentStyle));



        if (sprite != null)
        {
            GUI.DrawTexture(new Rect(SpellRect.x + 10, SpellRect.y + 50, 80,80), sprite.texture, ScaleMode.StretchToFill, true, 10.0F);
        }

    

        
        EditorUtility.SetDirty(this);
    }

    public virtual void DrawSpellProperties(Rect viewRect)
    {

    }
#endif

    private void ProcessEvents(Event e, Rect viewRect)
    {
        
    }
}

