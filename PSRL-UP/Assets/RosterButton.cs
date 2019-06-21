using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RosterButton : MonoBehaviour
{
    public GameObject myCharacter;

    public void DeleteFromRoster()
    {
        Unlocks.instance.RemoveFromTeam(myCharacter);
        Destroy(gameObject);
    }
}
