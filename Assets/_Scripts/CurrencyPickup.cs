using UnityEngine;
using components.controllables;

[RequireComponent(typeof(Collider2D))]
class CurrencyPickup : MonoBehaviour, ITappable
{
    [SerializeField] int value;

    public void onUserInput(TouchArgs e)
    {
        PlayerProgress.I.Add(value);

        Destroy(this.gameObject);
    }
}