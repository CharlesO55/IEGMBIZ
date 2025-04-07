using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenu : MonoBehaviour
{
    public static BuildMenu I = null;

    [SerializeField] ScaleInOut scaleAnim;
    [SerializeField] Button confirmBtn;


    FixtureLot lot;

    BuildOption selected;

    private void Awake()
    {
        if (I == null)
            I = this;
        else
            Destroy(gameObject);
    }

    public void OpenMenu(FixtureLot target)
    {
        gameObject.SetActive(true);
        scaleAnim.AnimIn();

        selected = null;
        confirmBtn.interactable = false;

        lot = target;
    }

    
    public void SelectBuild(BuildOption selection)
    {
        // Old selection
        selected?.UpdateSpriteColor();

        this.selected = selection;
        confirmBtn.interactable = true;
    }


    public void Purcahse()
    {
        FixtureManager.I.UnregisterLot(lot);
        lot.gameObject.SetActive(false);
        
        FixtureManager.I.BuildFixture(lot.transform.position, selected.Buy());

        confirmBtn.interactable = false;
        scaleAnim.AnimOut();
    }

}