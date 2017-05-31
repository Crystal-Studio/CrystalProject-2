using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_InteractWithPNJWithZoom : MonoBehaviour
{
    public Texture2D cursor;
    public Transform target;

    [HideInInspector]
    public List<String> dialogs = new List<String>();

    private List<Func<IEnumerator>> call = new List<Func<IEnumerator>>();

    void Start()
    {
        dialogs = new List<String>(GetComponent<GetDialogFromFile>().GetDialog(PlayerPrefs.GetInt("Language")));
    }

    private void Update()
    {
      //  Debug.Log("Error : " + dialogBox + " - " + dialogBase + " " + dialogGraph.Dialogs[idDialog]);
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
