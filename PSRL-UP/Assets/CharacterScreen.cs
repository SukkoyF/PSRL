using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScreen : MonoBehaviour
{
    public CodeFade warning;

    private void Start()
    {
        Unlocks.instance.currTeam = new List<GameObject>();
    }

    public void TryToStartGame()
    {
        if(FindObjectOfType<Unlocks>().currTeam.Count > 0)
        {
            FindObjectOfType<Level_Changer>().LoadLevel("Game");
        }
        else
        {
            warning.Fade();
        }
    }
}
