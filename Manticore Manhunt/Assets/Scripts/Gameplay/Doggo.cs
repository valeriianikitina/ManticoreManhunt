using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Doggo : MonoBehaviour
{
    [SerializeField]
    private GameBoardView gameBoardView;
    [SerializeField]
    private GameBoard gameBoard;
    [SerializeField]
    private GridMovement gridMovement;

    private int pathId = -1;
    private IList<IList<Vector2Int>> paths;

    // Start is called before the first frame update
    void Start()
    {
        gameBoardView.onTileHoverEnter += GeneratePaths;
        gameBoardView.onTileHoverExit += RemovePaths;
        gameBoardView.onTileSelected += GoToTile;
    }

    private void Update()
    {
        if (paths != null && Input.GetKeyDown(KeyCode.Tab))
        {
            pathId = (pathId + 1) % paths.Count;
            HighlightPath();
        }
    }
    void GeneratePaths(Vector2Int coord)
    {
        paths = gameBoard.GetDoggoPaths(gridMovement.position, coord);
        pathId = 0;
        HighlightPath();
    }
    void HighlightPath()
    {
        gameBoardView.RemoveHighlights();
        gameBoardView.HighlightPath(paths[pathId]);
    }
    void RemovePaths(Vector2Int coord)
    {
        gameBoardView.RemoveHighlights();
        paths = null;
    }

    // Update is called once per frame
    void GoToTile(Vector2Int coord)
    {
        gridMovement.FollowPath(paths[pathId]);
    }
}
