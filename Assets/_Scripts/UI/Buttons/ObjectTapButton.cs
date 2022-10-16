using MoreMountains.NiceVibrations;
using RH.Utilities.UI;

public class ObjectTapButton : BaseActionButton
{
    protected override void Init()
    {
    }

    protected override void PerformOnClick()
    {
        MMVibrationManager.Haptic(HapticTypes.LightImpact);
        UIEvents.ObjectButtonTap?.Invoke();
    }
}