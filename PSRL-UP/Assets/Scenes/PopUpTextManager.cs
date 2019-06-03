using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpTextManager : MonoBehaviour
{
    public static void SpawnPopUpText(string text,Vector3 position)
    {
        GameObject popUp = Resources.Load("PopUpText") as GameObject;

        GameObject instance = Instantiate(popUp, new Vector3(position.x,position.y +.5f,position.z), Quaternion.identity);

        instance.GetComponentInChildren<Text>().text = text;
    }
}
