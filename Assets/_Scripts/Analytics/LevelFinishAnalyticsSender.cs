//using GameAnalyticsSDK;

using RH.Utilities.ComponentSystem;

public class LevelFinishAnalyticsSender : BaseSystem
{
    protected override void Init()
    {
    }


    public override void Dispose()
    {
    }

    private void SendLevelFinishLog()
    {
    }
    // => GameAnalytics.NewDesignEvent("Level win", GameData.Instance.CreateAnalyticsDictionary());
}