using UnityEngine.Events;

public static class UIEvents
{
    //VibrationButton
    public static UnityEvent VibrationButtonTap = new();
    public static UnityEvent<bool> VibrationButtonShow = new();

    //MoneyText
    public static UnityEvent<string> ChangeMoneyText = new();

    //LevelText
    public static UnityEvent<string> ChangeLevelText = new();

    //LevelProgressBar
    public static UnityEvent<float> UpdateLevelProgressBar = new();

    //LevelCompletePanelShow
    public static UnityEvent<bool> LevelCompletePanelShow = new();
    public static UnityEvent<string> ChangeLevelCompleteMoneyText = new();
    public static UnityEvent<string> ChangeLevelAccuracyText = new();
    public static UnityEvent LevelCompleteGetRewardButtonTap = new();
    public static UnityEvent CloseLevelCompletePanelButtonTap = new();
    public static UnityEvent<bool> LevelCompleteGetRewardButtonShow = new();

    //LevelFailedPanelShow
    public static UnityEvent<bool> LevelFailedPanelShow = new();
    public static UnityEvent<bool> OpenLevelFailedPanelButtonShow = new();
    public static UnityEvent RestartButtonTap = new();

    //ObjectButtonTap
    public static UnityEvent ObjectButtonTap = new();

    //Commendable
    public static UnityEvent ShowCommend = new();

    //Tutorial
    public static UnityEvent<bool> TutorialTapToFixTextShow = new();
    public static UnityEvent<bool> TutorialTapOnTimeTextShow = new();
}