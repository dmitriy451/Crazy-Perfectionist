public class LevelCompletePanel : BasePanel
{
    protected override void Init()
    {
        UIEvents.LevelCompletePanelShow.AddListener(SetShow);
        UIEvents.LevelCompletePanelShow.AddListener(x => { UpdateInfo(); });
    }

    private void UpdateInfo()
    {
        UIEvents.ChangeLevelCompleteMoneyText?.Invoke($"{DataManager.Instance.balanceData.BaseRewardForLevel}");
    }
}