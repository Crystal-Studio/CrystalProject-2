using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum e_PNJType
{
    MOBILE,
    IMMOBILE
}

public class E_PNJ : MonoBehaviour
{
    public e_PNJType PNJType;
    public Texture2D cursor;

    public CS_DialogGraph dialogGraph;
    public int idDialog;

    [HideInInspector]
    public List<String> dialogs = new List<String>();

    private CS_DialogBase dialogBase;
    private CS_DialogBox dialogBox;

    private GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        dialogBase = new CS_DialogBase();
        dialogBox = new CS_DialogBox();

        dialogBase = dialogGraph.Dialogs[idDialog];
        dialogBox = (CS_DialogBox)dialogBase;
        dialogs.AddRange(dialogBox.dialogs);
    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseDown()
    {
        player.GetComponent<E_Movement>().SetDestination(transform.GetChild(0).position, e_actionType.TALK); 
        player.GetComponent<E_PlayerManager>().SetInteractableObject(gameObject);
    }
}
