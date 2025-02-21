using UnityEngine;

public struct TouchArgs
{
    public Vector2 startPos;
    public Vector2 currPos;
    public Vector2 direction;
    public GameObject hit;
    public float timePressed;

    public bool isTouchEnd;
}