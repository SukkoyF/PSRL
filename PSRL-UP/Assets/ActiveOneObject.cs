using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveOneObject : MonoBehaviour
{
    public GameObject[] objects;

    private void Awake()
    {
        objects[Random.Range(0, objects.Length)].SetActive(true);
    }
}
