using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupTextSpawner : MonoBehaviour
{
    [SerializeField] GameObject popupPrefab;
    [SerializeField] float popupDuration = 2f;


    public static PopupTextSpawner I;
    private void Awake()
    {
        if(I == null)
            I = this;
        else
            Destroy(gameObject);
    }
    public void ShowPopup(string msg, Vector2 pos)
    {
        GameObject popup = Instantiate(popupPrefab, pos, Quaternion.identity);
        popup.GetComponent<TextMeshProUGUI>().text = msg;

        LeanTween.moveLocalY(popup, 0.5f, popupDuration).setEase(LeanTweenType.easeOutExpo).setDestroyOnComplete(true);
    }

}
