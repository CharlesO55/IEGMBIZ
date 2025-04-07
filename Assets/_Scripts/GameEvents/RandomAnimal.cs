using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimal : MonoBehaviour
{
    [SerializeField] GameObject[] animals;

    private void Start()
    {
        SpawnRandom();
    }

    void SpawnRandom()
    {
        Vector3 spawnPos = Camera.main.transform.position;
        spawnPos.x = Random.Range(-10, 10);
        spawnPos.y = Random.Range(-5, 5);
        spawnPos.z = 0;

        GameObject newSpawn = Instantiate(animals[Random.Range(0, animals.Length)], spawnPos, Quaternion.identity);


        Invoke("SpawnRandom", 15);
    }
}
