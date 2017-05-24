using UnityEngine;
using UnityEditor;
using System.Collections;

public class CS_DialogMenus
{
    [MenuItem("Crystal Studio - RPG/Dialogs Editor")]
    public static void InitDialogEditor()
    {
        CS_DialogEditorWindow.InitEditorWindow();
    }

    [MenuItem("Crystal Studio - RPG/Character Editor")]
    public static void InitCharacterEditor()
    {
        CS_CharacterEditorWindow.InitEditorWindow();
    }

    [MenuItem("Crystal Studio - RPG/Spells Editor")]
    public static void InitSpellEditor()
    {
        CS_SpellEditorWindow.InitEditorWindow();
    }
}
