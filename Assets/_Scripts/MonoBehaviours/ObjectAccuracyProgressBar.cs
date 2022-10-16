using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(NotPerfectObject))]
public class ObjectAccuracyProgressBar : MonoBehaviour
{
    [SerializeField] private Image accuracyBarImage;
    [SerializeField] private TextMeshProUGUI accuracyPercentText;
    [SerializeField] private Color correctColor;
    [SerializeField] private Color wrongColor;
    private Image accuracyBarFillImage;

    private void Awake()
    {
        accuracyBarFillImage = accuracyBarImage.GetComponentInChildren<Image>();
        var _object = GetComponent<NotPerfectObject>();
        _object.OnInitObject.AddListener(Init);
        _object.OnStartObject.AddListener(Started);
        _object.OnPreCompleteObject.AddListener(FillAccuracy);
        //_object.OnCompleteObject.AddListener(Landing);
    }

    private void Init(GameObject _thisObject)
    {
        ImageTrigger(false);
        accuracyPercentText.gameObject.SetActive(false);
    }

    private void Started()
    {
        ImageTrigger(true);
    }

    private void Landing(int _accuracy)
    {
        ImageTrigger(false);
    }

    private void ImageTrigger(bool _isOn)
    {
        accuracyBarImage.gameObject.SetActive(_isOn);
        accuracyBarFillImage.gameObject.SetActive(_isOn);
    }

    private void FillAccuracy(int _accuracy)
    {
        accuracyPercentText.text = $"{_accuracy}%";
        DOTween.To(() => accuracyBarFillImage.fillAmount, x => accuracyBarFillImage.fillAmount = x, _accuracy / 100.0f,
            0.1f);
        accuracyBarFillImage.color = _accuracy > DataManager.Instance.balanceData.ThresholdForPassingLevel
            ? correctColor
            : wrongColor;
        accuracyPercentText.gameObject.SetActive(true);
    }
}