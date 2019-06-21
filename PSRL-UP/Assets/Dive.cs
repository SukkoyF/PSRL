using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dive : Attack
{
    public override List<Node> GetTargetablesAt(Node n)
    {
        List<Node> inRange = _MM.GetInRange(n, attackRange, AimingMode.line);

        return inRange;
    }

    public override void Strike(Node target)
    {
        _E.attacked = true;
        target.entity.GetComponent<Entity>().Damage(1);

        Direction d = _MM.GetDirection(_E.curr_Node, target);

        Node sendingTo = _MM.GetNode(target, d, 1);

        if(sendingTo != null)
        {
            target.entity.GetComponent<Entity>().Move(sendingTo,true);
        }
    }
}
