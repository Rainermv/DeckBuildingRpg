namespace Assets.Scripts.CardSystem.Model.Command
{
    internal class SwitchTypeCommand : ICardCommand
    {
        private readonly Card _affectedCard;

        public SwitchTypeCommand(Card affectedCard)
        {
            _affectedCard = affectedCard;
        }

        public CardCommandReport Run(Card sourceCard, GameContext gameContext)
        {
            // do nothing for now
            /*
            switch (_affectedCard.Resources[CardResourceNames.POWER_EFFECT_TYPE].Value)
            {
                case 1: 
                    _affectedCard. = 2;
                    break;

                case 2:
                    _affectedCard.CardType = 1;
                    break;

                default:
                    _affectedCard.CardType = 0;
                    break;
            }
            */

            return new CardCommandReport(CardCommandStatus.Success);
        }
    }
}