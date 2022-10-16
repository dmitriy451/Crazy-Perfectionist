using MoreMountains.NiceVibrations;
using RH.Utilities.UI;

public class LevelCompleteGetRewardButton : BaseActionButton
{
    protected override void Init()
    {
        UIEvents.LevelCompleteGetRewardButtonShow.AddListener(SetShow);
        UIEvents.LevelCompleteGetRewardButtonShow.AddListener(x => { UpdateButtonStatus(); });
    }

    protected override void PerformOnClick()
    {
        MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        animator.SetTrigger("OnClick");
        UIEvents.LevelCompleteGetRewardButtonTap?.Invoke();
        UIEvents.LevelCompletePanelShow?.Invoke(false);
    }

    private void UpdateButtonStatus()
    {
        //_button.interactable = 
    }
}