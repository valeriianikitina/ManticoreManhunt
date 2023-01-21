using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    public GameObject cubePrefab;
    public int gridWidth = 10;
    public int gridHeight = 10;
    public float cubeSize = 1f;

    void Start()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                Vector3 pos = new Vector3(x * cubeSize, 0, z * cubeSize);
                Instantiate(cubePrefab, pos, Quaternion.identity);
            }
        }
    }
}
