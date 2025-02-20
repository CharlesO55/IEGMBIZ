using UnityEngine;

namespace components.controllables
{
    // SAMPLE JUST PRINT THE NAME
    public class Tappable : MonoBehaviour, ITappable
    {
        public void onUserInput(TouchArgs e)
        {
            Debug.Log($"Tapped {name}");
        }
    }
}