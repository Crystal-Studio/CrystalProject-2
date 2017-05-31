using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CrystalStudioTools;

public class Grappin : MonoBehaviour
{
    public float speed;
    public LayerMask layerCollider;

    public Vector2 size;
    public GameObject arrow;

    private GameObject _arrow;

    private bool _arrowMovement;
    private bool _playerMovement;
    private bool _isActive;

    private NavMeshHit _navHit;
    private Vector3 _endPos;
    private Vector3 _startPos;

    private GameObject player;

	void Start ()
    {
        player = GM_Manager.instance.player;
    }
	
	void Update ()
    {
        if (!_isActive)
            return;

        if (_arrowMovement)
        {
            _arrow.transform.position += _arrow.transform.forward * speed * Time.deltaTime;

            if (Vector3.Distance(_startPos, _arrow.transform.position) > size.y)
            {
                _arrowMovement = false;
                Destroy(_arrow);
            }

            RaycastHit rhit;
            if (Physics.Raycast(_arrow.transform.position, _arrow.transform.forward, out rhit, 0.5f, layerCollider))
            {
                _endPos = Tools.GetPointDistanceFromObject(-1 * (rhit.transform.localScale.x / 2), rhit.point, player.transform.position);
                _playerMovement = true;
                _arrowMovement = false;
                player.GetComponent<NavMeshAgent>().enabled = false;
                Destroy(_arrow);
            }
        }

        if (_playerMovement)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, _endPos, speed * 1.5f * Time.deltaTime);

            if (player.transform.position == _endPos)
                _playerMovement = false;
        }

        if (Input.GetMouseButtonDown(0) && _arrowMovement == false && _playerMovement == false)
        {
            StartCoroutine(OnArrowMove());
        }
        
        if (Input.GetMouseButtonDown(1))
            OnBack();
    }

    void OnBack()
    {
        StartCoroutine(OnEndSpell());
        _isActive = false;
    }

    public void OnCastSpell()
    {
       _isActive = true;

        player.transform.GetChild(4).localScale = new Vector3(size.x / 10, 1, size.y / 10);
        player.transform.GetChild(4).gameObject.SetActive(true);
    }

    IEnumerator OnEndSpell()
    {
        _isActive = false;

        player.GetComponent<E_pManager>().SetTimerSpell(Time.time);
        player.GetComponent<E_pManager>().SetRotate(true);
        player.GetComponent<E_pManager>().SetCasting(false);
        yield return new WaitForSeconds(0.15f);
        player.GetComponent<E_pManager>().SetMove(true);
    }
    
    IEnumerator OnArrowMove()
    {
        player.GetComponent<E_pManager>().SetRotate(false);

        _arrowMovement = true;
        _playerMovement = false;
        _arrow = Instantiate(arrow, player.transform.position, Quaternion.identity) as GameObject;
        _arrow.transform.localEulerAngles = player.transform.localEulerAngles;

        player.transform.GetChild(4).gameObject.SetActive(false);

        _startPos = _arrow.transform.position;

        while (_arrowMovement)
            yield return null;

        while (_playerMovement)
            yield return null;

        player.GetComponent<NavMeshAgent>().enabled = true;

        StartCoroutine(OnEndSpell());
    }
}
