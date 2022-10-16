using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingsData", menuName = "GameData/SettingsData")]
public class SettingsData : BaseData
{
    public bool IsVibrationOn;

    public override void ResetData()
    {
        throw new NotImplementedException();
    }
}