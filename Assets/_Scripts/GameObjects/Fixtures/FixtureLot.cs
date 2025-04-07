using System.Collections;
using System.Collections.Generic;
using components.controllables;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FixtureLot : MonoBehaviour, ITappable
{
    [SerializeField] GameObject prefab;
    [SerializeField] int leafCost;

    
    [SerializeField] SpriteRenderer sRenderer;
    void Awake()
    {
        sRenderer.color = Color.gray;

        //if(prefab.TryGetComponent<SpriteRenderer>(out SpriteRenderer c))
        //{
        //    this.transform.localScale = c.transform.localScale;
        //    this.sRenderer.sprite = c.sprite;
        //}

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
        sRenderer.color = affordable ? Color.white : Color.grey;
    }

    public void onUserInput(TouchArgs e)
    {
        FindObjectOfType<BuildMenu>(true).OpenMenu(this);
    }
}