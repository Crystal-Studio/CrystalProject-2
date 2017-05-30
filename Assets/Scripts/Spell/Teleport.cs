using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CrystalStudioTools;

public class Teleport : MonoBehaviour
{
    [Header("FeedBack")] 
    public GameObject zone;
    public float size;

    [Header("FeedBack")]
    public float maxDistance;

    [Header("NavMesh Utility")]
    public LayerMask layer;
    private NavMeshHit _navHit;

    private void Awake()
    {

    }

    public void OnClickEnable()
    {
       /* zone.SetActive(true);
        zone.transform.localScale = new Vector3(size, size, size);*/

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 point = ray.origin + (ray.direction * 50.0f);
        Vector3 direction = Tools.GetDirection(point, Camera.main.transform.position);

        RaycastHit rhit;
        if (Physics.Raycast(Camera.main.transform.position, direction, out rhit, (point - Camera.main.transform.position).magnitude, layer))
            point = rhit.point;

        if (NavMesh.SamplePosition(point, out _navHit, 5, NavMesh.AllAreas))
        {
           // Debug.Log("B : " + _navHit.position);
            //Debug.DrawRay(_navHit.position, Vector3.up, Color.red, 3.0f);

            StartCoroutine(SetPosition(_navHit.position));
          //  GameObject.Find("Player").transform.position = _;

          //  NavMesh.CalculatePath(gameObject.transform.position, _navHit.position, NavMesh.AllAreas, _navPath);

        }
        
    }

    public void OnClickDisable()
    {
        zone.SetActive(false);

    }

    private IEnumerator SetPosition(Vector3 target)
    {
        GameObject.Find("Player").GetComponent<NavMeshAgent>().enabled = false;
        GameObject.Find("Player").transform.GetChild(PlayerPrefs.GetInt("CurrentSelectHeros")).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        GameObject.Find("Player").transform.GetChild(PlayerPrefs.GetInt("CurrentSelectHeros")).gameObject.SetActive(true);
        Debug.Log(target);
        GameObject.Find("Player").transform.position = target;
        GameObject.Find("Player").GetComponent<NavMeshAgent>().enabled = true;

      //  GameObject.Find("Player").GetComponent<E_Movement>().StopMovement();

    }
}
