using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class E_InteractChangeScene : MonoBehaviour
{
    public Texture2D cursor;
    public Transform target;

    public Vector3 playerPosition;

    AsyncOperation async;

    private List<Func<IEnumerator>> call = new List<Func<IEnumerator>>();

    public int idScene;


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
        
        async = SceneManager.LoadSceneAsync(idScene);
        async.allowSceneActivation = false;

        call.Add(GM_Manager.instance.s_fade.FadeIN);
        call.Add(LoadScene);
        StartCoroutine(DoActions());
    }

    IEnumerator LoadScene()
    {
        GM_Manager.instance.player.transform.position = Vector3.zero;
        Camera.main.transform.position = Vector3.zero;
        GM_Manager.instance.gameObject.transform.position = playerPosition;
        async.allowSceneActivation = true;
        yield return null;
    }

    IEnumerator DoActions()
    {
        foreach (Func<IEnumerator> func in call)
            yield return func.Invoke();
        call.Clear();
    }
}
