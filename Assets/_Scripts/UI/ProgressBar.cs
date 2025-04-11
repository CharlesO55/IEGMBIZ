using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{
    [SerializeField] Image progressFill;

    public void UpdateFill(float percent)
    {
        progressFill.fillAmount = percent;
    }
}
