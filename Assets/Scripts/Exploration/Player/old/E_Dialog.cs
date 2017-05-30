using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Dialog : MonoBehaviour
{
    public CS_DialogGraph dialogGraph;
    public int idDialog;

    private List<String> dialogs = new List<String>();
/*
    private CS_DialogBase dialogBase = new CS_DialogBase();
    private CS_DialogBox dialogBox = new CS_DialogBox();
    */
    private List<Func<IEnumerator>> call = new List<Func<IEnumerator>>();
    /*
    public void OnEnableDialog()
    {
        call.Add(Camera.main.gameObject.GetComponent<E_Camera>().CameraZoomIN);
        call.Add(ShowContent);
        StartCoroutine(StartDialog());
    }

    public void OnDisableDialog()
    {

    }

    public IEnumerator StartDialog()
    {
        foreach (Func<IEnumerator> func in call)
            yield return func.Invoke();
        call.Clear();
    }

    IEnumerator ShowContent()
    {
        GameObject.Find("Manager").GetComponent<E_DialogManager>().OnStart(GetComponent<E_PlayerManager>().interactable.GetComponent<E_PNJ>().dialogs);
        yield return null;
    }*/
}
