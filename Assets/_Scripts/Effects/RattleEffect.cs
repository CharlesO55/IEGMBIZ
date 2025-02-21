using System.Collections;
using UnityEngine;

public class RattleEffect : MonoBehaviour
{
    [SerializeField] public float rattleStrength = 0.1f;  // How far the object moves during the rattle
    public float rattleDuration = 1f;    // Duration of each rattle burst
    private Vector3 originalPosition;
    private float rattleTimer = 0f;

    void Start()
    {
        originalPosition = transform.position;
    }

    public void Shake()
    {
        StartCoroutine(DoShake());
    }

    IEnumerator DoShake()
    {
        while (rattleTimer < rattleDuration)
        {
            rattleTimer += Time.deltaTime;
            
            float shakeAmount = Mathf.PerlinNoise(Time.time * 10, 0f) * rattleStrength;
            transform.position = originalPosition + new Vector3(shakeAmount, shakeAmount, shakeAmount);
            
            yield return null;
        }

        rattleTimer = 0f;
        transform.position = originalPosition;
    }
}
