using RH.Utilities.ComponentSystem;

public class MoneySystem : BaseSystem
{
    public override void Dispose()
    {
        GlobalEvents.AddMoney.RemoveListener(AddMoney);
    }

    protected override void Init()
    {
        GlobalEvents.AddMoney.AddListener(AddMoney);
        UIEvents.LevelCompleteGetRewardButtonTap.AddListener(() =>
        {
            AddMoney(DataManager.Instance.balanceData.BaseRewardForLevel * 3);
        });
        UIEvents.CloseLevelCompletePanelButtonTap.AddListener(() =>
        {
            AddMoney(DataManager.Instance.balanceData.BaseRewardForLevel);
        });
    }

    private void AddMoney(int _count)
    {
        if (DataManager.Instance.mainData.Money + _count < 0) // NotEnough
            return;

        if (_count > 0 &&
            DataManager.Instance.mainData.Money + _count > DataManager.Instance.balanceData.MoneyMaxCap) // Max Cap
        {
            DataManager.Instance.mainData.Money = DataManager.Instance.balanceData.MoneyMaxCap;
            GlobalEvents.MoneyAdded?.Invoke(_count);
            return;
        }

        DataManager.Instance.mainData.Money += _count;
        GlobalEvents.MoneyAdded?.Invoke(_count);
    }
}