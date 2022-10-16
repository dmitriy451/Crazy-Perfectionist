using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BalanceData", menuName = "GameData/BalanceData")]
public class BalanceData : BaseData
{
    public int MoneyMaxCap;
    public int BaseRewardForLevel;
    public float MovingSpeed;
    public float RotatingSmoothSpeed;
    public float RotatingStepSpeed;
    public float PickUpTime;
    public int RotatingDegree;
    public int PermissibleErrorInAccuracy;
    public int ThresholdForPassingLevel;

    public override void ResetData()
    {
        throw new NotImplementedException();
    }
}