using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
  
    public float moveSpeed = 2f; // movement speed of the object
    public Vector2 gridSize = new Vector2(1, 1); // size of the grid in tiles

    private Vector3 targetPos; // target position to move to
    private bool isMoving = false; // flag to check if the object is currently moving

    void Update()
    {
        if (!isMoving)
        {
            // get input from arrow keys or WASD
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            // check if there is any input
            if (horizontal != 0 || vertical != 0)
            {
                // calculate target position
                targetPos = transform.position + new Vector3(horizontal * gridSize.x, vertical * gridSize.y, 0);

                // start moving the object
                isMoving = true;
            }
        }
        else
        {
            // move the object towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            // check if the object has reached the target position
            if (transform.position == targetPos)
            {
                isMoving = false;
            }
        }
    }
}
