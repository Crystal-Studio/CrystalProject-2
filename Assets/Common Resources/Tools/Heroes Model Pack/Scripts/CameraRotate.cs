using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour {

	public float rotateSpeed = 1;
	
	bool hasRotated = false;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(!Input.GetMouseButton (0) && !hasRotated)
			transform.RotateAround(transform.position, rotateSpeed * Time.deltaTime);
		
		if (Input.GetMouseButton(0)) {
         transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Mouse X")*rotateSpeed*-20);
			if(!hasRotated)
				hasRotated = true;
	}
	}
}
