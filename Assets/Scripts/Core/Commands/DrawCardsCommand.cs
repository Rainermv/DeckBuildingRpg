using Assets.Scripts.Core.Model;
using Assets.Scripts.Core.Model.Card;
using Assets.Scripts.Core.Model.Card.Collections;
using Assets.Scripts.Core.Model.Command;
using Assets.Scripts.Core.Utility;

namespace Assets.Scripts.Core.Commands
{
    internal class DrawCardsCommand : ICardCommand
    {
        private readonly CardCollectionModel _fromCollectionModel;
        private readonly CardCollectionModel _toCollectionModel;
        private readonly int _cardsToDraw;

        public DrawCardsCommand(CardCollectionModel fromCollectionModel, CardCollectionModel toCollectionModel, int cardsToDraw)
        {
            _fromCollectionModel = fromCollectionModel;
            _toCollectionModel = toCollectionModel;
            _cardsToDraw = cardsToDraw;
        }

        public CardCommandReport Run(CardModel sourceCardModel, BattleModel battleModel)
        {
            if (sourceCardModel.CardCollectionModelParent.PlayerParent.CardCollections == null)
            {
                return new CardCommandReport(CardCommandStatus.Failed);

            }

            CardUtilities.DrawCards(
                _fromCollectionModel,
                _toCollectionModel,
                    _cardsToDraw);

            return new CardCommandReport(CardCommandStatus.Success);

        }

        public string Text => $"Draw {_cardsToDraw} Cards from {_fromCollectionModel.CollectionIdentifier.ToString()}";
    }
}