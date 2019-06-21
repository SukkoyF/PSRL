using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Interactions : MonoBehaviour
{
    public Text turnBanner;
    public Animator banner_Anim;

    public void EndTurn()
    {
        FindObjectOfType<GameManager>().EndTurn();
    }

    public void ToggleAttacking()
    {
        if(FindObjectOfType<TileActions>().attacking == false)
        {
            FindObjectOfType<TileActions>().SetAttacking();
        }
    }

    public void ShowTurnBanner(string toDisplay)
    {
        turnBanner.text = toDisplay;

        banner_Anim.SetTrigger("ShowTurn");
    }
}
