using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeFade : MonoBehaviour
{
    public Image _Image;
    public Text _Text;

    public float showUpTime;
    public float fadingTime;

    bool animating = false;

    public void Fade()
    {
        if(animating == false)
        {
            StartCoroutine(FadeAnimation());
            animating = true;
        }
    }

    IEnumerator FadeAnimation()
    {
        float t = 0;

        while(t < 1)
        {
            yield return new WaitForEndOfFrame();

            t += Time.deltaTime/fadingTime;

            SetColors(t);
        }

        yield return new WaitForSeconds(showUpTime);

        while (t > 0)
        {
            yield return new WaitForEndOfFrame();

            t -= Time.deltaTime/fadingTime;

            SetColors(t);
        }

        animating = false;
    }

    void SetColors(float t)
    {
        if(_Image != null)
        {
            _Image.color = new Color(_Image.color.r, _Image.color.g, _Image.color.b, t);
        }

        if(_Text != null)
        {
            _Text.color = new Color(_Text.color.r, _Text.color.g, _Text.color.b, t);
        }
    }
}
