using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleSwipable : MonoBehaviour, ISwipeable
{
    [SerializeField] float scrollSpeed = 5; 
    Vector3 vel;

    public void onUserInput(TouchArgs e)
    {
        vel += new Vector3(e.direction.x / Screen.width, 0, 0);
    }

    private void FixedUpdate()
    {
        Camera.main.transform.position += vel * scrollSpeed * Time.deltaTime;
        
        vel = Vector3.Slerp(vel, Vector3.zero, Time.deltaTime);
    }
}