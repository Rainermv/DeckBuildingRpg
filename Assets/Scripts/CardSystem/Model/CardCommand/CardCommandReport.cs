namespace Assets.Scripts.CardSystem.Model.CardCommand
{
    public class CardCommandReport
    {
        public CardCommandReport(CardCommandStatus cardCommandStatus)
        {
            CardCommandStatus = cardCommandStatus;
        }

        public CardCommandStatus CardCommandStatus { get; set; }
    }
}