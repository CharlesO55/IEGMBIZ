using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExitCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counter;
    [SerializeField] private GameObject exitButton;

    [SerializeField] private int timer = 10;

    private void Start()
    {
        this.StartCoroutine(CountdownCoroutine(this.timer));
    }

    private IEnumerator CountdownCoroutine(int duration)
    {
        int remainingTime = duration;

        while (remainingTime > 0)
        {
            counter.text = remainingTime.ToString();
            yield return new WaitForSeconds(1);
            remainingTime--;
        }

        // Countdown has finished
        counter.gameObject.SetActive(false);
        exitButton.SetActive(true);
    }
}
