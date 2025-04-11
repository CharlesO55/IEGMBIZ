using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class AnimalInteraction : MonoBehaviour
{
    [System.Serializable]
    struct AnimalInfo{
        public GameObject prefab;
        public Sprite normal;
        public Sprite happy;
        public int injuries;
    }


    [SerializeField] GameObject injuryPrefab;
    [SerializeField] VisualEffect heartVFX;

    [Header("Animals")]
    [SerializeField] AnimalInfo[] animalSprites;
    [SerializeField] Image animalImage;


    AnimalInfo currAnimal;
    Vector3 spawnLoc;


    private void OnEnable()
    {
        injuryPrefab.SetActive(true);
        heartVFX.Stop();

        currAnimal = animalSprites[Random.Range(0, animalSprites.Length)];
        animalImage.sprite = currAnimal.normal;

        for (int i = 0; i < currAnimal.injuries; i++)
        {
            Vector2 randomPos = Random.insideUnitCircle * 300;

            GameObject clone = Instantiate(injuryPrefab, this.transform);
            RectTransform rt = clone.GetComponent<RectTransform>();
            rt.anchoredPosition = randomPos;
        }

        injuryPrefab.SetActive(false);
    }


    public void StartInteraction(Transform targetSpawn)
    {
        gameObject.SetActive(true);
        spawnLoc = targetSpawn.position;
    }
    public void Remove(RectTransform rect)
    {
        currAnimal.injuries--;
        LeanTween.moveY(rect, 100, 0.3f).setEase(LeanTweenType.easeOutSine).setDestroyOnComplete(true);

        if(currAnimal.injuries == 0)
        {
            heartVFX.Play();
            animalImage.sprite = currAnimal.happy;

            LeanTween.scale(gameObject, Vector2.zero, 2).setEase(LeanTweenType.easeInOutCirc).setOnComplete(SpawnAnimal);
        }
    }

    private void SpawnAnimal()
    {
        Instantiate(currAnimal.prefab, spawnLoc, Quaternion.identity);
        this.gameObject.SetActive(false);
    }
}