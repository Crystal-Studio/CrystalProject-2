using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System;
using System.Collections;
using System.Collections.Generic;


[Serializable]
public class CS_SpellGraph : ScriptableObject
{
    public string GraphName = "New graph";
    public CS_SpellBase selectedSpell;
    public bool ShowProperties;
    public List<CS_SpellBase> Spells;

    private void OnEnable()
    {
        if (Spells == null)
        {
            Spells = new List<CS_SpellBase>();
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
        if (Spells.Count > 0)
        {
            ProcessEvents(e, viewRect);
            foreach (var Dialog in Spells)
            {
                Dialog.UpdateSpellGUI(e, viewRect, guiSkin);
            }
        }

        if (e.type == EventType.Layout && selectedSpell != null)
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
                
                for (int i = 0; i < Spells.Count; i++)
                {
                    if (Spells[i].SpellRect.Contains(e.mousePosition))
                    {
                        Spells[i].IsSelected = true;
                        selectedSpell = Spells[i];
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
        for (int i = 0; i < Spells.Count; i++)
        {
            Spells[i].IsSelected = false;

        }
    }

}