using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
#endif

using System;
using System.Collections;

public class CS_CharacterBox : CS_CharacterBase
{

    /* *** Data Characater *** */

    public Sprite avatarSprite;
    public string characterName = "name";

    public Class charClass;

    public int health;
    public int resource;
    public int attackPower;
    public int magicPower;
    public int resistMagic;
    public int resistAttack;
    public int passifPower;

    public CS_CharacterBox()
    {

    }
#if UNITY_EDITOR
    public override void InitCharacter()
    {
        base.InitCharacter();
        CharacterRect = new Rect(10f, 10f, 100f, 100f);
    }

    public override void UpdateCharacter(Event e, Rect viewRect)
    {
        base.UpdateCharacter(e, viewRect);
    }


    public override void UpdateCharacterGUI(Event e, Rect viewRect, GUISkin guiSkin)
    {
        base.UpdateCharacterGUI(e, viewRect, guiSkin);

        sprite = avatarSprite;
        CharacterName = characterName;
    }

    public override void DrawCharacterProperties(Rect viewRect)
    {
        base.DrawCharacterProperties(viewRect);

        GUILayout.BeginVertical();
        characterName = EditorGUILayout.TextField("Dialog name: ", characterName);
        EditorGUILayout.LabelField("Avatar : ");
        avatarSprite = (Sprite)EditorGUILayout.ObjectField(avatarSprite, typeof(Sprite), GUILayout.Width(75), GUILayout.Height(75));

        charClass = (Class)EditorGUILayout.EnumPopup("Class:", charClass);
        health = EditorGUILayout.IntField("Health : ", health);
        EditorGUILayout.LabelField("Mana : ");
        resource = EditorGUILayout.IntSlider(resource, 0, 100);
        attackPower = EditorGUILayout.IntField("Attack Power : ", attackPower);
        magicPower = EditorGUILayout.IntField("Magic power : ", magicPower);
        resistAttack = EditorGUILayout.IntField("Resistance Physic : ", resistAttack);
        resistMagic = EditorGUILayout.IntField("Resistance Magic : ", resistMagic);
        passifPower = EditorGUILayout.IntField("Passif Power : ", passifPower);

        GUILayout.EndVertical();
    }
#endif

}
