using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CrystalStudioTools;

public class Telekinesie : MonoBehaviour
{
    public float speed;
    public LayerMask layerCatchObject;
    public LayerMask layerDropObject;
    public GameObject currentObject;

    public float maxDistance;

    private int _state;
    private bool _isActive;
    private bool _nextAction;

    private NavMeshHit _navHit;
    private Vector3 _startPos;

    private GameObject player;

	// Use this for initialization
	void Start ()
    {
        currentObject = null;
        _state = 0;
        _isActive = false;

        player = GM_Manager.instance.player;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!_isActive)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 point = ray.origin + (ray.direction * 50.0f);
        Vector3 direction = Tools.GetDirection(point, Camera.main.transform.position);

        if (Input.GetMouseButtonDown(0) && _state == 0)
        {
            _nextAction = false;
            RaycastHit rhit;
            if (Physics.Raycast(Camera.main.transform.position, direction, out rhit, (point - Camera.main.transform.position).magnitude, layerCatchObject))
            {
                Debug.DrawRay(_navHit.position, Vector3.up * 5, Color.blue, 10.0f);

                if (Vector3.Distance(player.transform.position, rhit.transform.position) <= maxDistance)
                {
                    currentObject = rhit.transform.gameObject;
                    _startPos = currentObject.transform.position;
                    _state = 1;

                    currentObject.GetComponent<Rigidbody>().detectCollisions = false;

                    player.transform.GetChild(3).gameObject.SetActive(false);
                }
            }
        }
        
        if (_state == 1 )
        {
            RaycastHit rhit;
            if (Physics.Raycast(Camera.main.transform.position, direction, out rhit, (point - Camera.main.transform.position).magnitude, layerDropObject))
                point = rhit.point;

            if (NavMesh.SamplePosition(point, out _navHit, 700, NavMesh.AllAreas))
            {
                currentObject.transform.position = Vector3.MoveTowards(currentObject.transform.position, _navHit.position, speed * Time.deltaTime);

                Debug.DrawRay(_navHit.position, Vector3.up * 5, Color.blue, 10.0f);
            }

            if (Input.GetMouseButtonDown(0) && _nextAction)
            {
                currentObject.GetComponent<Rigidbody>().detectCollisions = true;
                StartCoroutine(OnEndSpell());
                _isActive = false;
            }
        }

        if (Input.GetMouseButtonUp(0))
            _nextAction = true;

        if (Input.GetMouseButtonDown(1))
            OnBack();
    }

    void OnBack()
    {
        _state -= 1;

        if (_state == -1)
        {
            StartCoroutine(OnEndSpell());
            _isActive = false;
        }
        if (_state == 0)
        {
            currentObject.transform.position = _startPos;
            player.transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    public void OnCastSpell()
    {
        _isActive = true;
        currentObject = null;
        _state = 0;

        player.transform.GetChild(3).localScale = new Vector3(maxDistance, 1, maxDistance);
        player.transform.GetChild(3).gameObject.SetActive(true);
    }

    IEnumerator OnEndSpell()
    {
        player.GetComponent<E_pManager>().SetTimerSpell(Time.time);
        player.transform.GetChild(3).gameObject.SetActive(false);
        player.GetComponent<E_pManager>().SetCasting(false);
        yield return new WaitForSeconds(0.15f);
        if (Camera.main.GetComponent<E_Camera>().GetCameraState() == e_actionCamera.DEFAULT)
             player.GetComponent<E_pManager>().SetMove(true);
    }

}
