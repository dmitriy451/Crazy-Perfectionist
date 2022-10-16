using MoreMountains.NiceVibrations;
using RH.Utilities.UI;
using UnityEngine;
using UnityEngine.UI;

public class VibrationButton : BaseActionButton
{
    public Sprite vibrationOnSprite;
    public Sprite vibrationOffSprite;
    private Image vibractionStateImage;

    protected override void Init()
    {
        UIEvents.VibrationButtonShow.AddListener(SetShow);
    }

    protected override void PerformOnClick()
    {
        MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        animator.SetTrigger("OnClick");
        UIEvents.VibrationButtonTap?.Invoke();

        vibractionStateImage.sprite =
            DataManager.Instance.settingsData.IsVibrationOn ? vibrationOnSprite : vibrationOffSprite;
    }
}