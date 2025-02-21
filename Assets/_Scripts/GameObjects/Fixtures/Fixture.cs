using System.Collections;
using System.Collections.Generic;
using components.controllables;
using UnityEngine;


[System.Serializable]
public struct FixtureData : IGrowable
{
    [Tooltip("Current growth in seconds")]
    public int CurrentGrowTime;
    [Tooltip("Total time required to grow")]
    public int TotalGrowTime;
    [Tooltip("Position spawned in")]
    public Vector3 Pos;

    

    public void IncrementProgress(int t)
    {
        CurrentGrowTime = Mathf.Clamp(CurrentGrowTime + t, 0, TotalGrowTime);
    }

    public bool IsMature() => CurrentGrowTime >= TotalGrowTime; 
}

[RequireComponent(typeof(Collider2D))]
public class Fixture : MonoBehaviour, ITappable, IBurnable
{
    public FixtureData Data => data;
    
    [SerializeField] FixtureData data;
    [SerializeField] List<Sprite> sprites;
    
    int cycle;

    public void Burn(int damage)
    {
        data.IncrementProgress(-damage);
        UpdateSprite();
    }

    public void onUserInput(TouchArgs e)
    {
        Debug.Log("Boost tree growth");
        int boost = 4;
        data.IncrementProgress(boost);
        UpdateSprite();
    }

    private void OnEnable()
    {
        cycle = data.TotalGrowTime / (sprites.Count - 1);
        UpdateSprite();
        Invoke("Grow", 1);
    }

    void Grow()
    {
        data.IncrementProgress(1);
        UpdateSprite();

        if (!data.IsMature())
            Invoke("Grow", 1);
    }


    void UpdateSprite()
    {
        // GROW UNTIL THE LAST CYCLE BEFORE MATURE SPRITE
        if(data.CurrentGrowTime % cycle == 0 && data.CurrentGrowTime < cycle * (sprites.Count - 1))
        {
            //Debug.Log($"{data.currentGrowTime} / {cycle} = {data.currentGrowTime / cycle}");
            SetSprite(data.CurrentGrowTime / cycle);
        }
        else if(data.CurrentGrowTime == data.TotalGrowTime)
        {
            SetSprite(sprites.Count - 1);
        }
    }

    void SetSprite(int i)
    {
        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer renderer))
        {
            renderer.sprite = sprites[i];
        }
    }
}