using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public bool Toggle()
    {
        GetComponent<AudioSource>().enabled = !GetComponent<AudioSource>().enabled;

        return GetComponent<AudioSource>().enabled;
    }
}
