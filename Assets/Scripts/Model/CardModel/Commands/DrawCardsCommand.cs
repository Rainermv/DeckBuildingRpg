using Assets.Scripts.Model.CardModel.Collections;
using Assets.Scripts.Systems.CardSystem.Utility;

namespace Assets.Scripts.Model.CardModel.Commands
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

        public CardCommandReport Run(Card sourceCard, LevelModel levelModel)
        {
            if (sourceCard.CardCollectionParent.PlayerParent.CardCollections == null)
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