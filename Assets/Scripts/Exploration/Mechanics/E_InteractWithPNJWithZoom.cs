using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_InteractWithPNJWithZoom : MonoBehaviour
{
    public Texture2D cursor;
    public Transform target;

    public CS_DialogGraph dialogGraph;
    public int idDialog;

    [HideInInspector]
    public List<String> dialogs = new List<String>();

    private CS_DialogBase dialogBase;
    private CS_DialogBox dialogBox;

    private List<Func<IEnumerator>> call = new List<Func<IEnumerator>>();

    // Use this for initialization
    void Start()
    {
        dialogBase = new CS_DialogBase();
        Debug.Log("Dialog Base : " + dialogBase + "DialogGraph : " + dialogGraph + " ->  " + dialogGraph.Dialogs[idDialog]);
        dialogBase = dialogGraph.Dialogs[idDialog];
        Debug.Log("Dialsssssog Base : " + dialogBase + "DialossssgGraph : " + dialogGraph);

        dialogBox = new CS_DialogBox();
        dialogBox = (CS_DialogBox)dialogGraph.Dialogs[idDialog];

        dialogs.AddRange(dialogBox.dialogs);
    }

    private void Update()
    {
        Debug.Log("Error : " + dialogBox + " - " + dialogBase + " " + dialogGraph.Dialogs[idDialog]);
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
        GM_Manager.instance.player.GetComponent<E_pManager>().Interact(gameObject, target.position);
    }

    public void OnInteractStart()
    {
        call.Add(Camera.main.gameObject.GetComponent<E_Camera>().CameraZoomIN);
        call.Add(ShowContent);
        StartCoroutine(DoActions());
    }

    public void OnInteractEnd()
    {
        call.Add(HideContent);
        call.Add(Camera.main.gameObject.GetComponent<E_Camera>().CameraZoomOUT);
        call.Add(HeroMove);
        StartCoroutine(DoActions());
    }

    IEnumerator DoActions()
    {
        foreach (Func<IEnumerator> func in call)
            yield return func.Invoke();
        call.Clear();
    }

    IEnumerator ShowContent()
    {
        GM_Manager.instance.e_dialogManager.OnStart(dialogs);
        yield return null;
    }

    IEnumerator HideContent()
    {
        GM_Manager.instance.e_dialogManager.OnEnd();
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator HeroMove()
    {
        yield return new WaitForSeconds(0.1f);
        GM_Manager.instance.player.GetComponent<E_pManager>().SetMove(true);
    }

}
