public class LevelCompleteMoneyText : BaseText
{
    protected override void Init()
    {
        UIEvents.ChangeLevelCompleteMoneyText.AddListener(UpdateText);
    }
}