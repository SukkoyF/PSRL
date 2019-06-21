using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackRange;
    public bool allowAttackOnEmpty;

    protected Entity _E;
    protected MapManager _MM;

    private void Awake()
    {
        _E = GetComponent<Entity>();
        _MM = FindObjectOfType<MapManager>();
    }

    public List<Node> GetTargetables()
    {
        return GetTargetablesAt(_E.curr_Node);
    }

    public virtual void Strike(Node target)
    {
  
    }

    public virtual List<Node> GetTargetablesAt(Node n)
    {
        return new List<Node>();
    }
}
