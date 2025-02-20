using System.Collections;
using System.Collections.Generic;
using components.controllables;
using UnityEngine;

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
        public bool IsMature() => CurrentStayTime >= TotalStayTime;
    }
    [SerializeField] AnimalData data;
    #endregion 
    public void onUserInput(TouchArgs e)
    {
        CancelInvoke("Live");
        MoveToTouchPos(e);

        if (e.isTouchEnd)
        {
            if (TryAttachToClosestTree())
                Invoke("Live", 1);
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
        Vector3 pos = this.transform.position;
        pos.z--;
        PlayerProgress.I.SpawnCurrency(pos, data.LeafRewardAmount);
        Destroy(gameObject);
    }
}