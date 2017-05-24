using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CrystalStudioTools;
using System;

public enum e_actionType
{
    NOTHING,
    CAST,
    PICKUP,
    TALK
}

public class E_Movement : MonoBehaviour
{
    [Header("NavMesh Utility")]
    public LayerMask layer;
    public float maxDistance;
    [Tooltip("Temps de pression pour que l'unité suive le curseur")]
    public float timeButtonPressed;


    private float _time;
    private bool _followCursorMovement;
    private int _resourceType;



    private Vector3 _targetPos;
    private bool _hasTarget;
    private e_actionType actionType;

    private NavMeshAgent _navMesh;
    private NavMeshPath _navPath;
    private NavMeshHit _navHit;
    private E_PlayerManager s_pManager;

    private bool collect;
    private Vector3 digTarget;

    void Start()
    {
        _hasTarget = false;
        _navPath = new NavMeshPath();
        _navMesh = GetComponent<NavMeshAgent>();
        s_pManager = GetComponent<E_PlayerManager>();
    }

    void Update()
    {
        if (s_pManager.playerState != e_playerState.MOVEMENT && s_pManager.playerState != e_playerState.IDLE)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 point = ray.origin + (ray.direction * 50.0f);
        Vector3 direction = Tools.GetDirection(point, Camera.main.transform.position);

        RaycastHit rhit;
        if (Physics.Raycast(Camera.main.transform.position, direction, out rhit, (point - Camera.main.transform.position).magnitude, layer))
            point = rhit.point;

        Debug.Log(_hasTarget + " - " + _followCursorMovement);

        if (Input.GetMouseButtonDown(0))
        {
            _followCursorMovement = !_hasTarget;
            if (_hasTarget)
                _hasTarget = false;
            _time = Time.time;
        }

        if (Input.GetMouseButton(0) && _followCursorMovement)
        {
            if (NavMesh.SamplePosition(point, out _navHit, 5, NavMesh.AllAreas))
            {
                //Debug.DrawRay(_navHit.position, Vector3.up, Color.red, 3.0f);
                _navMesh.destination = _navHit.position;
                s_pManager.playerState = e_playerState.MOVEMENT;
                NavMesh.CalculatePath(gameObject.transform.position, _navHit.position, NavMesh.AllAreas, _navPath);
                s_pManager.anim.SetFloat("moveSpeed", 0.1f);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _time = Time.time - _time;

            if (_time > timeButtonPressed)
                StopMovement();
        }

        if (_navMesh.velocity == Vector3.zero && _navMesh.destination == transform.position)
        {
            gameObject.transform.LookAt(point);
            gameObject.transform.eulerAngles = new Vector3(0, gameObject.transform.eulerAngles.y, 0);
            StopMovement();

            switch (actionType)
            {
                case e_actionType.NOTHING:
                    break;
                case e_actionType.CAST:
                    break;
                case e_actionType.PICKUP:
                    break;
                case e_actionType.TALK:
                    s_pManager.EnabledDialog();
                    _followCursorMovement = true;
                     actionType = e_actionType.NOTHING;
                    break;
                default:
                    break;
            }
        }
    }

    public void StopMovement()
    {
        s_pManager.anim.SetFloat("moveSpeed", 0f);
        _navMesh.destination = transform.position;
        s_pManager.playerState = e_playerState.IDLE;
    }

    public void MoveResource(int i, Vector3 pos)
    {
        _targetPos = pos;
        _resourceType = i;

        if (Vector3.Distance(transform.position, pos) <= maxDistance)
        {
            StopMovement();
            s_pManager.anim.SetTrigger("Dig");
            s_pManager.playerState = e_playerState.PICKUP;
            gameObject.transform.LookAt(_targetPos);
            gameObject.transform.eulerAngles = new Vector3(0, gameObject.transform.eulerAngles.y, 0);
            StartCoroutine(SetStateAfterResources());
        }
        else
        {
            _hasTarget = true;
           
            if (NavMesh.SamplePosition(pos, out _navHit, 5, NavMesh.AllAreas))
            {
                //Debug.DrawRay(_navHit.position, Vector3.up, Color.red, 3.0f);
                _navMesh.destination = _navHit.position;
                s_pManager.playerState = e_playerState.MOVEMENT;
                NavMesh.CalculatePath(gameObject.transform.position, _navHit.position, NavMesh.AllAreas, _navPath);
                s_pManager.anim.SetFloat("moveSpeed", 0.1f);
            }
        }
    }

    
    IEnumerator SetStateAfterResources()
    {
        float f = 1;

        switch (_resourceType)
        {
            case 0:
                f = s_pManager.crystalTime;
                break;
            case 1:
                f = s_pManager.rockTime;
                break;
            case 2:
                f = s_pManager.plantTime;
                break;
            default:
                break;
        }

        yield return new WaitForSeconds(f);
        s_pManager.playerState = e_playerState.IDLE;
    }

    public void SetDestination(Vector3 v, e_actionType type)
    {
        if (NavMesh.SamplePosition(v, out _navHit, 5, NavMesh.AllAreas))
        {
            _hasTarget = true;
            _navMesh.destination = _navHit.position;
            _targetPos = _navHit.position;
            s_pManager.anim.SetFloat("moveSpeed", 0.1f);
            actionType = type;
        }     
    }



}

