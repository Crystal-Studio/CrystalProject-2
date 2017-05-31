using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Charge : MonoBehaviour
{
    public float speed;
    public string tagName;
    public LayerMask layerCollider;

    public Vector2 size;

    GameObject player;

    private bool _isActive;
    private bool _isMoving;

    private Vector3 _startPos;
    private Vector3 _endPos;

    void Start ()
    {
        player = GM_Manager.instance.player;
    }
	
	void Update ()
    {
        if (!_isActive)
            return;

        if (Input.GetMouseButtonDown(0) && !_isMoving)
        {
            _isMoving = true;
            _startPos = player.transform.position;
            _endPos = player.transform.position + player.transform.forward * (size.y + 1.5f);
            player.GetComponent<NavMeshAgent>().enabled = false;
        }

        if (_isMoving)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, _endPos, speed * Time.deltaTime);

            if (Vector3.Distance(_startPos, player.transform.position) > size.y)
            {
                _isMoving = false;
                StartCoroutine(OnEndSpell());
            }

            RaycastHit rhit;
            if (Physics.Raycast(player.transform.position, player.transform.forward, out rhit, 1f, layerCollider))
            {
                if (rhit.transform.tag == tagName)
                {
                    
                    Destroy(rhit.transform.gameObject);
                    StartCoroutine(OnEndSpell());
                    _isMoving = false;
                    GM_Manager.instance.GetComponent<NavMeshSurface>().BuildNavMesh();
                }
            }
        }
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

        player.transform.GetChild(4).gameObject.SetActive(false);
        player.GetComponent<E_pManager>().SetRotate(true);
        player.GetComponent<E_pManager>().SetTimerSpell(Time.time);
        player.GetComponent<E_pManager>().SetCasting(false);
        player.GetComponent<NavMeshAgent>().enabled = true;
        yield return new WaitForSeconds(0.15f);
        player.GetComponent<E_pManager>().SetMove(true);
    }
}
