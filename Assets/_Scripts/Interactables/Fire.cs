using System.Collections;
using System.Collections.Generic;
using components.controllables;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] int spreadTime = 2;
    void Start()
    {
        InvokeRepeating("Spread", spreadTime, spreadTime);
    }

    void Spread()
    {
        Vector3 offset = new Vector3(Random.Range(-5, 5), Random.Range(-1, 2), 0);
        Instantiate(gameObject, this.transform.position + offset, Quaternion.identity, transform.parent);
    }
}
