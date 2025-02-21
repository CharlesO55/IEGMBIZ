using System.Collections;
using UnityEngine;

public class LaunchEffect : MonoBehaviour
{
    [SerializeField] float launchHeight = 5f;
    [SerializeField] float speed = 10f;
    [SerializeField] [Range(0.1f, 100)] float xDir = 1;
    
    Vector3 targetPosition;
    Vector3 startPosition;
    float flightTime;
    
    public void Launch()
    {
        startPosition = transform.position;
        targetPosition = transform.position;
        targetPosition.x += xDir;
        flightTime = xDir / speed;
        
        StartCoroutine("DoLaunch");
    }

    IEnumerator DoLaunch()
    {
        float time = 0;
        while (time < flightTime)
        {
            time += Time.deltaTime;

            float t = time / flightTime;

            Vector3 horizontalPosition = Vector3.Lerp(startPosition, targetPosition, t);

            float verticalProgress = Mathf.Sin(t * Mathf.PI); 
            float verticalPosition = Mathf.Lerp(0, launchHeight, verticalProgress);

            transform.position = new Vector3(horizontalPosition.x, startPosition.y + verticalPosition, horizontalPosition.z);
            
            yield return null;
        }

        transform.position = targetPosition;
    }
}
