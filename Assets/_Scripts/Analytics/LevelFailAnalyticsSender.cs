//using GameAnalyticsSDK;

using RH.Utilities.ComponentSystem;

public class LevelFailAnalyticsSender : BaseSystem
{
    protected override void Init()
    {
    } // =>=>
    //GlobalEvents.LevelFailed.AddListener(LogLevelFail);

    public override void Dispose()
    {
    } // =>
    //GlobalEvents.LevelFailed.RemoveListener(LogLevelFail);

    private void LogLevelFail()
    {
    }
    //=> GameAnalytics.NewDesignEvent("Level fail", GameData.Instance.CreateAnalyticsDictionary());
}