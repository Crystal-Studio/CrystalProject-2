using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM_Fade : MonoBehaviour
{
    public float fadeSpeed;
    public Image fadeImage;

    private float _value;
    private bool _fade;
    private bool _next;

    private Color c;

    private void Update()
    {
        if (_fade)
        {
            fadeImage.color = new Color(c.r, c.g, c.b, c.a += _value);
            if (c.a < 0 || c.a > 1)
            {
                _fade = false;
                _next = true;
            }
        }
    }

    public IEnumerator FadeIN()
    {
        _fade = true;
        c = new Color(0,0,0,0);
        fadeImage.color = c;
        _next = false;
        _value = fadeSpeed;

        while (_next == false)
            yield return null;
        yield return new WaitForSeconds(0.15f);
    }

    public IEnumerator FadeOUT()
    {
        Debug.Log("lfreswlwfvcwjdsxjliclwjksd");
        _fade = true;
        c = Color.black;
        fadeImage.color = c;
        _next = false;
        _value = fadeSpeed * -1;

        while (_next == false)
            yield return null;
        yield return new WaitForSeconds(0.15f);
    }
}
