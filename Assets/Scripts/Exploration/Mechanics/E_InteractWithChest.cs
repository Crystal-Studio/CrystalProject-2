using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum e_chestItem
{
    KEY,
    PLAN,
    COMPASS,
};

public class E_InteractWithChest : MonoBehaviour
{
    public Texture2D cursor;
    public Transform target;

    public e_chestItem chestItem;

    [SerializeField] private bool _open;

    private bool _openChest;

    private List<String> dialogs = new List<String>();
    private List<Func<IEnumerator>> call = new List<Func<IEnumerator>>();

    void Start()
    {
        dialogs = new List<String>(GetComponent<GetDialogFromFile>().GetDialog(PlayerPrefs.GetInt("Language")));

        CheckState();

        _openChest = false;
    }

    void CheckState()
    {
        switch (chestItem)
        {
            case e_chestItem.KEY:
                if (PlayerPrefs.GetInt(GM_Manager.instance.zoneKey + "_key") == 1)
                    _open = true;
                break;
            case e_chestItem.PLAN:
                if (PlayerPrefs.GetInt(GM_Manager.instance.zoneKey + "_plan") == 1)
                    _open = true;
                break;
            case e_chestItem.COMPASS:
                if (PlayerPrefs.GetInt(GM_Manager.instance.zoneKey + "_compass") == 1)
                    _open = true;
                break;
            default:
                break;
        }

        if (_open)
            GetComponent<Animation>().Play();
    }

    public void OnInteractStart()
    {
        if (_open)
            return;
        call.Add(Camera.main.GetComponent<E_Camera>().CameraZoomIN);
        call.Add(OpenChest);
        call.Add(ShowContent);
        StartCoroutine(DoActions());

        AddItem();
    }

    public void OnInteractEnd()
    {
        call.Add(HideContent);
        call.Add(Camera.main.GetComponent<E_Camera>().CameraZoomOUT);
        call.Add(HeroMove);
        StartCoroutine(DoActions());
    }

    void AddItem()
    {
        switch (chestItem)
        {
            case e_chestItem.KEY:
                PlayerPrefs.SetInt(GM_Manager.instance.zoneKey + "_key", 1);
                break;
            case e_chestItem.PLAN:
                PlayerPrefs.SetInt(GM_Manager.instance.zoneKey + "_plan", 1);
                break;
            case e_chestItem.COMPASS:
                PlayerPrefs.SetInt(GM_Manager.instance.zoneKey + "_compass", 1);
                break;
            default:
                break;
        }
    }

    IEnumerator DoActions()
    {
        foreach (Func<IEnumerator> func in call)
            yield return func.Invoke();
        call.Clear();
    }

    IEnumerator OpenChest()
    {
        GetComponent<Animation>().Play();
        yield return new WaitForSeconds(0.75f);
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
