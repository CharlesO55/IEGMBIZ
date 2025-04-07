using System.Collections;
using System.Collections.Generic;
using components.controllables;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class FixtureLot : MonoBehaviour, ITappable
{
    [SerializeField] GameObject prefab;
    [SerializeField] int leafCost;

    
    SpriteRenderer sRenderer;
    void Awake()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        sRenderer.color = Color.gray;

        if(prefab.TryGetComponent<SpriteRenderer>(out SpriteRenderer c))
        {
            this.transform.localScale = c.transform.localScale;
            this.sRenderer.sprite = c.sprite;
        }

        RegisterToManager();
    }

    void RegisterToManager()
    {
        if (FixtureManager.I)
        {
            FixtureManager.I.RegisterLot(this);
            gameObject.SetActive(false);
        }
        else
        {
            Invoke("RegisterToManager", 0.1f);
        }
    }

    public void UpdatePurchasability()
    {
        bool affordable = PlayerProgress.I.CanBuy(leafCost);
        sRenderer.color = affordable ? Color.grey : Color.black;
    }

    public void onUserInput(TouchArgs e)
    {
        FindObjectOfType<BuildMenu>(true).OpenMenu(this);
    }
}