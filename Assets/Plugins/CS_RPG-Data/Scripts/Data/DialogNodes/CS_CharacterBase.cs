using System;
using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CS_CharacterBase : ScriptableObject
{
    public string CharacterName;
    public Sprite sprite;
    public Rect CharacterRect;
    public CS_CharacterGraph ParentGraph;
    public bool IsSelected;
    public GUISkin CharacterSkin { get; set; }

    public virtual void InitCharacter()
    {

    }
#if UNITY_EDITOR
    public virtual void UpdateCharacter(Event e, Rect viewRect)
    {
        ProcessEvents(e, viewRect);

        EditorUtility.SetDirty(this);
    }


    public virtual void UpdateCharacterGUI(Event e, Rect viewRect, GUISkin guiSkin)
    {
        ProcessEvents(e, CharacterRect);

        string currentStyle = IsSelected ? "EditorSelected" : "EditorDefault";


        GUI.Box(CharacterRect, CharacterName, guiSkin.GetStyle(currentStyle));



        if (sprite != null)
        {
            GUI.DrawTexture(new Rect(CharacterRect.x + 10, CharacterRect.y + 50, 80,80), sprite.texture, ScaleMode.StretchToFill, true, 10.0F);
        }

    

        
        EditorUtility.SetDirty(this);
    }

    public virtual void DrawCharacterProperties(Rect viewRect)
    {

    }
#endif

    private void ProcessEvents(Event e, Rect viewRect)
    {
        
    }
}

