using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum e_playerState
{
    IDLE,
    MOVEMENT,
    CAST,
    PICKUP,
    TALK
};

public class E_PlayerManager : MonoBehaviour
{
    public e_playerState playerState;

    public E_Dialog s_pDialog;
    public E_Movement s_pMovement;

    public GameObject interactable;

    [Header("Timer")]
    public float crystalTime;
    public float rockTime;
    public float plantTime;

    [HideInInspector]public Animator anim;

    private List<Func<IEnumerator>> call = new List<Func<IEnumerator>>();


    private void Start()
    {
        anim = transform.GetChild(PlayerPrefs.GetInt("CurrentSelectHeros")).GetComponent<Animator>();
        ChangeHero(PlayerPrefs.GetInt("CurrentSelectHeros"));
    }

    public void EndDigging()
    {
        playerState = e_playerState.IDLE;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Heros_1"))
            ChangeHero(0);
        if (Input.GetButtonDown("Heros_2"))
            ChangeHero(1);
        if (Input.GetButtonDown("Heros_3"))
            ChangeHero(2);
    }

    private void ChangeHero(int i)
    {
        PlayerPrefs.SetInt("CurrentSelectHeros", i);
        anim = transform.GetChild(PlayerPrefs.GetInt("CurrentSelectHeros")).GetComponent<Animator>();

        foreach (Transform item in transform)
        {
            item.gameObject.SetActive(false);
        }

        transform.GetChild(i).gameObject.SetActive(true);
    }

    public void EnabledDialog()
    {
        playerState = e_playerState.TALK;

        gameObject.transform.LookAt(interactable.transform.position);
        gameObject.transform.eulerAngles = new Vector3(0, gameObject.transform.eulerAngles.y, 0);

        call.Add(Camera.main.gameObject.GetComponent<E_Camera>().CameraZoomIN);
        call.Add(ShowContent);
        StartCoroutine(StartDialog());
    }

    public IEnumerator StartDialog()
    {
        foreach (Func<IEnumerator> func in call)
            yield return func.Invoke();
        call.Clear();
    }

    public void DisabledDialog()
    {
        call.Add(GM_Manager.instance.e_dialogManager.OnEnd);
        call.Add(Camera.main.gameObject.GetComponent<E_Camera>().CameraZoomOUT);
        call.Add(SetStateEndDialog);
        StartCoroutine(EndDialog());
    }

    public IEnumerator EndDialog()
    {
        foreach (Func<IEnumerator> func in call)
            yield return func.Invoke();
        call.Clear();
    }

    IEnumerator ShowContent()
    {
        GM_Manager.instance.e_dialogManager.OnStart(interactable.GetComponent<E_PNJ>().dialogs);
        yield return null;
    }

    public void SetInteractableObject(GameObject g)
    {
        interactable = g;
    }

    IEnumerator SetStateEndDialog()
    {
        yield return new WaitForSeconds(0.15f);
        playerState = e_playerState.IDLE;
    }
}
