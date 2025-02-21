using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeafCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI UI_text;
    

    private void Awake()
    {
        RegisterToPlayerProgress();        
    }

    void RegisterToPlayerProgress()
    {
        if (!PlayerProgress.I)
        {
            Invoke("RegisterToPlayerProgress", 0.1f);
        }
        else
        {
            PlayerProgress.I.onCurrencyAdd.AddListener(UpdateCounter);
            UpdateCounter(PlayerProgress.I.Leaves);
        }
    }

    private void OnDestroy()
    {
        PlayerProgress.I.onCurrencyAdd.RemoveListener(UpdateCounter);
    }

    private void UpdateCounter(int value)
    {
        this.UI_text.text = value.ToString();
    }
}
