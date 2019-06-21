using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineDrop : Attack
{
    public GameObject mineIcon;

    public override List<Node> GetTargetablesAt(Node n)
    {
        List<Node> inRange = _MM.GetInRange(n, attackRange, AimingMode.line);

        List<Node> final = new List<Node>();

        foreach(Node no in inRange)
        {
            if(no.CanBeWalkedOn())
            {
                final.Add(no);
            }
        }

        return final;
    }

    public override void Strike(Node target)
    {
        if(target.CanBeWalkedOn())
        {
            GameObject instance = Instantiate(mineIcon, target.tile.transform.position, Quaternion.identity);
            target.effects.Add(new TE_Mine(3,instance,target));         
            _E.attacked = true;
        }
    }
}
