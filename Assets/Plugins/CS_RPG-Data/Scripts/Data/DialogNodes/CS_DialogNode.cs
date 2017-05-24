using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;

public class CS_DialogBox : CS_DialogBase
{
    #region Variables
    /* *** DataDialog *** */
    public int characterID;
    public int nbDialog;
    public string dialog;
    public string dialogName;

    public List<String> dialogs = new List<string>();


    private int _lastNb;
    #endregion

    #region Constructor
    public CS_DialogBox()
    {

    }
    #endregion

    #region Main Methods

#if UNITY_EDITOR
    public override void InitDialog()
    {
        base.InitDialog();
        dialogName = DialogName;
        DialogType = DialogType.Dialog;
        DialogRect = new Rect(10f, 10f, 150f, 65f);
        _lastNb = nbDialog;
    }

    public override void UpdateDialog(Event e, Rect viewRect)
    {
        base.UpdateDialog(e, viewRect);
    }


    public override void UpdateDialogGUI(Event e, Rect viewRect, GUISkin guiSkin)
    {
        base.UpdateDialogGUI(e, viewRect, guiSkin);

        

        DialogName = dialogName;
        DrawInputLines();
    }

    public override void DrawDialogProperties(Rect viewRect)
    {
        base.DrawDialogProperties(viewRect);

        GUILayout.BeginVertical();
        dialogName = EditorGUILayout.TextField("Dialog name: ", dialogName);
        characterID = EditorGUILayout.IntField("ID character: ", characterID);
        nbDialog = EditorGUILayout.IntField("Nb dialog: ", nbDialog);
        EditorGUILayout.LabelField("Dialog: ");
        int i = 0;
        Debug.Log(_lastNb + " " + nbDialog + " " + dialogs.Count);
        if (_lastNb != nbDialog)
        {
            List<String> s = new List<string>();
            while (i < nbDialog)
            {
                if (dialogs.Count > i)
                    s.Add(dialogs[i]);
                else
                    s.Add("");
                i = i + 1;
            }
            dialogs.Clear();
            dialogs.AddRange(s);
            s.Clear();

        }
        i = 0;
        while (i < nbDialog)
        {
         //   dialogs.Add("");
            dialogs[i] = EditorGUILayout.TextArea(dialogs[i], GUILayout.Height(120));
            i = i + 1;
        }

        _lastNb = nbDialog;
        GUILayout.EndVertical();
    }
#endif

    #endregion

    #region Utility Methods

#if UNITY_EDITOR
    void DrawInputLines()
    {
       

    }

  
#endif

#endregion
}
