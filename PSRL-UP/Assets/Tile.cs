using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : EventTrigger
{
    public int x;
    public int y;

    public void LightUp()
    {
        GetComponentsInChildren<SpriteRenderer>()[1].enabled = true;
    }

    public void LightOff()
    {
        GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;
    }

    public void SetGridPos(int _x,int _y)
    {
        x = _x;
        y = _y;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
       // FindObjectOfType<TileActions>().TileClicked(transform,x,y);
    }

    public void OnMouseDown()
    {
        if(GameManager.gamePaused != true)
        {
            FindObjectOfType<TileActions>().TileClicked(transform, x, y);
        }
       
    }
}
