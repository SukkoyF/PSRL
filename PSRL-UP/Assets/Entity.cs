using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Owner { Player,Ai,Neutral}
public class Entity : MonoBehaviour
{
    public int ID;

    public Node curr_Node;

    public int health;

    public int maxMP;
    public int curr_MP;

    public Owner myOwner;

    public bool attacked;

    void Awake()
    {
        ResetTurn();
    }

    public void Damage(int damage)
    {
        health -= damage;

        PopUpTextManager.SpawnPopUpText(damage.ToString(), transform.position,PopColor.red);

        if(health <=0)
        {
            Die();
        }
    }

    void Die()
    {
        Unlocks.unlocks[ID] = true;
        FindObjectOfType<GameManager>().EntityDied();
        Destroy(gameObject);
    }

    public void Move(Node target, bool pushed)
    {
        if (target.CanBeWalkedOn() == true)
        {
            curr_Node.entity = null;
            transform.position = target.tile.transform.position;
            curr_Node = target;
            target.entity = gameObject;

            for(int i = curr_Node.effects.Count -1;i>=0;i--)
            {
                curr_Node.effects[i].Activate(this);
            }

            if (pushed == false)
            {
                curr_MP--;
            }
        }
    }

    public void Heal(int heal)
    {
        health += heal;

        PopUpTextManager.SpawnPopUpText(heal.ToString(), transform.position, PopColor.green);
    }

    public void ResetTurn()
    {
        curr_MP = maxMP;
        attacked = false;
    }
}
