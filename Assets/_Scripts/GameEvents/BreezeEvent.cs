using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreezeEvent : MonoBehaviour
{
    [SerializeField] GameObject swirlingLeaf;

    [SerializeField] int breezeAmount = 5;
    [SerializeField] int triggerTime = 10; 

    int i;
    private void Start()
    {
        Invoke("RandomizeSpawn", triggerTime);
    }

    void RandomizeSpawn()
    {
        SpawnLeaf();
        i++;

        if (i < breezeAmount)
            Invoke("RandomizeSpawn", Random.value);
        else
        {
            i = 0;
            Invoke("RandomizeSpawn", triggerTime);
        }
    }

    void SpawnLeaf()
    {
        Vector3 spawnPos = Camera.main.transform.position + Vector3.left * 15;
        spawnPos.z = this.transform.position.z;

        spawnPos.y = Random.Range(-4, 5);
        GameObject o = Instantiate(swirlingLeaf, spawnPos, Quaternion.identity, transform);
        if(o.TryGetComponent<SwirlMoveEffect>(out SwirlMoveEffect c)){
            c.Swirl();
            c.RandomizeFrequency();
        }
    }
}