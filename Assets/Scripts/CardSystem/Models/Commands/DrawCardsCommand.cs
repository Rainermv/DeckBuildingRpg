using System.Threading.Tasks;
using Assets.Scripts.CardSystem.Model;
using Assets.Scripts.CardSystem.Model.Collection;
using Assets.Scripts.CardSystem.Model.Command;

namespace Assets.Scripts.CardSystem
{
    internal class DrawCardsCommand : ICardCommand
    {
        private readonly CardCollection _fromCollection;
        private readonly CardCollection _toCollection;
        private readonly int _cardsToDraw;

        public DrawCardsCommand(CardCollection fromCollection, CardCollection toCollection, int cardsToDraw)
        {
            _fromCollection = fromCollection;
            _toCollection = toCollection;
            _cardsToDraw = cardsToDraw;
        }

        public CardCommandReport Run(Card sourceCard, GameContext gameContext)
        {
            if (sourceCard.CardCollectionParent.CardPlayerParent.CardCollections == null)
            {
                return new CardCommandReport(CardCommandStatus.Failed);

            }

            CardService.DrawCards(
                _fromCollection,
                _toCollection,
                    _cardsToDraw);

            return new CardCommandReport(CardCommandStatus.Success);

        }
    }
}