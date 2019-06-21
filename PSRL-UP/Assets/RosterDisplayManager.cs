using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RosterDisplayManager : MonoBehaviour
{
    public GameObject rosterParent;
    public GameObject rosterDisplay;

    public void AddDisplay(GameObject character)
    {
        GameObject instance = Instantiate(rosterDisplay, rosterParent.transform);
        instance.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = character.GetComponentInChildren<SpriteRenderer>().sprite;
        instance.GetComponent<RosterButton>().myCharacter = character;
    }
}
