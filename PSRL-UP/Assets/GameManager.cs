using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Transform selected;

    UI_Selected _US;
    MapManager _MM;

    private void Awake()
    {
        _US = FindObjectOfType<UI_Selected>();
        _MM = FindObjectOfType<MapManager>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if(FindObjectOfType<TileActions>().attacking == true)
            {
                FindObjectOfType<TileActions>().DisableAttacking();
            }
            else
            {
                selected = null;
                _MM.UnlitTiles();
                _US.Close();
            }
        }
    }

    public static void Select(Transform toSet)
    {
        selected = toSet;
        FindObjectOfType<UI_Selected>().Open();
    }

    public void EndTurn()
    {
        Entity[] entities = FindObjectsOfType<Entity>();

        foreach(Entity e in entities)
        {
            e.ResetTurn();
        }
        _MM.UnlitTiles();
        selected = null;
        _US.Close();
    }
}
