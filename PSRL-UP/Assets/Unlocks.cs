using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlocks : MonoBehaviour
{
    static public bool[] unlocks;
    static bool unlocksInitialized = false;
    static public Unlocks instance;

    public GameObject[] characters;
    public List<GameObject> currTeam;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != gameObject)
        {
            Destroy(gameObject);
        }

        if(unlocksInitialized == false)
        {
            unlocks = new bool[15];
            unlocksInitialized = true;

            unlocks[0] = true;
        }
    }

    public void AddToTeam(int toAdd)
    {
        currTeam.Add(characters[toAdd]);
        FindObjectOfType<RosterDisplayManager>().AddDisplay(characters[toAdd]);
    }

    public void RemoveFromTeam(GameObject toRemove)
    {
        for(int i =0;i < currTeam.Count;i++)
        {
            if(currTeam[i] == toRemove)
            {
                currTeam.RemoveAt(i);
                i += currTeam.Count;
            }
        }
    }
    
}
