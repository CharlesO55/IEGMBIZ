using System.Collections;
using System.Collections.Generic;
using components.controllables;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.VFX;

[RequireComponent(typeof(Collider2D))]
public class Animal : MonoBehaviour, IHoldable
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

    [SerializeField] VisualEffect cryingVFX;
    [SerializeField] VisualEffect loveVFX;

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
                cryingVFX.Stop();
                Invoke("Live", 1);
            }
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
        loveVFX.enabled = true;
        loveVFX.Play();

        GetComponent<SpriteRenderer>().sprite = happySprite;

        GetComponent<ReadyCollect>().IsMature = true;
        this.enabled = false;
    }
}