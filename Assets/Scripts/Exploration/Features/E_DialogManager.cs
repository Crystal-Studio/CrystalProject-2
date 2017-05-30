using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class E_DialogManager : MonoBehaviour
{
    public Text dialogText;
    public GameObject DialogBox;

    public GameObject previous;
    public GameObject next;
    public GameObject ok;

    private List<string> _dialogs = new List<string>();
    private int _id;
	
	public void OnStart (List<string> s)
    {
        DialogBox.SetActive(true);
        _dialogs.AddRange(s);

        UpdateButton();

        dialogText.text = _dialogs[0];
    }
	

	public void OnEnd ()
    {
        _dialogs.Clear();
        DialogBox.SetActive(false);
	}

    public void SetIndex(int i)
    {
        _id += i;

        if (_id < 0)
            _id = 0;
        if (_id > _dialogs.Count -1)
            _id = _dialogs.Count -1;

        UpdateButton();

        dialogText.text = _dialogs[_id];
    }

    void UpdateButton()
    {
        if (_id == 0)
            previous.SetActive(false);
        else
            previous.SetActive(true);

        if (_id < _dialogs.Count -1)
        {
            next.SetActive(true);
            ok.SetActive(false);
        }
        else
        {
            next.SetActive(false);
            ok.SetActive(true);
        }
    }
}
