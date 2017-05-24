using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum resources
{
    CRYSTAL,
    ROCK,
    PLANT
}


public class E_Resources : MonoBehaviour
{
    public resources resourcesType;
    public Texture2D cursor;

    private GameObject player;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");	
	}
	
	// Update is called once per frame
	void Update () {
		
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
        player.GetComponent<E_Movement>().MoveResource((int)resourcesType, transform.position);
    }
}
