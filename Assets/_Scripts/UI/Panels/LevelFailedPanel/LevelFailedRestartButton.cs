using MoreMountains.NiceVibrations;
using RH.Utilities.UI;

public class LevelFailedRestartButton : BaseActionButton
{
    protected override void Init()
    {
    }

    protected override void PerformOnClick()
    {
        MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        animator.SetTrigger("OnClick");
        UIEvents.RestartButtonTap?.Invoke();
        UIEvents.LevelFailedPanelShow?.Invoke(false);
        UIEvents.LevelCompletePanelShow?.Invoke(false);
    }

    private void UpdateButtonStatus()
    {
        //_button.interactable = 
    }
}