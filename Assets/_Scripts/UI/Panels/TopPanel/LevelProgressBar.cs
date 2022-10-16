using UnityEngine;
using UnityEngine.UI;

public class LevelProgressBar : MonoBehaviour
{
    private Image progressFillBarImage;

    private void Awake()
    {
        progressFillBarImage = transform.GetChild(0).GetComponent<Image>();
        UIEvents.UpdateLevelProgressBar.AddListener(UpdateFill);
    }

    private void UpdateFill(float _amount)
    {
        progressFillBarImage.fillAmount = _amount;
    }
}