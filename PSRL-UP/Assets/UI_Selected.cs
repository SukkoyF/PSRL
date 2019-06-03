using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Selected : MonoBehaviour
{
    public GameObject panel;
    public Image portrait;

    public Text mpDisplay;

    public GameObject attackShutter;

    private void Awake()
    {
        panel.SetActive(false);    
    }

    private void LateUpdate()
    {
        if(panel.activeInHierarchy && GameManager.selected != null)
        {
            mpDisplay.text = "MP : " + GameManager.selected.GetComponent<Entity>().curr_MP + " / " + GameManager.selected.GetComponent<Entity>().maxMP;

            if(GameManager.selected.GetComponent<Entity>().attacked == true)
            {
                attackShutter.SetActive(true);
            }
            else
            {
                attackShutter.SetActive(false);
            }
        }
    }

    public void Open()
    {
        portrait.sprite = GameManager.selected.GetComponentInChildren<SpriteRenderer>().sprite;

        panel.SetActive(true);
    }

    public void Close()
    {
        panel.SetActive(false);
    }
}
