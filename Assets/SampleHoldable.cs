using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleHoldable : MonoBehaviour, IHoldable
{
    public void onUserInput(TouchArgs e)
    {
        float z = this.transform.position.z;
        Vector3 touchPoint = Camera.main.ScreenToWorldPoint(e.currPos);
        touchPoint.z = z;
        this.transform.position = touchPoint;
    }
}