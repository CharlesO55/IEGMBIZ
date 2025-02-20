using UnityEngine;

namespace components.controllables
{
    public class SampleHoldable : MonoBehaviour, IHoldable
    {
        // SAMPLE JUST FOLLOW WHERE THE FINGER IS
        public void onUserInput(TouchArgs e)
        {
            float z = this.transform.position.z;
            Vector3 touchPoint = Camera.main.ScreenToWorldPoint(e.currPos);
            touchPoint.z = z;
            this.transform.position = touchPoint;
        }
    }
}