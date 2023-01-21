using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximittDestroyer : MonoBehaviour
{
    public GameObject targetObject; //objet causing the destruction- dogi
    public float proximity = 5f;
    //public GameObject[] objectsToDestroy;    
    public GameObject parentObject;
    public GameObject[] children;

    void Start()
    {
         children = new GameObject[parentObject.transform.childCount];
        for (int i = 0; i < parentObject.transform.childCount; i++)
        {
            children[i] = parentObject.transform.GetChild(i).gameObject;
        }
        //objectsToDestroy = children;

       // This script uses the childCount property of the Transform component to determine the number of children the parent object has, and then it uses the GetChild() method to assign each child to an element in the children array.
        //It is supposed that the parentObject variable is assigned in the script or in the Unity editor.
        //You can use this array to perform different actions on the children, like rotate, move or destroy them, as well as access their components, as you have a reference to each child object

    }

    void Update()
    {
        foreach (GameObject obj in children)
        {
            float distance = Vector3.Distance(obj.transform.position, targetObject.transform.position);
            if (distance <= proximity)
            {
                Destroy(obj);
            }
        }
    }

}
