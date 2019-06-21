using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PopColor { red,green}

public class PopUpTextManager : MonoBehaviour
{
    public static void SpawnPopUpText(string text,Vector3 position,PopColor toSet)
    {
        GameObject popUp = Resources.Load("PopUpText") as GameObject;

        GameObject instance = Instantiate(popUp, new Vector3(position.x,position.y +.5f,position.z), Quaternion.identity);

        instance.GetComponentInChildren<Text>().text = text;

        if(toSet == PopColor.red)
        {
            instance.GetComponentInChildren<Text>().color = new Color32(0xAA, 0x44, 0x65, 0xFF);
        }
        else if(toSet == PopColor.green)
        {
            instance.GetComponentInChildren<Text>().color = new Color32(0x96, 0xE0, 0x72, 0xFF);
        }
        
    }
}
