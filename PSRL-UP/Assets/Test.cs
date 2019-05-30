using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public void Enter()
    {
        Debug.Log("Pointer Entered");
    }

    public void Exit()
    {
        Debug.Log("Pointed Exited");
    }

    public void Clicked()
    {
        Debug.Log("Clicked");
    }
}
