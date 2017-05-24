using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class move : MonoBehaviour
{
    public Texture2D[] cursor;
    public NavMeshAgent player;
    public LineRenderer line;

    NavMeshPath path;
    int i = 0;
    float j = 0;

	// Use this for initialization
	void Start () {
        path = new NavMeshPath();
      // Cursor.SetCursor(cursor[0], Vector2.zero, CursorMode.Auto);
	}
	
	// Update is called once per frame
	void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 point  = ray.origin + (ray.direction * 50f);
        //Debug.Log("Camera Pos : " + transform.position + "World point : " + point);

        var heading = point - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;

        RaycastHit rhit;
        Debug.DrawRay(transform.position, direction, Color.yellow);

        if (Physics.Raycast(transform.position, direction, out rhit, distance))
            point = rhit.point;

            NavMeshHit hit;

        if (NavMesh.SamplePosition(point, out hit, 50f, NavMesh.AllAreas))
        {
            Debug.DrawRay(hit.position, Vector3.up, Color.red, 3.0f);
            if (Input.GetMouseButtonDown(0))
            {
                player.destination = hit.position;

            }
            
            NavMesh.CalculatePath(player.gameObject.transform.position, hit.position, NavMesh.AllAreas, path);

            line.positionCount = path.corners.Length;
            line.SetPositions(path.corners);
        }
    }
}
