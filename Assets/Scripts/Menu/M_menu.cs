using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M_menu : MonoBehaviour
{

    public void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
    }
}
