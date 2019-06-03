using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Selected : MonoBehaviour
{
    public GameObject panel;
    public Image portrait;

    public Text mpDisplay;

    private void Awake()
    {
        panel.SetActive(false);    
    }

    private void LateUpdate()
    {
        if(panel.activeInHierarchy && GameManager.selected != null)
        {
            mpDisplay.text = "MP : " + GameManager.selected.GetComponent<Entity>().curr_MP + " / " + GameManager.selected.GetComponent<Entity>().maxMP;
        }
    }

    public void Open()
    {
        portrait.sprite = GameManager.selected.GetComponent<SpriteRenderer>().sprite;

        panel.SetActive(true);
    }

    public void Close()
    {
        panel.SetActive(false);
    }
}
