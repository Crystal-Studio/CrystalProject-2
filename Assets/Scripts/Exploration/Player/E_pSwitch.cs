using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_pSwitch : MonoBehaviour
{
    public GameObject fx;

    private E_pManager s_pManger;

	// Use this for initialization
	void Start () {
        s_pManger = GetComponent<E_pManager>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeHero(int i)
    {
        if (i == PlayerPrefs.GetInt("CurrentSelectHeros"))
            return;
        PlayerPrefs.SetInt("CurrentSelectHeros", i);
        s_pManger.SetAnimator(transform.GetChild(PlayerPrefs.GetInt("CurrentSelectHeros")).GetComponent<Animator>());

        foreach (Transform item in transform)
        {
            item.gameObject.SetActive(false);
        }

        transform.GetChild(i).gameObject.SetActive(true);
        Instantiate(fx, new Vector3(transform.position.x, 2, transform.position.z), Quaternion.identity);
    }

}
