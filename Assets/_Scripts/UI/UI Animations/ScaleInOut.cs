using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct AnimParams
{
    public RectTransform transform;
    public float animTime;
    public LeanTweenType animType;

    public Vector2 hideVec;
    
    public AnimParams(RectTransform t, Vector2 hide, float time = 0.5f,  LeanTweenType type = LeanTweenType.easeInOutCirc)
    {
        transform = t;
        animTime = time;
        animType = type;

        hideVec = hide;
    }
}

public class ScaleInOut : MonoBehaviour
{
    [SerializeField] AnimParams[] anims;

    public void AnimIn()
    {
        foreach (AnimParams a in anims)
        {
            a.transform.localScale = a.hideVec;
            LeanTween.scale(a.transform, Vector2.one, a.animTime).
                setEase(a.animType);
        }
    }

    public void AnimOut()
    {
        foreach (AnimParams a in anims)
        {
            LeanTween.scale(a.transform, a.hideVec, a.animTime).
                setEase(a.animType).
                setOnComplete(Disable);
        }
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
}