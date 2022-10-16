using RH.Utilities.ComponentSystem;

public class CommendSystem : BaseSystem
{
    public override void Dispose()
    {
        GlobalEvents.OnObjectComplete.RemoveListener(ShowCommend);
    }

    protected override void Init()
    {
        GlobalEvents.OnObjectComplete.AddListener(ShowCommend);
    }

    private void ShowCommend(int _accuracy)
    {
        if (_accuracy > DataManager.Instance.balanceData.ThresholdForPassingLevel)
            UIEvents.ShowCommend.Invoke();
    }
}