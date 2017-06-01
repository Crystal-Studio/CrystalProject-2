using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using CrystalStudioTools;

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
    private bool _canRotate;

    private float _timeButtonClickMovement;

    private Vector3 _targetPosition;
    /* *** EndMovement *** */

    /* *** Interact *** */
    [Header("Interact")]
    public float maxDistance;

    private GameObject interactObj;
    /* *** EndInteract *** */

    /* *** Spell *** */
    [Header("Spell")]
    private bool _isCasting;

    public float timerSpell;
    private float _timeSpell;

    enum e_spell
    {
        TELEKINESIE,
        CHARGE,
        GRAPPIN
    };

    e_spell currentSpell;
    /* *** EndSpell *** */

    private void Start()
    {
        s_pMovement = GetComponent<E_pMovement>();
        s_pSwitch = GetComponent<E_pSwitch>();

        _canMove = true;
        _canRotate = true;
        _anim = transform.GetChild(PlayerPrefs.GetInt("CurrentSelectHeros")).GetComponent<Animator>();

       // ChangeHero(PlayerPrefs.GetInt("CurrentSelectHeros"));
    }

    public void EndDigging()
    {
        playerState = e_playerState.IDLE;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            GM_Manager.instance.GetComponent<NavMeshSurface>().BuildNavMesh();
        }
        if (_isCasting == false)
        { 
            #region Movement Input
            if (Input.GetMouseButtonDown(0))
            {
                _followCursor = !_hasTarget;
                if (_hasTarget)
                    _hasTarget = false;
                _timeButtonClickMovement = Time.time;
                _canRotate = false;
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

            if (Camera.main.GetComponent<E_Camera>().GetCameraState() != e_actionCamera.DEFAULT)
                s_pMovement.StopMovement();
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

        #region Spell
        if (Input.GetButtonDown("Spell_A") && !_isCasting)
            CastSpell();
           
        if (Input.GetMouseButtonDown(1))
            DecastSpell();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 point = ray.origin + (ray.direction * 50.0f);
        Vector3 direction = Tools.GetDirection(point, Camera.main.transform.position);

        RaycastHit rhit;
        if (Physics.Raycast(Camera.main.transform.position, direction, out rhit, (point - Camera.main.transform.position).magnitude))
            point = rhit.point;

        if (_canRotate)
        {
            transform.LookAt(point);
            transform.eulerAngles = new Vector3(0, gameObject.transform.eulerAngles.y, 0);
        }
        #endregion
    }

    void CastSpell()
    {
        if (Time.time < _timeSpell + timerSpell)
            return;
       
        _isCasting = true;
        _canMove = false;
        s_pMovement.StopMovement();
        GM_Manager.instance.spell.transform.GetChild(PlayerPrefs.GetInt("CurrentSelectHeros")).GetChild(0).SendMessage("OnCastSpell");
    }

    void DecastSpell()
    {
        _isCasting = false;
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

   /* public void StopCastInteract()
    {
        _isCasting = false;
        GM_Manager.instance.spell.transform.GetChild(PlayerPrefs.GetInt("CurrentSelectHeros")).GetChild(0).SendMessage("StopSpellInteract");
        GM_Manager.instance.spell.transform.GetChild(PlayerPrefs.GetInt("CurrentSelectHeros")).GetChild(1).SendMessage("StopSpellInteract");
    }*/

    

    #region Setter & Getter
    public void SetAnimator(Animator anim) { _anim = anim; }

    public Animator GetAnimator() { return _anim; }

    public void SetMove(bool b)
    {
        _canMove = b;
        if (b == false)
            s_pMovement.StopMovement();
    }

    public void SetSpell(int i) { currentSpell = (e_spell)i; }

    public void SetCasting(bool b) { _isCasting = false; }
   
    public bool GetCasting() { return _isCasting; }

    public void SetRotate(bool b) { _canRotate = b; }

    public void SetTimerSpell(float f)
    {
        _timeSpell = f;
        GM_Manager.instance.DisplaySpellBar(timerSpell, f);
    }
    #endregion

}
