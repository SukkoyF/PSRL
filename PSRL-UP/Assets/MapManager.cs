using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AimingMode { notSpecified,line,diagonal}

public class MapManager : MonoBehaviour
{
    public GameObject tile;

    public List<GameObject> toSpawn;

    public Node[,] tiles;
    public int width;
    public int height;

    private void Awake()
    {
        tiles = new Node[width, height];

        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                if (Random.value > .3f)
                {
                    GameObject instance = Instantiate(tile, new Vector2((w * .5f) + h * -0.5f, (w * .25f) + h * .25f), Quaternion.identity);
                    tiles[w, h] = new Node(w, h, true, instance);
                    instance.GetComponent<Tile>().SetGridPos(w, h);
                }
                else
                {
                    tiles[w, h] = new Node(w, h, false, null);
                }
            }
        }

        foreach (GameObject g in toSpawn)
        {
            Node chosen = null;

            while (chosen == null)
            {
                int x = Random.Range(0, width);
                int y = Random.Range(0, height);
                chosen = tiles[x, y];

                if(chosen != null && chosen.entity == null && chosen.walkAble == true)
                {

                }
                else
                {
                    chosen = null;
                }
            }

            GameObject instanceTwo = Instantiate(g, chosen.tile.transform.position, Quaternion.identity);
            chosen.entity = instanceTwo;
            instanceTwo.GetComponent<Entity>().curr_Node = chosen;
        }
    }

    public void UnlitTiles()
    {
        for(int x =0;x < width;x++)
        {
            for(int y = 0;y<height;y++)
            {
                if(tiles[x,y].tile != null)
                {
                    tiles[x, y].tile.GetComponent<Tile>().LightOff();
                }
              
            }
        }
    }

    public List<Node> GetNeighbours(Node startPoint)
    {
        List<Node> adjacent = new List<Node>();

        if (startPoint.xPos < width - 1)
        {
            adjacent.Add(tiles[startPoint.xPos + 1, startPoint.yPos]);
        }

        if (startPoint.xPos > 0)
        {
            adjacent.Add(tiles[startPoint.xPos - 1, startPoint.yPos]);
        }

        if (startPoint.yPos > 0)
        {
            adjacent.Add(tiles[startPoint.xPos, startPoint.yPos - 1]);
        }

        if (startPoint.yPos < height - 1)
        {
            adjacent.Add(tiles[startPoint.xPos, startPoint.yPos + 1]);
        }

        return adjacent;
    }

    public Node GetNode(int x, int y)
    {
        return tiles[x, y];
    }

    public List<Node> GetInRange(Node startPoint,int distance,AimingMode am)
    {
        List<Node> toReturn = new List<Node>();

        for(int x = 0;x< width;x++)
        {
            for(int y =0;y <height;y++)
            {
                if(GetDistance(startPoint,tiles[x,y]) <= distance)
                {
                    toReturn.Add(tiles[x, y]);
                }
            }
        }

        if(am == AimingMode.line)
        {
            for(int i =0;i < toReturn.Count;i++)
            {
                if(toReturn[i].xPos != startPoint.xPos && toReturn[i].yPos != startPoint.yPos)
                {
                    toReturn.RemoveAt(i);
                    i--;
                }
            }
        }

        return toReturn;
    }

    public int GetDistance(Node nOne,Node nTwo)
    {
    
        int toReturn = 0;

        toReturn += Mathf.Abs(nOne.xPos - nTwo.xPos);

        toReturn += Mathf.Abs(nOne.yPos - nTwo.yPos);

        return toReturn;
    }
}

public class Node
{
    public int xPos;
    public int yPos;

    public GameObject tile;
    public GameObject entity;

    public bool walkAble;

    public Node(int x, int y, bool walk, GameObject _tile)
    {
        tile = _tile;
        xPos = x;
        yPos = y;
        walkAble = walk;
    }

    public void LightUp()
    {
        if(tile != null)
        {
            tile.GetComponent<Tile>().LightUp();
        }
    }

    public bool CanBeWalkedOn()
    {
        if(walkAble == false || entity != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
