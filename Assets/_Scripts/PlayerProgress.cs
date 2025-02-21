using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgress : MonoBehaviour
{
    #region SINGLETON
    static PlayerProgress i = null;
    public static PlayerProgress I => i;

    private void Awake()
    {
        if (i == null)
            i = this;
        else
            Destroy(gameObject);
    }
    #endregion

    #region DATA
    [System.Serializable] 
    struct PlayerData
    {
        public int CurrencyAmount;
    }
    [SerializeField] PlayerData data;
    #endregion

    [SerializeField] GameObject currencyPrefab;

    public void Add(int value)
    {
        data.CurrencyAmount += value;
    }

    public void SpawnCurrency(Vector3 position)
    {
        GameObject o = Instantiate(currencyPrefab, position, Quaternion.identity, transform);
        /*if(o.TryGetComponent<CurrencyPickup>(out CurrencyPickup c)){
        }*/
    }
}