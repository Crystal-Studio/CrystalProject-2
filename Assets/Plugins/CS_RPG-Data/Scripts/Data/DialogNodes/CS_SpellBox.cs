using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
#endif

using System;
using System.Collections;

public class CS_SpellBox : CS_SpellBase
{

    /* *** Data Characater *** */

    public Sprite avatarSprite;
    public string spellName = "name";

    public spellType sType;

    public string tooltip;
    public int cost;
    public int attackPower;
    public int magicPower;
    public int average;
    public int idSpecial;

    public CS_SpellBox()
    {

    }
#if UNITY_EDITOR
    public override void InitSpell()
    {
        base.InitSpell();
        SpellRect = new Rect(10f, 10f, 100f, 100f);
    }

    public override void UpdateSpell(Event e, Rect viewRect)
    {
        base.UpdateSpell(e, viewRect);
    }

    public override void UpdateSpellGUI(Event e, Rect viewRect, GUISkin guiSkin)
    {
        base.UpdateSpellGUI(e, viewRect, guiSkin);

        sprite = avatarSprite;
        SpellName = spellName;
    }

    public override void DrawSpellProperties(Rect viewRect)
    {
        base.DrawSpellProperties(viewRect);

        GUILayout.BeginVertical();
        spellName = EditorGUILayout.TextField("Spell name: ", spellName);

        sType = (spellType)EditorGUILayout.EnumPopup("Type:", sType);

        EditorGUILayout.LabelField("Avatar : ");
        avatarSprite = (Sprite)EditorGUILayout.ObjectField(avatarSprite, typeof(Sprite), GUILayout.Width(75), GUILayout.Height(75));

        tooltip = EditorGUILayout.TextArea(tooltip, GUILayout.Height(50));
        cost = EditorGUILayout.IntField("Cout : ", cost);
        attackPower = EditorGUILayout.IntField("Attack Power : ", attackPower);
        magicPower = EditorGUILayout.IntField("Magic power : ", magicPower);
        average = EditorGUILayout.IntField("Average : ", average);
        idSpecial = EditorGUILayout.IntField("Special ID : ", idSpecial);

        GUILayout.EndVertical();
    }
#endif

}
