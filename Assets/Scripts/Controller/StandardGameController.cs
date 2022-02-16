using System;
using Assets.Scripts.CardSystem.Constants;
using Assets.Scripts.CardSystem.Models;
using Assets.Scripts.CardSystem.Models.Attributes;
using Assets.Scripts.CardSystem.Models.Collections;
using Assets.Scripts.CardSystem.Models.Commands;
using Assets.Scripts.CardSystem.Utility;

namespace Assets.Scripts.Ruleset
{
    internal class StandardGameController : IGameController
    {
        private readonly ICardShuffler _cardShuffler;

        private GameModel _gameModel;

        public StandardGameController(ICardShuffler cardShuffler)
        {
            _cardShuffler = cardShuffler;
        }

        public GameModel SetupWithSettings(GameControllerSettings gameControllerSettings)
        {
            _gameModel = new GameModel()
            {
                CardSystemModel = CardSystemModelFactory.Build(),
                GridSystemModel = GridSystemModelFactory.Build(gameControllerSettings.MapWidth, gameControllerSettings.MapHeight),
                GlobalAttributeSet = new AttributeSet()
            };

            _gameModel.GlobalAttributeSet.Set(AttributeKey.Health, 100);

            foreach (var cardPlayer in _gameModel.CardSystemModel.CardPlayers.Values)
            {
                SetupPlayer(cardPlayer);

                foreach (var cardCollection in cardPlayer.CardCollections.Values)
                {
                    SetupCollection(cardCollection);
                }

                // Add cards to Deck
                foreach (var card in cardPlayer.CardCollections[CardCollectionIdentifier.Deck].Cards)
                {
                    SetupCard(card, cardPlayer);
                }

                // Draw initial hand
                CardService.DrawCards(cardPlayer.CardCollections[CardCollectionIdentifier.Deck],
                    cardPlayer.CardCollections[CardCollectionIdentifier.Hand],
                    5);
            }

            return _gameModel;

        }

       
        private void SetupPlayer(CardPlayer cardPlayer)
        {
            cardPlayer.AttributeSet.Set(AttributeKey.Power, 5);

        }

        private void SetupCard(Card card, CardPlayer cardPlayer)
        {
            var random = new Random();

            var possibleCardTypes = new[] { CardTypes.DRAW, CardTypes.ATTACK, CardTypes.POWER };

            var cardType = random.Next(0, possibleCardTypes.Length);
            var powerCost = 0;
            card.ImageIndex = cardType;

            card.AttributeSet.Set(AttributeKey.CardType, cardType);

            switch (cardType)
            {
                case CardTypes.DRAW:
                    var cardsToDraw = random.Next(1, 4); //1 to 3
                    powerCost = cardsToDraw * 2;

                    card.Name = $"Insight";


                    card.Commands.Add(
                        new DrawCardsCommand(
                            cardPlayer.CardCollections[CardCollectionIdentifier.Deck],
                            cardPlayer.CardCollections[CardCollectionIdentifier.Hand],
                            cardsToDraw));
                    break;

                case CardTypes.ATTACK: // Attack
                    powerCost = random.Next(1, 6);

                    card.Name = $"Sword Attack";

                    card.Commands.Add(
                        new SumGlobalAttributeCommand(AttributeKey.Health, -powerCost * 2));
                    break;

                case CardTypes.POWER: // Attack
                    var powerGenerated = 3;
                    card.Name = $"Empower";

                    card.Commands.Add(
                        new SumAttributeCommand(cardPlayer.AttributeSet, AttributeKey.Power, powerGenerated));
                    break;

            }

            card.AttributeSet.Set(AttributeKey.PowerCost, powerCost);
        }


        public void SetupCollection(CardCollection cardCollection)
        {
            switch (cardCollection.CollectionIdentifier)
            {
                case CardCollectionIdentifier.Deck:

                    cardCollection.Cards = _cardShuffler.Run(
                        cardCollection.Cards);

                    break;
                case CardCollectionIdentifier.Hand:
                    break;
                case CardCollectionIdentifier.Discard:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }
        
        public async void OnCardClicked(Card card)
        {
            var player = card.CardCollectionParent.CardPlayerParent;

            switch (card.CardCollectionParent.CollectionIdentifier)
            {
                case CardCollectionIdentifier.Hand:

                    //if (player.AttributeSet.GetValue(PlayerAttributeNames.Power))

                    card.Play(_gameModel);
                    CardService.MoveCardTo(card, player.CardCollections[CardCollectionIdentifier.Discard]);
                    break;

            }
        }

        public void OnCardCollectionClicked(CardCollection cardCollection)
        {
            if (cardCollection.CollectionIdentifier == CardCollectionIdentifier.Deck)
            {
                CardService.DrawCards(cardCollection, cardCollection.CardPlayerParent.CardCollections[CardCollectionIdentifier.Hand], 1);
            }
        }
    }

    public enum AttributeKey
    {
        Power,
        Health,
        CardType,
        PowerCost
    }
}