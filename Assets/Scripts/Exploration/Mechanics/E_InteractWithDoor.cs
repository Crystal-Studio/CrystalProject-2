using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class E_InteractWithDoor : MonoBehaviour
{
    public Texture2D cursor;
    public Transform target;
    public GameObject door;

    public CS_DialogGraph dialogGraph;
    public int idDialog;

    [SerializeField] private float _speed;
    [SerializeField] private bool _open;

    private bool _changeStateDoor;

    private CS_DialogBase dialogBase;
    private CS_DialogBox dialogBox;

    private List<String> dialogs = new List<String>();
    private List<Func<IEnumerator>> call = new List<Func<IEnumerator>>();

    void Start ()
    {
        dialogBase = new CS_DialogBase();
        dialogBox = new CS_DialogBox();

        dialogBase = dialogGraph.Dialogs[idDialog];
        dialogBox = (CS_DialogBox)dialogBase;
        dialogs.AddRange(dialogBox.dialogs);

        _changeStateDoor = false;
    }

	void Update ()
    {
		if (_changeStateDoor)
        {
            door.transform.localEulerAngles = Vector3.MoveTowards(door.transform.localEulerAngles, Vector3.zero, _speed * Time.deltaTime);

            if (door.transform.localEulerAngles == Vector3.zero)
                _changeStateDoor = false;
        }
	}

    public void OnInteractStart()
    {
        call.Add(ShowContent);
        StartCoroutine(DoActions());
    }

    public void OnInteractSwitchStart()
    {
        GM_Manager.instance.player.GetComponent<E_pManager>().SetMove(false);
        Camera.main.GetComponent<E_Camera>().SetTarget(door.transform.position);
        call.Add(Camera.main.GetComponent<E_Camera>().CameraMoveTo);
        call.Add(OpenDoor);
        call.Add(Camera.main.GetComponent<E_Camera>().CameraZoomOUT);
        call.Add(HeroMove);
        StartCoroutine(DoActions());
        _open = true;
    }

    public void OnInteractSwitchEnd()
    {

    }


    public void OnInteractEnd()
    {
        call.Add(HideContent);
        call.Add(HeroMove);
        StartCoroutine(DoActions());
    }

    IEnumerator DoActions()
    {
        foreach (Func<IEnumerator> func in call)
            yield return func.Invoke();
        call.Clear();
        
    }

    IEnumerator OpenDoor()
    {
        _changeStateDoor = true;
        while(_changeStateDoor)
            yield return null;
        yield return new WaitForSeconds(0.1f);
    }


    IEnumerator ShowContent()
    {
        GM_Manager.instance.e_dialogManager.OnStart(dialogs);
        yield return new WaitForSeconds(0.1f);
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

    private void OnMouseEnter()
    {
        if (!_open)
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        if (!_open)
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseDown()
    {
        if (!_open)
            GM_Manager.instance.player.GetComponent<E_pManager>().Interact(gameObject, target.position);
    }
}
