using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public List<GameObject> enemyRoster;

    MapManager _MM;
    PathFinding _PF;

    private void Start()
    {
        _PF = FindObjectOfType<PathFinding>();
        _MM = FindObjectOfType<MapManager>();

        Invoke("SpawnRoster", 1f);
    }

    void SpawnRoster()
    {
        _MM.SpawnCreatures(enemyRoster,Owner.Ai);
    }

    public bool CheckIfAnyLeft()
    {
        List<Entity> myEntities = new List<Entity>();

        foreach (Entity e in FindObjectsOfType<Entity>())
        {
            if (e.myOwner == Owner.Ai)
            {
                myEntities.Add(e);
            }
        }

        if(myEntities.Count >0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void StartAiTurn()
    {
        List<Entity> myEntities = new List<Entity>();

        foreach(Entity e in FindObjectsOfType<Entity>())
        {
            if(e.myOwner == Owner.Ai)
            {
                myEntities.Add(e);
            }
        }

        StartCoroutine(PlayEachCharacter(myEntities));

    }

    IEnumerator PlayEachCharacter(List<Entity> toPlay)
    {
        yield return new WaitForSeconds(1);

        foreach (Entity e in toPlay)
        {
            if(e)
            {
                if (CanYouAttackWithoutMoving(e) == false)
                {
                    List<Node> nodesToAttack = new List<Node>();

                    foreach (Entity n in FindObjectsOfType<Entity>())
                    {
                        if (n.myOwner == Owner.Player)
                        {
                            nodesToAttack.Add(n.curr_Node);
                        }
                    }

                    int distance = 2000;
                    Node target = null;

                    foreach (Node n in nodesToAttack)
                    {
                        foreach (Node nx in _MM.GetNeighbours(n))
                        {
                            if (nx != null && nx.CanBeWalkedOn())
                            {
                                int i = _PF.GetWalkingDistance(e.curr_Node, nx);

                                if (i < distance && i > 0)
                                {
                                    distance = i;
                                    target = nx;
                                }
                            }
                        }
                    }

                    if (target != null)
                    {
                        List<Node> path = _PF.GetPath(e.curr_Node, target);

                        if (path.Count > 0)
                        {
                            while (e.curr_MP > 0 && path.Count > 0)
                            {
                                if(e != null)
                                {
                                    if (path[0] == e.curr_Node)
                                    {
                                        path.RemoveAt(0);
                                    }
                                    else
                                    {
                                        e.Move(path[0], false);
                                    }

                                    yield return new WaitForSeconds(1);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }

                }
            }    

            yield return new WaitForSeconds(1);
        }

       
        FindObjectOfType<GameManager>().EndAiTurn();
    }

    bool CanYouAttackWithoutMoving(Entity e)
    {
        Attack e_attack = e.GetComponent<Attack>();

        foreach (Node n in e_attack.GetTargetables())
        {
            if (n.entity != null && n.entity.GetComponent<Entity>().myOwner == Owner.Player)
            {
                e_attack.Strike(n);
                return true;
            }
        }

        return false;
    }
}
