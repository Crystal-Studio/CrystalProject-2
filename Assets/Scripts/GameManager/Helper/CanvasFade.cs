using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFade : MonoBehaviour
{
    bool _isActive;
    int _fade;

    public float speed;
    public float bonusSpeed = 1;

    public float timeShowText;

    private void Update()
    {
        if (_isActive)
        {
            if (_fade == 1)
            {
                GetComponent<CanvasGroup>().alpha += speed / 10 * bonusSpeed * Time.deltaTime;

                if (GetComponent<CanvasGroup>().alpha >= 1)
                    StartCoroutine(ShowText());
            }
            else
            {
                GetComponent<CanvasGroup>().alpha -= speed / 10 * Time.deltaTime;

                if (GetComponent<CanvasGroup>().alpha <= 0)
                    _isActive = false;
            }
        }
    }

    public IEnumerator OnDisplayText()
    {
        yield return new WaitForSeconds(1.75f);
        GetComponent<CanvasGroup>().alpha = 0;
        _fade = 1;
        _isActive = true;
    }

    IEnumerator ShowText()
    {
        yield return new WaitForSeconds(timeShowText);
        _fade = -1;
    }
}
