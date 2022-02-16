using Assets.Scripts.CardSystem.Models.Collections;
using Assets.Scripts.CardSystem.Utility;

namespace Assets.Scripts.CardSystem.Models.Commands
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

        public CardCommandReport Run(Card sourceCard, GameModel gameModel)
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

        public string Text => $"Draw {_cardsToDraw} Cards from {_fromCollection.CollectionIdentifier.ToString()}";
    }
}