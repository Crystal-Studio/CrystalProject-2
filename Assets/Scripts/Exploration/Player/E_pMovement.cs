using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CrystalStudioTools;
using System;

public class E_pMovement : MonoBehaviour
{
    [Tooltip("Layer(s) à prendre en compte pour le déplacement possible")]
    public LayerMask layer;
    [Tooltip("Distance max à laquelle le joueur peut interagir")]
    public float maxDistanceInteract;

    private NavMeshAgent _navMesh;
    private NavMeshPath _navPath;
    private NavMeshHit _navHit;

    private E_pManager s_pManager;

    private bool _hasTarget;

    void Start()
    {
        _navPath = new NavMeshPath();
        _navMesh = GetComponent<NavMeshAgent>();
        s_pManager = GetComponent<E_pManager>();
    }

    private void Update()
    {
        if (_hasTarget && transform.position == _navMesh.destination)
        {
            _hasTarget = false;
            s_pManager.InteractReachedPosition();
        }

        if (transform.position == _navMesh.destination)
            StopMovement();
    }

    public void Move()
    {
        _hasTarget = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 point = ray.origin + (ray.direction * 50.0f);
        Vector3 direction = Tools.GetDirection(point, Camera.main.transform.position);

        RaycastHit rhit;
        if (Physics.Raycast(Camera.main.transform.position, direction, out rhit, (point - Camera.main.transform.position).magnitude, layer))
            point = rhit.point;

        if (NavMesh.SamplePosition(point, out _navHit, 700, NavMesh.AllAreas))
        {
            Debug.DrawRay(_navHit.position, Vector3.up, Color.yellow, 10.0f);
            _navMesh.destination = _navHit.position;
            _navMesh.isStopped = false;

            s_pManager.playerState = e_playerState.MOVEMENT;
            NavMesh.CalculatePath(gameObject.transform.position, _navHit.position, NavMesh.AllAreas, _navPath);
            s_pManager.GetAnimator().SetFloat("moveSpeed", 0.1f);
        }
    }

    public void MoveToTarget(Vector3 target)
    {
        _hasTarget = true;
        if (NavMesh.SamplePosition(target, out _navHit, 700, NavMesh.AllAreas))
        {
            Debug.DrawRay(_navHit.position, Vector3.up, Color.yellow, 10.0f);
            _navMesh.destination = _navHit.position;
            _navMesh.isStopped = false;
            Debug.Log(_navHit.position);

            s_pManager.playerState = e_playerState.MOVEMENT;
            NavMesh.CalculatePath(gameObject.transform.position, _navHit.position, NavMesh.AllAreas, _navPath);
        }
    }

    public void StopMovement()
    {
        s_pManager.GetAnimator().SetFloat("moveSpeed", 0f);
        _navMesh.destination = transform.position;
        _navMesh.isStopped = true;
        s_pManager.playerState = e_playerState.IDLE;
        s_pManager.SetRotate(true);
    }


}

