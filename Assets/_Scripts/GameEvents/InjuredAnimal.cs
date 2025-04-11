using System.Collections;
using System.Collections.Generic;
using components.controllables;
using UnityEngine;
using UnityEngine.EventSystems;

public class InjuredAnimal : MonoBehaviour, ITappable
{
    public void onUserInput(TouchArgs e)
    {
        FindObjectOfType<AnimalInteraction>(true).StartInteraction(this.transform);
        Destroy(gameObject);
    }
}
