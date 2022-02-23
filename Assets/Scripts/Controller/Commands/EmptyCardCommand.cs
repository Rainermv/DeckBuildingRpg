using Assets.Scripts.Core.Model;
using Assets.Scripts.Core.Model.Card;
using Assets.Scripts.Core.Model.Command;

namespace Assets.Scripts.Core.Commands
{
    public class EmptyCardCommand : ICardCommand
    {
        public CardCommandReport Run(CardModel sourceCardModel, BattleModel battleModel)
        {
            return new CardCommandReport(CardCommandStatus.Success);
        }

        public string Text => string.Empty;
    }
}