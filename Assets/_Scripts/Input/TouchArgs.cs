using UnityEngine;

public struct TouchArgs
{
    public Vector2 startPos;
    public Vector2 currPos;
    public Vector2 direction;
    public GameObject hit;

    public bool isTouchEnd;

    public TouchArgs(Vector2 start)
    {
        startPos = start;
        currPos = start;
        
        direction = Vector2.zero;

        hit = null;
        isTouchEnd = false;
    }
}