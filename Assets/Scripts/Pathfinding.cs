using UnityEngine;
using System.Collections.Generic;

public class Pathfinding
{

    public static List<IntegerVector2> FindPath(Map map, IntegerVector2 startPos, IntegerVector2 targetPos)
    {
        // find path
        List<CellInfo> nodes_path = _ImpFindPath(map, startPos, targetPos);

        // convert to a list of points and return
        List<IntegerVector2> ret = new List<IntegerVector2>(); 
        if (nodes_path != null)
        {
            foreach (CellInfo node in nodes_path)
            {
                ret.Add(new IntegerVector2(node.position.x, node.position.y));
            }
        }
        return ret;
    }

    // internal function to find path dont use this one from outside
    private static List<CellInfo> _ImpFindPath(Map map, IntegerVector2 startPos, IntegerVector2 targetPos)
    {
        CellInfo startNode = map.GetCell(startPos.x, startPos.y).info;
        CellInfo targetNode = map.GetCell(targetPos.x, targetPos.y).info;

        List<CellInfo> openSet = new List<CellInfo>();
        HashSet<CellInfo> closedSet = new HashSet<CellInfo>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            CellInfo currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode) 
            {   
                return RetracePath(map, startNode, targetNode);
            }

            foreach (CellInfo neighbour in map.GetNeighbours(currentNode))
            {
                if (neighbour.cell.type.GetType() != typeof(Empty) || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour) * (int)(10.0f * neighbour.penalty);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }

        return null;
    }

    private static List<CellInfo> RetracePath(Map map, CellInfo startNode, CellInfo endNode)
    {
        List<CellInfo> path = new List<CellInfo>();
        CellInfo currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }

    private static int GetDistance(CellInfo nodeA, CellInfo nodeB)
    {
        int dstX = Mathf.Abs(nodeA.position.x - nodeB.position.x);
        int dstY = Mathf.Abs(nodeA.position.y - nodeB.position.y);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}

