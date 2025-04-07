using System.Collections;
using UnityEngine;

public class RattleEffect : MonoBehaviour
{
    [SerializeField] public float rattleStrength = 1;  // How far the object moves during the rattle

    [SerializeField] float rattlePeriod = 0.05f;
    [SerializeField] int rattleTimes = 4;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    public void Shake()
    {
        LeanTween.alpha(gameObject, 0, 0.05f).setLoopPingPong(4);
        LeanTween.moveLocalX(gameObject, originalPosition.x + rattleStrength, rattlePeriod)
                .setLoopPingPong(rattleTimes);
    }

    /*IEnumerator DoShake()
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
    }*/
}
