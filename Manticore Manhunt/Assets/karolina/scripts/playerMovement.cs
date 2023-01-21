using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float moveDistance = 1f;
    private Vector3 movement;

    void Update()
    {
        movement = new Vector3();
        if (Input.GetKey(KeyCode.UpArrow))
        {
            movement += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            movement += Vector3.back;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movement += Vector3.right;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement += Vector3.left;
        }
        transform.position += movement * moveDistance * Time.deltaTime;
    }
}

//This script uses the Input.GetKey() method to check if an arrow key is pressed, and the Vector3.forward, Vector3.back, Vector3.right, and Vector3.left properties to determine the direction of movement. The movement distance can be adjusted by changing the "moveDistance" variable.
//It uses a Vector3 variable called movement that is set to zero and depending on the arrow key pressed it adds or subtracts the corresponding value to the vector. Finally, it moves the object by adding the result of the movement vector multiplied by the moveDistance variable and Time.deltaTime to the current position of the object.

//You can also change the movement to be relative to the object's rotation by using the transform.TransformDirection method.

//It is important to note that for this script to work properly, the object to which it is attached should have a Rigidbody component and its constraints should be set to freeze position x, y and z.


