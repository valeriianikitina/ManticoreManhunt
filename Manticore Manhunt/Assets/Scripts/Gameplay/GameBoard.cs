using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameBoard : MonoBehaviour
{

    [SerializeField]
    private int width;
    [SerializeField]
    private GridCell[] cells;

    public int Width => width;
    public int Height => cells.Length / width;
    public Vector2Int Size => new Vector2Int(Width, Height);

    public GridCell GetCell(Vector2Int coord)
    {
        return IsInBoard(coord) ? cells[coord.x + coord.y * width] : GridCell.Empty;
    }

    public bool CanHasAlive(Vector2Int coord)
    {
        return GetCell(coord) != GridCell.Manticore;
    }

    public bool IsInBoard(Vector2Int coord)
    {
        return coord.x >= 0 && coord.y >= 0 && coord.x < Width && coord.y < Height;
    }

    public bool CanHasPassage(Vector2Int coord)
    {
        return IsInBoard(coord) && GetCell(coord) != GridCell.Wall;
    }

    private IList<Vector2Int> GetNeighbours(Vector2Int originalPoint)
    {
        return new Vector2Int[] { 
            originalPoint + Vector2Int.up,
            originalPoint + Vector2Int.right,
            originalPoint + Vector2Int.down,
            originalPoint + Vector2Int.left
        };
    }

    private IList<Vector2Int> GetWalkableNeighbours(Vector2Int coord)
    {
        return GetNeighbours(coord).Where(pos => CanHasPassage(pos)).ToList();
    }

    public IList<IList<Vector2Int>> GetDoggoPaths(Vector2Int startingPos, Vector2Int endPos)
    {
        Dictionary<Vector2Int, List<Vector2Int>> possibleParents = new();
        Dictionary<Vector2Int, int> dist = new();
        HashSet<Vector2Int> seen = new();
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        queue.Enqueue(startingPos);
        dist[startingPos] = 0;
        while (queue.Count > 0)
        {
            Vector2Int pos = queue.Dequeue();
            if (pos == endPos)
                break;
            if (seen.Contains(pos))
                continue;
            seen.Add(pos);
            foreach(var neighbour in GetWalkableNeighbours(pos))
            {
                if (possibleParents.ContainsKey(neighbour)) 
                {
                    if (dist[neighbour] == dist[pos] + 1)
                        possibleParents[neighbour].Add(pos);
                }
                else
                {
                    possibleParents[neighbour] = new List<Vector2Int>() { pos };
                    dist[neighbour] = dist[pos] + 1;
                }
                queue.Enqueue(neighbour);
            }
        }
        return GenerateAllPaths(endPos, startingPos, possibleParents);
    }

    private IList<IList<Vector2Int>> GenerateAllPaths(Vector2Int endPos, Vector2Int startingPos,
        Dictionary<Vector2Int, List<Vector2Int>> possibleParents)
    {
        List<IList<Vector2Int>> allPaths = new();
        if (endPos == startingPos)
        {
            allPaths.Add(new List<Vector2Int>() { startingPos });
            return allPaths;
        }
        foreach (var parent in possibleParents[endPos])
        {
            var pathsUntilHere = GenerateAllPaths(parent, startingPos, possibleParents);
            foreach (var path in pathsUntilHere)
            {
                path.Add(endPos);
                allPaths.Add(path);
            }
        }
        return allPaths;
    }

}
