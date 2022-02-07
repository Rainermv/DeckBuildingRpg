namespace Assets.Scripts.CardSystem.Model.CardCommand
{
    internal class SwitchTypeCommand : ICardCommand
    {
        private readonly Card _affectedCard;

        public SwitchTypeCommand(Card affectedCard)
        {
            _affectedCard = affectedCard;
        }

        public CardCommandReport Run()
        {
            switch (_affectedCard.CardType)
            {
                case 1: 
                    _affectedCard.CardType = 2;
                    break;

                case 2:
                    _affectedCard.CardType = 1;
                    break;

                default:
                    _affectedCard.CardType = 0;
                    break;
            }

            return new CardCommandReport(CardCommandStatus.Success);
        }
    }
}