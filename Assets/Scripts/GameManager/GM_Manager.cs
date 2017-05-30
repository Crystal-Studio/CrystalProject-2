using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM_Manager : MonoBehaviour
{
    public static GM_Manager instance = null;

    public E_DialogManager e_dialogManager;
    public GM_Fade s_fade;

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

        Debug.Log("'sdfsd");

        player = GameObject.Find("Player");
        
    }

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(s_fade.FadeOUT());
        player.GetComponent<E_pManager>().SetMove(true);
    }
}
