using MoreMountains.NiceVibrations;
using RH.Utilities.UI;

public class CloseLevelCompletePanelButton : BaseActionButton
{
    protected override void Init()
    {
    }

    protected override void PerformOnClick()
    {
        MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        animator.SetTrigger("OnClick");
        UIEvents.LevelCompletePanelShow?.Invoke(false);
        UIEvents.CloseLevelCompletePanelButtonTap?.Invoke();
    }
}