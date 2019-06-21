using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    MapManager _MM;

    private void Start()
    {
        _MM = FindObjectOfType<MapManager>();

        Invoke("SpawnRoster", 1.1f);
    }

    void SpawnRoster()
    {
        _MM.SpawnCreatures(FindObjectOfType<Unlocks>().currTeam,Owner.Player);
    }
}
