using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    MapManager _MM;

    List<Node> askedPath;

    void Awake()
    {
        askedPath = new List<Node>();
        _MM = FindObjectOfType<MapManager>();
    }

    public int GetWalkingDistance(Node startNode,Node endNode)
    {
        return FindPath(startNode, endNode).Count;
    }

    public bool IsReachable(Node startNode,Node targetNode)
    {
        if(startNode == targetNode)
        {
            return true;
        }

        int i = FindPath(startNode, targetNode).Count;

        if(i > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<Node> GetPath(Node startNode,Node targetNode)
    {
        return FindPath(startNode, targetNode);
    }

    List<Node> FindPath(Node startNode, Node targetNode)
    {
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node node = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCost < node.FCost || openSet[i].FCost == node.FCost)
                {
                    if (openSet[i].hCost < node.hCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            if (node == targetNode)
            {             
                return RetracePath(startNode, targetNode); ;
            }

            foreach (Node neighbour in _MM.GetNeighbours(node))
            {
                if (!neighbour.CanBeWalkedOn() || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }

        return new List<Node>();
    }

    List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        return path;
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.xPos - nodeB.xPos);
        int dstY = Mathf.Abs(nodeA.yPos - nodeB.yPos);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
