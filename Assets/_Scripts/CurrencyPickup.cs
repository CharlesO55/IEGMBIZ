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

    private void Start()
    {
        if(TryGetComponent<LaunchEffect>(out LaunchEffect c)){
            c.Launch();
        }
    }
    public int Value { get { return value; } set { this.value = value; } }
}