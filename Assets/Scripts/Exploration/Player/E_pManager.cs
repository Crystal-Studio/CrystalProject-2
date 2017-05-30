using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public enum e_playerState
{
    IDLE,
    MOVEMENT,
    CAST,
    INTERACT
};

[RequireComponent(typeof(E_pMovement))]
[RequireComponent(typeof(E_pSwitch))]
public class E_pManager : MonoBehaviour
{
    [HideInInspector] public e_playerState playerState;
    [HideInInspector] public GameObject interactable;

    private Animator _anim;
    private E_pMovement s_pMovement;
    private E_pSwitch s_pSwitch;

    /* *** Movement *** */
    [Header("Mouvement")]
    public float timeButtonClickMovement;

    private bool _followCursor;
    private bool _hasTarget;
    private bool _canMove;

    private float _timeButtonClickMovement;

    private Vector3 _targetPosition;
    /* *** EndMovement *** */

    /* *** Interact *** */
    [Header("Interact")]
    public float maxDistance;

    private GameObject interactObj;

    /* *** EndInteract *** */

    private void Start()
    {
        s_pMovement = GetComponent<E_pMovement>();
        s_pSwitch = GetComponent<E_pSwitch>();

        _canMove = true;

        _anim = transform.GetChild(PlayerPrefs.GetInt("CurrentSelectHeros")).GetComponent<Animator>();

       // ChangeHero(PlayerPrefs.GetInt("CurrentSelectHeros"));
    }

    public void EndDigging()
    {
        playerState = e_playerState.IDLE;
    }

    private void Update()
    {
        #region Movement Input
        if (Input.GetMouseButtonDown(0))
        {
            _followCursor = !_hasTarget;
            if (_hasTarget)
                _hasTarget = false;
            _timeButtonClickMovement = Time.time;
        }

        if (Input.GetMouseButton(0) && _followCursor && _canMove)
        {
            s_pMovement.Move();
        }

        if (Input.GetMouseButtonUp(0))
        {
            _timeButtonClickMovement = Time.time - _timeButtonClickMovement;

            if (_timeButtonClickMovement > timeButtonClickMovement)
                s_pMovement.StopMovement();
        }
        #endregion

        #region Switch Hero
        if (Input.GetButtonDown("Heros_1"))
            s_pSwitch.ChangeHero(0);
        if (Input.GetButtonDown("Heros_2"))
            s_pSwitch.ChangeHero(1);
        if (Input.GetButtonDown("Heros_3"))
            s_pSwitch.ChangeHero(2);
        #endregion
    }

    public void Interact(GameObject obj, Vector3 target)
    {
        _hasTarget = true;
        interactObj = obj;
        s_pMovement.MoveToTarget(target);
    }

    public void InteractReachedPosition()
    {
        _canMove = false;
        interactObj.SendMessage("OnInteractStart");
    }

    public void EndInteract()
    {
        interactObj.SendMessage("OnInteractEnd");
    }

    #region Setter & Getter
    public void SetAnimator(Animator anim) { _anim = anim; }

    public Animator GetAnimator() { return _anim; }

    public void SetMove(bool b)
    {
        _canMove = b;
        if (b == false)
            s_pMovement.StopMovement();
    }
    #endregion

}
