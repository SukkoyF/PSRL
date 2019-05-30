using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : EventTrigger
{
    public int x;
    public int y;

    public void SetGridPos(int _x,int _y)
    {
        x = _x;
        y = _y;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        FindObjectOfType<TileActions>().MoveCharacter(transform,x,y);
    }
}
