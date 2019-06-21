using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeechLife : Attack
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
        _E.Heal(1);
    }
}
