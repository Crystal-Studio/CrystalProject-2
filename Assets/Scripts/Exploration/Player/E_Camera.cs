using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum e_actionCamera
{
    DEFAULT,
    ZOOMIN,
    ZOOMOUT
}

public class E_Camera : MonoBehaviour
{
    public float speed;
	public GameObject player;
    public Vector3 offsetDefault;
    public Vector3 offsetEoomIn;

    private e_actionCamera actionCamera;

    private bool _next;

    private List<GameObject> go = new List<GameObject>();

	void Start ()
    {
        actionCamera = e_actionCamera.DEFAULT;
        _next = false;
	}

	void Update ()
    {
        switch (actionCamera)
        {
            case e_actionCamera.DEFAULT:
                Vector3 targetDefault = new Vector3(player.transform.position.x + offsetDefault.x,
                                         player.transform.position.y + offsetDefault.y,
                                         player.transform.position.z + offsetDefault.z);

                transform.position = Vector3.MoveTowards(transform.position, targetDefault, speed * Time.deltaTime);
                break;
            case e_actionCamera.ZOOMIN:
                Vector3 targetZoomIn = new Vector3(player.transform.position.x + offsetEoomIn.x,
                                         player.transform.position.y + offsetEoomIn.y,
                                         player.transform.position.z + offsetEoomIn.z);

                transform.position = Vector3.MoveTowards(transform.position, targetZoomIn, speed / 5 * Time.deltaTime);

                if (transform.position == targetZoomIn)
                    _next = true;
                break;
            case e_actionCamera.ZOOMOUT:
                Vector3 targetZoomOut = new Vector3(player.transform.position.x + offsetDefault.x,
                                         player.transform.position.y + offsetDefault.y,
                                         player.transform.position.z + offsetDefault.z);

                transform.position = Vector3.MoveTowards(transform.position, targetZoomOut, speed / 5 * Time.deltaTime);

                if (transform.position == targetZoomOut)
                    _next = true;
                break;
        }
	}

    public IEnumerator CameraZoomIN()
    {
        _next = false;
        actionCamera = e_actionCamera.ZOOMIN;

        while (_next == false)
            yield return null;
        yield return new WaitForSeconds(0.15f);
    }

    public IEnumerator CameraZoomOUT()
    {
        _next = false;
        actionCamera = e_actionCamera.ZOOMOUT;

        while (_next == false)
            yield return null;
        yield return new WaitForSeconds(0.15f);
    }
}
