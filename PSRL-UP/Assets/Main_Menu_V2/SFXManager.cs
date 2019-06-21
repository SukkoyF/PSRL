using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    bool sfxOn = true;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySFX()
    {
        if(sfxOn == true)
        {

        }
    }

    public bool Toggle()
    {
       sfxOn = !sfxOn;

        return sfxOn;
    }
}
