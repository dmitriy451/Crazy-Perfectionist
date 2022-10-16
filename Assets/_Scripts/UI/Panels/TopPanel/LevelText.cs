public class LevelText : BaseText
{
    protected override void Init()
    {
        UIEvents.ChangeLevelText.AddListener(UpdateText);
    }
}