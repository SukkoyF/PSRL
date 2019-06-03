using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileActions : MonoBehaviour
{
    MapManager _MM;
    GameManager _GM;

    public bool attacking;

    private void Awake()
    {
        _MM = FindObjectOfType<MapManager>();
        _GM = FindObjectOfType<GameManager>();
    }

    public void TileClicked(Transform source,int x,int y)
    {
        Transform character = GameManager.selected;

        if(character == null)
        {
            if(_MM.GetNode(x,y).entity != null)
            {
                GameManager.Select(_MM.GetNode(x, y).entity.transform);
                
                if(GameManager.selected.GetComponent<Entity>().curr_MP > 0)
                {
                    List<Node> neighbours = _MM.GetNeighbours(GameManager.selected.GetComponent<Entity>().curr_Node);

                    foreach(Node n in neighbours)
                    {
                        if(n.CanBeWalkedOn() == true)
                        {
                            n.LightUp();
                        }
                    }
                }
            }
        }
        else
        {
            if(attacking == true)
            {
                AttemptAttack(x,y);
            }
            else
            {
                AttemptMove(source, character, x, y);
            }      
        }
    }

    void AttemptMove(Transform clickSource,Transform character,int x,int y)
    {
        Entity data = character.GetComponent<Entity>();

        if (data.curr_MP > 0)
        {
            List<Node> neighbours = _MM.GetNeighbours(character.GetComponent<Entity>().curr_Node);

            if (neighbours.Contains(_MM.GetNode(x, y)))
            {
                if (_MM.GetNode(x, y).walkAble == true && _MM.GetNode(x, y).entity == null)
                {
                    character.GetComponent<Entity>().curr_Node.entity = null;
                    character.position = clickSource.position;
                    character.GetComponent<Entity>().curr_Node = _MM.GetNode(x, y);
                    _MM.GetNode(x, y).entity = character.gameObject;
                    data.curr_MP--;

                    _MM.UnlitTiles();

                    if (GameManager.selected.GetComponent<Entity>().curr_MP > 0)
                    {
                        List<Node> temp = _MM.GetNeighbours(GameManager.selected.GetComponent<Entity>().curr_Node);

                        foreach (Node n in temp)
                        {
                            if (n.CanBeWalkedOn() == true)
                            {
                                n.LightUp();
                            }

                        }
                    }
                }
            }

        }
    }

    void AttemptAttack(int x,int y)
    {
        List<Node> inRange = GameManager.selected.GetComponent<Attack>().GetTargetables();
        Node clicked = _MM.GetNode(x, y);

        if(inRange.Contains(clicked))
        {
            if(clicked.entity != null)
            {
                DisableAttacking();
                GameManager.selected.GetComponent<Attack>().Strike(clicked);
            }
        }
    }

    public void SetAttacking()
    {
        if(GameManager.selected.GetComponent<Entity>().attacked == false)
        {
            attacking = true;

            _MM.UnlitTiles();

            List<Node> inRange = GameManager.selected.GetComponent<Attack>().GetTargetables();

            foreach (Node n in inRange)
            {
                if (n.tile != null)
                {
                    n.LightUp();
                }
            }
        }
    }

    public void DisableAttacking()
    {
        attacking = false;

        _MM.UnlitTiles();

        if (GameManager.selected.GetComponent<Entity>().curr_MP > 0)
        {
            List<Node> neighbours = _MM.GetNeighbours(GameManager.selected.GetComponent<Entity>().curr_Node);

            foreach (Node n in neighbours)
            {
                if (n.CanBeWalkedOn() == true)
                {
                    n.LightUp();
                }
            }
        }
    }
}
