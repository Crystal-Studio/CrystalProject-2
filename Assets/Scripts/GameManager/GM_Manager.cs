using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
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
    public GameObject spellBar;

    private Vector2 _size;

    private bool _spellBar;
    private float _timer;
    private float _timerSpell;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        player = GameObject.Find("Player");
        _size = spellBar.transform.GetChild(1).transform.GetComponent<RectTransform>().sizeDelta;
    }

    private void Update()
    {
        if (_spellBar)
        {
            spellBar.transform.GetChild(1).transform.GetComponent<RectTransform>().sizeDelta = new Vector2(
                (Time.time - _timerSpell) * _size.x / _timer,
                spellBar.transform.GetChild(1).transform.GetComponent<RectTransform>().sizeDelta.y);

            if (spellBar.transform.GetChild(1).transform.GetComponent<RectTransform>().sizeDelta.x >= _size.x)
            {
                spellBar.SetActive(false);
                _spellBar = false;
            }
        }
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
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    public void DisplaySpellBar(float f, float t)
    {
        _spellBar = true;
        spellBar.transform.GetChild(1).transform.GetComponent<RectTransform>().sizeDelta = _size;
        spellBar.SetActive(true);
        _timer = f;
        _timerSpell = t;
    }
}
