using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drip : Attack
{
    public override List<Node> GetTargetablesAt(Node n)
    {
        List<Node> inRange = _MM.GetInRange(n, attackRange, AimingMode.notSpecified);

        return inRange;
    }

    public override void Strike(Node target)
    {
        _E.attacked = true;

        List<Node> touched = FindObjectOfType<MapManager>().GetNeighbours(_E.curr_Node);

        foreach(Node n in touched)
        {
            if(n.entity != null)
            {
                n.entity.GetComponent<Entity>().Damage(1);
            }
        }
    }
}
