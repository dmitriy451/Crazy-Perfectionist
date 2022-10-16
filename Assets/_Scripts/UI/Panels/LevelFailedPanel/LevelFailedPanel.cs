public class LevelFailedPanel : BasePanel
{
    protected override void Init()
    {
        UIEvents.LevelFailedPanelShow.AddListener(SetShow);
    }
}