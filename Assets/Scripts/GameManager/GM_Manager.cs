using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_Manager : MonoBehaviour
{
    public static GM_Manager instance = null;

    public E_DialogManager e_dialogManager;

    public GameObject player;

    [Header("Cursor")]
    public Cursor[] cursors;

    [Header("Spells")]
    public GameObject spell;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        player = GameObject.Find("Player");
    }
}
