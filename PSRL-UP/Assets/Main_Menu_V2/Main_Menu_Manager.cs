using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Main_Menu_Manager : MonoBehaviour
{
    public void ChangeScene(string toLoad)
    {
        FindObjectOfType<Level_Changer>().LoadLevel(toLoad);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ToggleObject(GameObject toSwitch)
    {
        toSwitch.SetActive(!toSwitch.activeInHierarchy);
    }

    public void ToggleMusic(Text toEdit)
    {
        if(FindObjectOfType<MusicManager>().Toggle() == true)
        {
            toEdit.text = "Music : On";
        }
        else
        {
            toEdit.text = "Music : Off";
        }
    }

    public void ToggleSFX(Text toEdit)
    {
        if(FindObjectOfType<SFXManager>().Toggle() == true)
        {
            toEdit.text = "Sfx : On";
        }
        else
        {
            toEdit.text = "sfx : Off";
        }
    }

    public void LoadLevel(string toLoad)
    {
        FindObjectOfType<Level_Changer>().LoadLevel(toLoad);
    }
}
