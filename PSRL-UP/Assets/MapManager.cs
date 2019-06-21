using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AimingMode { notSpecified,line,diagonal}
public enum Direction { Up,Down,Left,Right,NotSpecified}

public class MapManager : MonoBehaviour
{
    public GameObject tile;

    public Node[,] tiles;
    public int width;
    public int height;


    PathFinding _PF;

    private void Awake()
    {
        _PF = FindObjectOfType<PathFinding>();
        tiles = new Node[width, height];

        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                if(w == width -1 || h == height -1)
                {
                    SpawnTile(w, h);
                }
                else if (Random.value > .3f)
                {
                    SpawnTile(w, h);
                }
                else
                {
                    tiles[w, h] = new Node(w, h, false, null);
                }
            }
        }

        PurgeTiles();
    }

    void PurgeTiles()
    {
        for(int w =0;w < width -1;w++)
        {
            for(int h = 0;h < height-1;h++)
            {
                Node curr = tiles[w, h];

                if(_PF.IsReachable(tiles[width-1,height-1],curr) == false)
                {
                    if(curr.tile != null)
                    {
                        Destroy(curr.tile);
                    }

                    curr.walkAble = false;
                }
            }
        }
    }

    void SpawnTile(int w,int h)
    {
        GameObject instance = Instantiate(tile, new Vector2((w * .5f) + h * -0.5f, (w * .25f) + h * .25f), Quaternion.identity, transform);
        tiles[w, h] = new Node(w, h, true, instance);
        instance.GetComponent<Tile>().SetGridPos(w, h);
    }

    public void SpawnCreatures(List<GameObject> toSpawn,Owner toSet)
    {
        int i = 0;

        foreach (GameObject g in toSpawn)
        {
            Node chosen = null;

            while (chosen == null)
            {
                int x = Random.Range(0, width);
                int y = Random.Range(0, height);
                chosen = tiles[x, y];

                if (chosen != null && chosen.entity == null && chosen.walkAble == true)
                {

                }
                else
                {
                    for (int xx = 0; xx < width; xx++)
                    {
                        for (int yy = 0; yy < height; yy++)
                        {
                            if (tiles[xx, yy].walkAble == true && tiles[xx, yy].entity == null)
                            {
                                chosen = null;
                            }
                        }
                    }
                }
            }

            if(chosen.entity != null || chosen.walkAble == false)
            {
                break;
            }

            GameObject instanceTwo = Instantiate(g, chosen.tile.transform.position, Quaternion.identity);
            chosen.entity = instanceTwo;
            instanceTwo.GetComponent<Entity>().curr_Node = chosen;
            instanceTwo.GetComponent<Entity>().myOwner = toSet;

            instanceTwo.name = instanceTwo.name + i.ToString();
            i++;
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

    public Direction GetDirection(Node origin,Node target)
    {
        if(origin.xPos == target.xPos)
        {
            if(origin.yPos > target.yPos)
            {
                return Direction.Down;
            }
            else if(origin.yPos < target.yPos)
            {
                return Direction.Up;
            }
        }
        else if(origin.yPos == target.yPos)
        {
            if (origin.xPos > target.xPos)
            {
                return Direction.Right;
            }
            else if (origin.xPos < target.xPos)
            {
                return Direction.Left;
            }
        }
     
        return Direction.NotSpecified;
    }

    public Node GetNode(Node origin,Direction d,int distance)
    {
        Vector2Int offset = Vector2Int.zero;

        switch(d)
        {
            case Direction.Up:
                offset.y = 1;
                break;

            case Direction.Down:
                offset.y = -1;
                break;

            case Direction.Left:
                offset.x = 1;
                break;

            case Direction.Right:
                offset.x = -1;
                break;
        }

        offset = offset * distance;

        int newX = origin.xPos + offset.x;
        int newY = origin.yPos + offset.y;

        if(newX >=0 && newX < width && newY >= 0 && newY < height)
        {
            return tiles[origin.xPos + offset.x, origin.yPos + offset.y];
        }
        else
        {
            return null;
        }
    }
}

[System.Serializable]
public class Node
{
    public int xPos;
    public int yPos;

    public GameObject tile;
    public GameObject entity;

    public bool walkAble;

    public int gCost;
    public int hCost;
    public Node parent;

    public List<TileEffect> effects;

    public Node(int x, int y, bool walk, GameObject _tile)
    {
        tile = _tile;
        xPos = x;
        yPos = y;
        walkAble = walk;
        effects = new List<TileEffect>();
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

    public int FCost => (gCost + hCost);
}
