namespace Assets.Scripts.Model.CardModel.Commands
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