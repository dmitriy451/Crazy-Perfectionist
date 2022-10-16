public class LevelAccuracyText : BaseText
{
    protected override void Init()
    {
        UIEvents.ChangeLevelAccuracyText.AddListener(UpdateText);
    }
}