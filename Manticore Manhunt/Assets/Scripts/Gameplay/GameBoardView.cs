using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameBoardView : MonoBehaviour
{
    [SerializeField]
    private GameBoard gameBoard;
    [SerializeField]
    private float cellSize;
    [SerializeField]
    private Transform boardCenter;

    [SerializeField]
    private GameObject manticorePrefab;
    [SerializeField]
    private GameObject smallBushPrefab;
    [SerializeField]
    private GameObject bigBushPrefab;
    [SerializeField]
    private GameObject flowerPrefab;
    [SerializeField]
    private GameObject wallPrefab;
    [SerializeField]
    private GameObject unexploredPrefab;
    [SerializeField]
    private GameObject highlightPrefab;

    public Action<Vector2Int> onTileSelected;
    public Action<Vector2Int> onTileHoverEnter;
    public Action<Vector2Int> onTileHoverExit;

    private List<GameObject> highlights = new List<GameObject>();

    private void Start()
    {
        for (int x = 0; x < gameBoard.Width; x++)
            for (int y = 0; y < gameBoard.Height; y++)
                PutUnexploredTileAt(new Vector2Int(x, y));
    }

    public void PutUnexploredTileAt(Vector2Int coord)
    {
        var tile = Instantiate(unexploredPrefab).GetComponent<Tile>();
        tile.transform.position = GetPosition(coord);
        tile.onMouseDown += () => onTileSelected.Invoke(coord);
        tile.onMouseEnter += () => onTileHoverEnter.Invoke(coord);
        tile.onMouseExit += () => onTileHoverExit.Invoke(coord);
    }

    public void HighlightPath(IList<Vector2Int> path)
    {
        foreach (var coord in path)
        {
            var highlight = Instantiate(highlightPrefab);
            highlight.transform.position = GetPosition(coord);
            highlights.Add(highlight);
        }
    }

    public void RemoveHighlights()
    {
        foreach (var h in highlights)
            Destroy(h);
        highlights.Clear();
    }

    public Vector3 GetPosition(Vector2Int coord)
    {
        return boardCenter.position + (Vector3)((Vector2)coord - ((Vector2)gameBoard.Size - Vector2.one) / 2) * cellSize;
    }

    public void Reveal(Vector2Int coord)
    {
        var tile = GetTile(gameBoard.GetCell(coord));
        if (tile is not null)
        {
            tile = Instantiate(tile);
            tile.transform.position = GetPosition(coord);
        }
    }

    private GameObject GetTile(GridCell cell)
    {
        switch (cell)
        {
            case GridCell.Wall:
                return wallPrefab;
            case GridCell.SmallBush:
                return smallBushPrefab;
            case GridCell.BigBush:
                return bigBushPrefab;
            case GridCell.Flower:
                return flowerPrefab;
            case GridCell.Manticore:
                return manticorePrefab;
            default:
                return null;
        }
    }
}
