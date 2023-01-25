using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    [SerializeField]
    private GameBoardView gameBoardView;
    [SerializeField]
    private GameBoard gameBoard;
    private VictoryNotification victoryNotification;

    public float moveSpeed = 2f; // movement speed of the object
    public Vector2 gridSize = new Vector2(1, 1); // size of the grid in tiles

    private Vector3 targetPos; // target position to move to
    private bool isMoving = false; // flag to check if the object is currently moving
    [SerializeField]
    private Vector2Int initialPosition;
    [SerializeField]
    private DefeatNotification defeatNotification;

    private Vector2Int direction;
    public Vector2Int position;

    private IList<Vector2Int> path;
    private bool followingPath = false;
    private int currentPathNodeIndex = 0;
    [SerializeField]
    private bool isControllable;

    private void Awake()
    {
        position = initialPosition;
        targetPos = transform.position;
    }

    private void Start()
    {
        victoryNotification = FindObjectOfType<VictoryNotification>();
        transform.position = gameBoardView.GetPosition(position);
        gameBoardView.Reveal(position);
        if (isControllable)
            RevealSurroundings();
    }

    private void RevealSurroundings()
    {
        gameBoardView.Reveal(position + Vector2Int.up);
        gameBoardView.Reveal(position + Vector2Int.down);
        gameBoardView.Reveal(position + Vector2Int.left);
        gameBoardView.Reveal(position + Vector2Int.right);
    }

    public void FollowPath(IList<Vector2Int> path)
    {
        if (followingPath)
            return;

        this.path = path;
        followingPath = true;
        currentPathNodeIndex = 1;
        targetPos = gameBoardView.GetPosition(path[currentPathNodeIndex]);
        isMoving = true;
        if (gameBoard.GetCell(position) == GridCell.Flower)
        {
            victoryNotification.UncollectCollectible(position);
        }
    }

    void Update()
    {
        if (!isMoving)
        {
            if (isControllable && !followingPath)
            { 
                // get input from arrow keys or WASD
                int horizontal = Input.GetKeyDown(KeyCode.LeftArrow) ? -1 : Input.GetKeyDown(KeyCode.RightArrow) ? 1 : 0;
                int vertical = Input.GetKeyDown(KeyCode.DownArrow) ? -1 : Input.GetKeyDown(KeyCode.UpArrow) ? 1 : 0;

                // check if there is any input
                if (horizontal != 0 || vertical != 0)
                {
                    direction = new Vector2Int(horizontal, vertical);
                    if (gameBoard.CanHasPassage(position + direction))
                    {
                        // calculate target position
                        targetPos = gameBoardView.GetPosition(position + direction);

                        // start moving the object
                        isMoving = true;
                        if (gameBoard.GetCell(position) == GridCell.Flower)
                        {
                            victoryNotification.UncollectCollectible(position);
                        }
                    }
                }
            }
        }
        else
        {
            // move the object towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            // check if the object has reached the target position
            if (transform.position == targetPos)
            {
                if (followingPath)
                {
                    position = path[currentPathNodeIndex];
                    gameBoardView.Reveal(position);
                    currentPathNodeIndex++;
                    if (currentPathNodeIndex < path.Count)
                        targetPos = gameBoardView.GetPosition(path[currentPathNodeIndex]);
                    else
                    {
                        isMoving = false;
                        followingPath = false;
                        if (gameBoard.GetCell(position) == GridCell.Flower)
                        {
                            victoryNotification.CollectCollectible(position);
                        }
                        GameManager.Instance.OnMove();
                    }
                }
                else
                {
                    position += direction;
                    gameBoardView.Reveal(position);
                    gameBoardView.Reveal(position + direction);
                    isMoving = false;
                    if (gameBoard.GetCell(position) == GridCell.Flower)
                    {
                        victoryNotification.CollectCollectible(position);
                    }
                    GameManager.Instance.OnMove();
                }
                var alive = gameBoard.CanHasAlive(position);
                if (!alive)
                {
                    defeatNotification.ShowDefeat();
                    this.enabled = false;
                }
            }
        }
    }
}
