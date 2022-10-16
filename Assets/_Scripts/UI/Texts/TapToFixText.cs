public class TapToFixText : BaseText
{
    protected override void Init()
    {
        UIEvents.TutorialTapToFixTextShow.AddListener(SetShow);
    }
}