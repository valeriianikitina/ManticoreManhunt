using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dogi : MonoBehaviour
{
    public GameObject objectToMove;
    public Vector3 targetPosition;
    public float speed = 5f;
    private bool reached = false;

    void Update()
    {
        if(!reached)
        {
            float step = speed * Time.deltaTime;
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, targetPosition, step);
            if (objectToMove.transform.position == targetPosition) reached = true;
        }
    }
}
