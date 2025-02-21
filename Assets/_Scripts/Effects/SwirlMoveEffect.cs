using System.Collections;
using UnityEngine;

public class SwirlMoveEffect : MonoBehaviour
{
    [SerializeField] float speed = 5f;         // Speed of the movement along the X-axis
    [SerializeField] float amplitude = 2f;     // Amplitude of the circular motion (height of the rollercoaster)
    [SerializeField] float frequency = 1f;     // Frequency of the circular motion (how fast it oscillates)
    [SerializeField] float lifetime = 20;              // Used to track time for movement

    [SerializeField] bool destroyOnFinish = true;
    public void RandomizeFrequency()
    {
        frequency = Random.Range(1, 5);
    }
    public void Swirl()
    {
        StartCoroutine(DoSwirl());
    }

    IEnumerator DoSwirl()
    {
        Vector3 offset = this.transform.position;
        Vector3 position = this.transform.position;

        float t = 0;
        while (t < lifetime)
        {
            t += Time.deltaTime;
            offset.x += Time.deltaTime * speed;

            position.x = Mathf.Cos(t * frequency) * amplitude;
            position.y = Mathf.Sin(t * frequency) * amplitude;
            transform.position = position + offset;

            yield return null;
        }

        if(destroyOnFinish)
            Destroy(gameObject);
    }
}
