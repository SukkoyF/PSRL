using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFitter : MonoBehaviour
{
    Text myText;
    RectTransform rt;

    private void Awake()
    {
        myText = GetComponentInChildren<Text>();
        rt = GetComponent<RectTransform>();
        rt.sizeDelta = myText.rectTransform.sizeDelta;
        rt.sizeDelta = new Vector2(rt.sizeDelta.x + 24, rt.sizeDelta.y + 24);
    }

    private void Update()
    {
        rt.sizeDelta = myText.rectTransform.sizeDelta;
        rt.sizeDelta = new Vector2(rt.sizeDelta.x + 24, rt.sizeDelta.y + 24);
    }
}
