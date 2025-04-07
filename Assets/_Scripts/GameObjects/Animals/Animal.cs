using System.Collections;
using System.Collections.Generic;
using components.controllables;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.VFX;

[RequireComponent(typeof(Collider2D))]
public class Animal : MonoBehaviour, IHoldable, ITappable
{
    #region DATA
    [System.Serializable]
    struct AnimalData : IGrowable
    {
        public int CurrentStayTime;
        public int TotalStayTime;
        public int LeafRewardAmount;
        public void IncrementProgress(int t)
        {
            CurrentStayTime = Mathf.Clamp(CurrentStayTime + t, 0, TotalStayTime);
        }

        public float GetProgress()
        {
            return CurrentStayTime / TotalStayTime;
        }
        public bool IsMature() => CurrentStayTime >= TotalStayTime;
    }
    #endregion 
    [SerializeField] AnimalData data;

    [SerializeField] VisualEffect effect;

    [SerializeField] Sprite neutralSprite;
    [SerializeField] Sprite happySprite;

    public void onUserInput(TouchArgs e)
    {
        CancelInvoke("Live");
        MoveToTouchPos(e);

        if (e.isTouchEnd)
        {
            if (TryAttachToClosestTree())
            {
                Invoke("Live", 1);
            }
        }

        else if (data.IsMature())
        {
            Leave();
        }
    }

    private void MoveToTouchPos(TouchArgs e)
    {
        float z = this.transform.position.z;
        Vector3 touchPoint = Camera.main.ScreenToWorldPoint(e.currPos);
        touchPoint.z = z;
        this.transform.position = touchPoint;
    }

    bool TryAttachToClosestTree()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 1, transform.forward);

        foreach(RaycastHit2D hit in hits)
        {
            if(hit.collider.TryGetComponent<Fixture>(out Fixture t))
            {
                if (t.Data.IsMature())
                {
                    Vector3 pos = hit.collider.transform.position;
                    pos.z--;
                    this.transform.position = pos;
                    return true;
                }
            }
        }
        return false;
    }

    void Live()
    {
        data.IncrementProgress(1);
        
        if (!data.IsMature())
            Invoke("Live", 1);
        else
        {
            OnMature();
        }
    }


    void OnMature()
    {
        effect.enabled = true;
        effect.Play();

        GetComponent<SpriteRenderer>().sprite = happySprite;
    }


    
    public void Leave()
    {
        Vector3 pos = this.transform.position;
        pos.z--;
        PlayerProgress.I.SpawnCurrency(pos, data.LeafRewardAmount);
            
        LeanTween.moveLocalY(gameObject, 50, 1).setEase(LeanTweenType.easeInOutElastic).setDestroyOnComplete(true);
    }
}