using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Selected : MonoBehaviour
{
    public GameObject panel;
    public Image portrait;

    public Text mpDisplay;
    public Text hpDisplay;

    public GameObject attackShutter;
    public Image attackLine;
    public Color white;
    public Color green;

    private void Awake()
    {
        panel.SetActive(false);    
    }

    private void LateUpdate()
    {
        if(panel.activeInHierarchy && GameManager.selected != null)
        {
            mpDisplay.text = "mp : " + GameManager.selected.GetComponent<Entity>().curr_MP + " / " + GameManager.selected.GetComponent<Entity>().maxMP;
            hpDisplay.text = "hp : " + GameManager.selected.GetComponent<Entity>().health;

            if(GameManager.selected.GetComponent<Entity>().attacked == true)
            {
                attackShutter.SetActive(true);
                attackLine.color = green;
            }
            else
            {
                attackShutter.SetActive(false);
                attackLine.color = white;
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
