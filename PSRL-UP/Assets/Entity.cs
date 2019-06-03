using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Node curr_Node;

    public int health;

    public int maxMP;
    public int curr_MP;

    public bool attacked;

    void Awake()
    {
        ResetTurn();
    }

    public void Damage(int damage)
    {
        health -= damage;

        if(health <=0)
        {
            Destroy(gameObject);
        }
    }

    public void ResetTurn()
    {
        curr_MP = maxMP;
        attacked = false;
    }
}
