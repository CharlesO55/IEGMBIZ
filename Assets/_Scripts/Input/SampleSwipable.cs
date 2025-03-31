using UnityEngine;

namespace components.controllables
{
    // SAMPLE SCROLL THE CAMERA
    public class SampleSwipable : MonoBehaviour, ISwipeable
    {
        [SerializeField] float scrollSpeed = 5;
        [SerializeField] private float minX = -10f; 
        [SerializeField] private float maxX = 10f; 

        private Vector3 targetPosition;

        private void Start()
        {
            // Initialize the target position to the current camera position
            targetPosition = Camera.main.transform.position;
        }

        public void onUserInput(TouchArgs e)
        {
            // Calculate the movement based on swipe direction and scroll speed
            float moveX = (e.direction.x / Screen.width) * scrollSpeed;

            // Update the target position based on the swipe input
            targetPosition += new Vector3(moveX, 0, 0);

            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        }

        private void Update()
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPosition, Time.deltaTime * scrollSpeed);
        }
    }
}