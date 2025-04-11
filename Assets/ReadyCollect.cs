using System.Collections;
using System.Collections.Generic;
using components.controllables;
using UnityEngine;

public class ReadyCollect : MonoBehaviour, ITappable
{
    [SerializeField] int LeafRewardAmount;
    
    public bool IsMature = false;

    
    public void onUserInput(TouchArgs e)
    {
        if (IsMature)
            Collect();
    }


    public void Collect()
    {
        Vector3 pos = this.transform.position;
        pos.z--;
        PlayerProgress.I.SpawnCurrency(pos, LeafRewardAmount);



        LeanTween.moveLocalY(gameObject, 50, 1).setEase(LeanTweenType.easeInOutElastic).setDestroyOnComplete(true);
    }
}
