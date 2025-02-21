using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FixtureManager : MonoBehaviour
{
    [SerializeField] List<FixtureLot> lots;     // Vacant lost
    [SerializeField] List<Fixture> fixtures;    // Built objects


    [SerializeField] bool test;

    #region SINGLETON
    private static FixtureManager i;
    public static FixtureManager I => i;
    private void Awake()
    {
        if(i == null)
            i = this;
        else
            Destroy(gameObject);
    }
    #endregion
    public void BuildFixture(Vector3 position, GameObject prefab)
    {
        GameObject o = Instantiate(prefab, position, Quaternion.identity, transform);
        
        if(o.TryGetComponent<Fixture>(out Fixture fixture)){
            fixtures.Add(fixture);
        }
    }

    public Fixture RequestMatureFixture()
    {
        return fixtures.Where(t => t.Data.IsMature()).First();
    }



    private void Update()
    {
        ToggleLotsPurchaseMode(test);
    }

    void ToggleLotsPurchaseMode(bool enable)
    {
        foreach(FixtureLot l in lots)
        {
            l.gameObject.SetActive(enable);
            l.UpdatePurchasability();
        }
    }

    public void RegisterLot(FixtureLot lot)
    {
        this.lots.Add(lot);
    }

    public void UnregisterLot(FixtureLot lot)
    {
        this.lots.Remove(lot);
    }
}