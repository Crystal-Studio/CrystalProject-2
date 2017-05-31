using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum e_actionCamera
{
    DEFAULT,
    ZOOMIN,
    ZOOMOUT,
    MOVETO,
    EARTHQUAKE
}

public class E_Camera : MonoBehaviour
{
    public float speed;
	public GameObject player;
    public Vector3 offsetDefault;
    public Vector3 offsetEoomIn;

    /* SHAKE */
    Vector3 _originalPos;
    // How long the object should shake for.
    public float shakeDuration;
    public float shakeAmount;
    public float decreaseFactor;
    /* *** ENDSHAKE *** */

    private e_actionCamera actionCamera;

    private bool _next;
    private Vector3 _target;

    private List<GameObject> go = new List<GameObject>();

	void Start ()
    {
        actionCamera = e_actionCamera.DEFAULT;
        _next = false;
	}

	void Update ()
    {
        Debug.Log(actionCamera);
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
            case e_actionCamera.MOVETO:
                Vector3 targetMoveTo = new Vector3(_target.x + offsetDefault.x,
                                         _target.y + offsetDefault.y,
                                         _target.z + offsetDefault.z);

                transform.position = Vector3.MoveTowards(transform.position, targetMoveTo, speed / 5 * Time.deltaTime);

                if (transform.position == targetMoveTo)
                    _next = true;
                break;
            case e_actionCamera.EARTHQUAKE:
                if (shakeDuration > 0)
                {
                    transform.position = new Vector3(player.transform.position.x + offsetDefault.x,
                                         player.transform.position.y + offsetDefault.y,
                                         player.transform.position.z + offsetDefault.z) + Random.insideUnitSphere * shakeAmount;
                    transform.position = new Vector3(transform.position.x, player.transform.position.y + offsetDefault.y, transform.position.z);

                    shakeDuration -= Time.deltaTime * decreaseFactor;
                }
                else
                {
                    shakeDuration = 0f;
                    transform.position = new Vector3(player.transform.position.x + offsetDefault.x,
                                         player.transform.position.y + offsetDefault.y,
                                         player.transform.position.z + offsetDefault.z);
                    actionCamera = e_actionCamera.DEFAULT;
                    _next = true;
                }

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

    public void SetTarget(Vector3 v)
    {
        _target = v;
    }

    public IEnumerator CameraMoveTo()
    {
        yield return new WaitForSeconds(0.35f);
        _next = false;
        actionCamera = e_actionCamera.MOVETO;

        while (_next == false)
            yield return null;
        yield return new WaitForSeconds(0.15f);
    }

    public IEnumerator EarthQUake()
    {
        shakeDuration = 0.5f;
        shakeAmount = 0.2f;
        decreaseFactor = 1;

        player.GetComponent<E_pManager>().SetMove(false);
        _originalPos = transform.position;
        _next = false;
        actionCamera = e_actionCamera.EARTHQUAKE;
      
        while (_next)
            yield return null;
 
        yield return new WaitForSeconds(0.75f);

        player.GetComponent<E_pManager>().SetMove(true);

    }
}
