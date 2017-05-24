using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Transparency : MonoBehaviour
{
    private Renderer rend;
    private Material currentMat;
    private Material changeMat;

    private GameObject player;
    
    public Material mat;
    public int indexMat;

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rend = GetComponent<Renderer>();
        currentMat = rend.sharedMaterials[indexMat];


	}

    private void Update()
    {
        RaycastHit[] hits;

        hits = Physics.RaycastAll(Camera.main.transform.position, GetDirection(), Mathf.Infinity);

        if (System.Array.Exists(hits, element => element.transform.gameObject == gameObject))
        {
            changeMat = mat;
        }
        else
        {
            changeMat = currentMat;
        }

        ChangeMaterial();
    }


    Vector3 GetDirection()
    {
        var heading = player.transform.position - Camera.main.transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;

        return direction;
    }

    public void ChangeMaterial()
    {
        
        Material[] mats = rend.materials;

        mats[indexMat] = changeMat;

        rend.materials = mats;

    }
}
