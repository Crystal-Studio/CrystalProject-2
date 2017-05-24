using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System;
using System.Collections;
using System.Collections.Generic;


[Serializable]
public class CS_CharacterGraph : ScriptableObject
{
    public GraphTypeCharacter GraphType;
    public string GraphName = "New graph";
    public CS_CharacterBase selectedCharacter;
    public bool ShowProperties;
    public List<CS_CharacterBase> Characters;

    private void OnEnable()
    {
        if (Characters == null)
        {
            Characters = new List<CS_CharacterBase>();
        }
    }

    public void InitGraph()
    {
       
    }

    public void UpdateGraph()
    {

    }

#if UNITY_EDITOR

    public void UpdateGraphGUI(Event e, Rect viewRect, GUISkin guiSkin)
    {
        if (Characters.Count > 0)
        {
            ProcessEvents(e, viewRect);
            foreach (var Dialog in Characters)
            {
                Dialog.UpdateCharacterGUI(e, viewRect, guiSkin);
            }
        }

        if (e.type == EventType.Layout && selectedCharacter != null)
        {
            ShowProperties = true;
        }

        EditorUtility.SetDirty(this);
    }

#endif

    private void ProcessEvents(Event e, Rect viewRect)
    {
        if (viewRect.Contains(e.mousePosition))
        {
            if (e.button == 0 && e.type == EventType.MouseDown)
            {
                DeselectAllDialogs();
                ShowProperties = false;
                bool setDialog = false;
                
                for (int i = 0; i < Characters.Count; i++)
                {
                    if (Characters[i].CharacterRect.Contains(e.mousePosition))
                    {
                        Characters[i].IsSelected = true;
                        selectedCharacter = Characters[i];
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
        for (int i = 0; i < Characters.Count; i++)
        {
            Characters[i].IsSelected = false;

        }
    }

}