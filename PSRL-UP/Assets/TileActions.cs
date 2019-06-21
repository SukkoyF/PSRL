using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileActions : MonoBehaviour
{
    MapManager _MM;
    GameManager _GM;

    public bool attacking;

    public bool aiIsPlaying;

    private void Awake()
    {
        _MM = FindObjectOfType<MapManager>();
        _GM = FindObjectOfType<GameManager>();
        aiIsPlaying = false;
    }

    public void TileClicked(Transform source,int x,int y)
    {
        if(aiIsPlaying == true)
        {
            return;
        }

        Transform character = GameManager.selected;

        if(character == null)
        {
            if(_MM.GetNode(x,y).entity != null)
            {
                if(_MM.GetNode(x,y).entity.GetComponent<Entity>().myOwner == Owner.Player)
                {
                    _MM.UnlitTiles();

                    GameManager.Select(_MM.GetNode(x, y).entity.transform);

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
                    data.Move(_MM.GetNode(x, y),false);

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
        Attack currAttack = GameManager.selected.GetComponent<Attack>();

        List<Node> inRange = currAttack.GetTargetables();
        Node clicked = _MM.GetNode(x, y);

        if(inRange.Contains(clicked))
        {
            if(currAttack.allowAttackOnEmpty == false)
            {
                if (clicked.entity != null)
                {
                    DisableAttacking();
                   currAttack.Strike(clicked);
                }
            }
            else
            {
                DisableAttacking();
                currAttack.Strike(clicked);
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
