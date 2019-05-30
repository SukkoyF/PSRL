using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileActions : MonoBehaviour
{
    public Transform character;
    MapManager _MM;
    GameManager _GM;

    private void Awake()
    {
        _MM = FindObjectOfType<MapManager>();
        _GM = FindObjectOfType<GameManager>();
    }

    public void MoveCharacter(Transform source,int x,int y)
    {
        if(character == null)
        {
            if(_MM.GetNode(x,y).entity != null)
            {
                character = _MM.GetNode(x, y).entity.transform;
            }
        }
        else
        {
            Debug.Log("Looking At Neighbours");

            List<Node> neighbours = _MM.GetNeighbours(character.GetComponent<Entity>().curr_Node);

            foreach(Node n in neighbours)
            {
                Debug.Log(n.xPos + "     " + n.yPos);
            }

            if(neighbours.Contains(_MM.GetNode(x,y)))
            {
                Debug.Log("Clicked a Neighbour");
                character.position = source.position;
                character.GetComponent<Entity>().curr_Node = _MM.GetNode(x, y);
            }
        }
    }
}
