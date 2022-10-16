using RH.Utilities.ComponentSystem;
using UnityEngine;

public class VibrationSystem : BaseSystem
{
    protected override void Init()
    {
        UIEvents.VibrationButtonTap.AddListener(SetVibrationState);
    }

    public override void Dispose()
    {
        UIEvents.VibrationButtonTap.RemoveListener(SetVibrationState);
    }

    private void SetVibrationState()
    {
        DataManager.Instance.settingsData.IsVibrationOn = !DataManager.Instance.settingsData.IsVibrationOn;
        Handheld.Vibrate();
    }
}