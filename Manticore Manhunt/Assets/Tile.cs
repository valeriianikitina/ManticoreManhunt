using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Action onMouseDown;
    public Action onMouseUp;
    public Action onMouseEnter;
    public Action onMouseExit;

    private void OnMouseDown()
    {
        onMouseDown?.Invoke();
    }

    private void OnMouseEnter()
    {
        onMouseEnter?.Invoke();
    }

    private void OnMouseExit()
    {
        onMouseExit?.Invoke();
    }

    private void OnMouseUp()
    {
        onMouseUp?.Invoke();
    }
}
