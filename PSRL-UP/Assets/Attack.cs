using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackRange;

    Entity _E;
    MapManager _MM;

    private void Awake()
    {
        _E = GetComponent<Entity>();
        _MM = FindObjectOfType<MapManager>();
    }

    public List<Node> GetTargetables()
    {
        List<Node> inRange = _MM.GetInRange(_E.curr_Node, attackRange);

        return inRange;
    }

    public void Strike(Node target)
    {
        target.entity.GetComponent<Entity>().Damage(1);
    }
}
