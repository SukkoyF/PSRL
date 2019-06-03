using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Node curr_Node;

    public int health;

    public int maxMP;
    public int curr_MP;

    void Awake()
    {
        curr_MP = maxMP;
    }

    public void Damage(int damage)
    {
        health -= damage;

        if(health <=0)
        {
            Destroy(gameObject);
        }
    }
}
