using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveOnAwake : MonoBehaviour
{
    public GameObject[] toDeactivate;

    private void OnEnable()
    {
        foreach(GameObject g in toDeactivate)
        {
            if(g.activeInHierarchy == true)
            {
                g.SetActive(false);
            }
        }
    }
}
