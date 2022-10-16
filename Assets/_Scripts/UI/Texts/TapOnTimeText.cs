public class TapOnTimeText : BaseText
{
    protected override void Init()
    {
        UIEvents.TutorialTapOnTimeTextShow.AddListener(SetShow);
    }
}