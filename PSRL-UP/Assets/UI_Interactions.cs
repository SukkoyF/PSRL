using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Interactions : MonoBehaviour
{
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
}
