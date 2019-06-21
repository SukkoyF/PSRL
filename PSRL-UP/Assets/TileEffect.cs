using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEffect
{
    public virtual void Activate(Entity walkedOn)
    {

    }
}

public class TE_Mine : TileEffect
{
    public GameObject mySprite;
    public Node myNode;
    int damage;

    public TE_Mine(int _damage,GameObject _mySprite,Node _myNode)
    {
        myNode = _myNode;
        mySprite = _mySprite;
        damage = _damage;
    }

    public override void Activate(Entity walkedOn)
    {
        walkedOn.Damage(damage);
        Object.Destroy(mySprite);
        myNode.effects.Remove(this);
    }
}
