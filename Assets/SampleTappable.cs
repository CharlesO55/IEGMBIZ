using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tappable : MonoBehaviour, ITappable
{
    public void onUserInput(TouchArgs e)
    {
        Debug.Log($"Tapped {name}");
    }
}
