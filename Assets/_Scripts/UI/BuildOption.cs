using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildOption : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int leafCost;
    [SerializeField] GameObject prefab;

    [SerializeField] Image bg;

    private void OnEnable()
    {
        UpdateSpriteColor();
    }

    public void UpdateSpriteColor()
    {
        if (!PlayerProgress.I.CanBuy(leafCost))
        {
            bg.color = new Color(0.2f, 0.2f, 0.2f);
        }
        else
        {
            bg.color = Color.gray;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!PlayerProgress.I.CanBuy(leafCost))
            return;

        BuildMenu.I.SelectBuild(this);
        bg.color = Color.white;
    }

    public GameObject Buy()
    {
        PlayerProgress.I.Add(-leafCost);
        return prefab;
    }

}