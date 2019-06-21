using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{
    public int index;

    private void Start()
    {
        transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Unlocks.instance.characters[index].GetComponentInChildren<SpriteRenderer>().sprite;
        if (Unlocks.unlocks[index] == false)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    public void AddToTeam()
    {
        FindObjectOfType<Unlocks>().AddToTeam(index);
    }
}
