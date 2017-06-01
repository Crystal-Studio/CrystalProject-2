using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_FireRay : MonoBehaviour
{
    public float timeActivate;
    public float timeDesactivate;

    private bool _isActivate;

    private void Start()
    {
        StartCoroutine(FireRay());
    }

    IEnumerator FireRay()
    {
        while (true)
        {
            foreach (Transform item in transform)
                item.gameObject.SetActive(true);
            yield return new WaitForSeconds(timeActivate);

            foreach (Transform item in transform)
                item.gameObject.SetActive(false);
            Debug.Log("b");

            yield return new WaitForSeconds(timeDesactivate);
            Debug.Log("c");
        }
      
        yield return new WaitForSeconds(timeDesactivate);

        Debug.Log("d");
    }

    

}
