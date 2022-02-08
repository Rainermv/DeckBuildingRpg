namespace Assets.Scripts.CardSystem.Model.Command
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