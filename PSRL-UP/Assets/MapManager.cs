using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject tile;
    public Transform player;

    public GameObject charac;

    public int x;
    public int y;

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
                if(Random.value >.9f)
                {
                    GameObject instance = Instantiate(tile, new Vector2((w * .5f) + h * -0.5f, (w * .25f) + h * .25f), Quaternion.identity);
                    GameObject instanceTwo = Instantiate(charac, new Vector2((w * .5f) + h * -0.5f, (w * .25f) + h * .25f), Quaternion.identity);


                    tiles[w, h] = new Node(w, h, true, instance);
                    tiles[w, h].entity = instanceTwo;

                    instanceTwo.GetComponent<Entity>().curr_Node = tiles[w, h];

                    instance.GetComponent<Tile>().SetGridPos(w, h);
                }
                else if (Random.value > .5f)
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

        if(startPoint.yPos > 0)
        {
            adjacent.Add(tiles[startPoint.xPos, startPoint.yPos - 1]);
        }

        if(startPoint.yPos < height -1)
        {
            adjacent.Add(tiles[startPoint.xPos, startPoint.yPos + 1]);
        }

        return adjacent;
    }

    public Node GetNode(int x,int y)
    {
        return tiles[x, y];
    }
}

public class Node
{
    public int xPos;
    public int yPos;

    public GameObject tile;
    public GameObject entity;

    public bool walkAble;

    public Node(int x,int y,bool walk,GameObject _tile)
    {
        tile = _tile;
        xPos = x;
        yPos = y;
        walkAble = walk;
    }
}
