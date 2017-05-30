using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Spell : MonoBehaviour
{
    private GameObject _spell;

    private void Update()
    {
        if (Input.GetButtonDown("Spell_A"))
        {
            GM_Manager.instance.spell.transform.GetChild(PlayerPrefs.GetInt("CurrentSelectHeros")).GetChild(0).SendMessage("OnClickEnable");
        }

        if (Input.GetButtonUp("Spell_A"))
        {
            GM_Manager.instance.spell.transform.GetChild(PlayerPrefs.GetInt("CurrentSelectHeros")).GetChild(0).SendMessage("OnClickDisable");
        }
    }
}
