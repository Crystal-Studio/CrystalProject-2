using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class E_changeScene : MonoBehaviour
{
    public GameObject pos;
    public int sceneID;
    public Texture2D cursor;

    private GameObject player;

    public void OnInteract()
    {
        player = GameObject.Find("Player");
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
       /* player.GetComponent<E_Movement>().SetDestination(transform.GetChild(0).position, e_actionType.TALK);
        player.GetComponent<E_PlayerManager>().SetInteractableObject(gameObject);*/
    }
}
