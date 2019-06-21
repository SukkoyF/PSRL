using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashManager : MonoBehaviour
{
    public string toLoad;
    public float splashDelay;

    Level_Changer _LC;
         
    private void Start()
    {
        _LC = FindObjectOfType<Level_Changer>();
        Invoke("Call", splashDelay);
    }

    void Call()
    {
        _LC.LoadLevel(toLoad);
    }
}
