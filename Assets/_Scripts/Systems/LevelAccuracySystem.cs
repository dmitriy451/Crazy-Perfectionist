using RH.Utilities.ComponentSystem;

public class LevelAccuracySystem : BaseSystem
{
    public override void Dispose()
    {
        GlobalEvents.AddLevelAccuracy.RemoveListener(AddAccuracy);
    }

    protected override void Init()
    {
        GlobalEvents.AddLevelAccuracy.AddListener(AddAccuracy);
    }

    private void AddAccuracy(int _count)
    {
        DataManager.Instance.mainData.LevelAccuracy += _count;
        GlobalEvents.LevelAccuracyAdded?.Invoke(_count);
    }
}