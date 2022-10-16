public class MoneyText : BaseText
{
    protected override void Init()
    {
        UIEvents.ChangeMoneyText.AddListener(UpdateText);
    }
}