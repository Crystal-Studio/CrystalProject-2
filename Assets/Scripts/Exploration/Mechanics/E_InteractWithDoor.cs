using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class E_InteractWithDoor : MonoBehaviour
{
    public Texture2D cursor;
    public Transform target;
    public GameObject door;

    [SerializeField] private float _speed;
    [SerializeField] private bool _open;

    private bool _changeStateDoor;

    private List<String> dialogs = new List<String>();
    private List<Func<IEnumerator>> call = new List<Func<IEnumerator>>();

    void Start ()
    {
        dialogs = new List<String>(GetComponent<GetDialogFromFile>().GetDialog(PlayerPrefs.GetInt("Language")));

        _changeStateDoor = false;
    }

	void Update ()
    {
        if (_changeStateDoor)
        {
            if (_open)
            {
                door.transform.localEulerAngles = Vector3.MoveTowards(door.transform.localEulerAngles, Vector3.zero, _speed * Time.deltaTime);

                if (door.transform.localEulerAngles == Vector3.zero)
                    _changeStateDoor = false;
            }
            else
            {
                door.transform.localEulerAngles = Vector3.MoveTowards(door.transform.localEulerAngles, new Vector3(0, 90, 0), _speed * Time.deltaTime);

                if (door.transform.localEulerAngles == new Vector3(0, 90, 0))
                    _changeStateDoor = false;
            }
            
        }
	}

    public void OnInteractStart()
    {
        call.Add(ShowContent);
        StartCoroutine(DoActions());
    }

    public void OnInteractSwitchStart()
    {
        _open = true;
        GM_Manager.instance.player.GetComponent<E_pManager>().SetMove(false);
        Camera.main.GetComponent<E_Camera>().SetTarget(door.transform.position);
        call.Add(Camera.main.GetComponent<E_Camera>().CameraMoveTo);
        call.Add(OpenDoor);
        call.Add(Camera.main.GetComponent<E_Camera>().CameraZoomOUT);
        call.Add(HeroMove);
        StartCoroutine(DoActions());
        
    }

    public void OnInteractSwitchEnd()
    {
        _open = false;
        GM_Manager.instance.player.GetComponent<E_pManager>().SetMove(false);
        Camera.main.GetComponent<E_Camera>().SetTarget(door.transform.position);
        call.Add(Camera.main.GetComponent<E_Camera>().CameraMoveTo);
        call.Add(CloseDoor);
        call.Add(Camera.main.GetComponent<E_Camera>().CameraZoomOUT);
        call.Add(HeroMove);
        StartCoroutine(DoActions());
    }

    public void OnInteractWithoutCamera()
    {

        StartCoroutine(Camera.main.GetComponent<E_Camera>().EarthQUake());
        door.transform.localEulerAngles = new Vector3(0,0,0);
       
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

    IEnumerator CloseDoor()
    {
        _changeStateDoor = true;
        while (_changeStateDoor)
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
