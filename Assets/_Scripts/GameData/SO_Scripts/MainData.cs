using UnityEngine;

[CreateAssetMenu(fileName = "MainData", menuName = "GameData/MainData")]
public class MainData : BaseData
{
    public int LevelNumber;
    public int Money;
    public int LevelAccuracy;

    public bool TutorialTapToFixCompleted;
    public bool TutorialTapOnTimeCompleted;

    public override void ResetData()
    {
        LevelNumber = 0;
        Money = 0;
        Money = 0;
        LevelAccuracy = 0;
        TutorialTapToFixCompleted = false;
        TutorialTapOnTimeCompleted = false;
    }
}