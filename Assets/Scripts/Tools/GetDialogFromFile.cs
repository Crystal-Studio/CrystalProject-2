using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public struct DialogFile
{
    [Serializable]
    public struct DialogInfo
    {
        public string key;
        public string[] fr;
        public string[] en;
    }

    public DialogInfo[] dialog;
}

public class GetDialogFromFile : MonoBehaviour
{
    public TextAsset value;
    [SerializeField]
    public int index;
    public DialogFile data = new DialogFile();


    private void Update()
    {
   //     Debug.Log(data.dialog[index].fr[0]);
    }

    public string[] GetDialog(int i)
    {
        if (i == 0)
            return data.dialog[index].fr;
        else
            return data.dialog[index].en;
    }
}

