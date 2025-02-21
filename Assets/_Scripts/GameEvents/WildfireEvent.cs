using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildfireEvent : MonoBehaviour
{
    [SerializeField] GameObject firePrefab;

    [SerializeField] [Range(0,1)] float wildfireChance = 0.1f;

    private void Start()
    {
        InvokeRepeating("DoRNG", 1, 1);
    }

    void DoRNG()
    {
        if (Random.value < wildfireChance)
        {
            StartFire();
        }
    }

    void StartFire()
    {
        Vector3 position = Camera.main.transform.position;
        position.z = transform.position.z;

        for (int i = 0; i < Random.Range(2, 6); i++)
        {
            Vector3 offset = new Vector3(Random.Range(-2, 3), Random.Range(-2, 3));

            GameObject o = Instantiate(firePrefab, position + offset, Quaternion.identity, transform);
        }
    }
}