using System;
using Assets.Scripts.CardSystem;
using Assets.Scripts.CardSystem.Constants;
using Assets.Scripts.CardSystem.Model;
using Assets.Scripts.CardSystem.Model.Collection;

namespace Assets.Scripts
{
    internal class StandardGameRuleset : IGameRuleset
    {
        private readonly ICardShuffler _cardShuffler;

        private GameContext _gameContext;

        public StandardGameRuleset(ICardShuffler cardShuffler)
        {
            _cardShuffler = cardShuffler;
        }

        public void Setup(GameContext gameContext)
        {
            _gameContext = gameContext;
            gameContext.GlobalAttributeSet.Set(GlobalAttributeNames.ENEMY_HEALTH, 100);

            foreach (var cardPlayer in gameContext.CardSystemModel.CardPlayers.Values)
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


        }

       
        private void SetupPlayer(CardPlayer cardPlayer)
        {
            cardPlayer.AttributeSet.Set(PlayerAttributeNames.Power, 5);

        }

        private void SetupCard(Card card, CardPlayer cardPlayer)
        {
            var random = new Random();

            var possibleCardTypes = new[] { CardTypes.DRAW, CardTypes.ATTACK, CardTypes.POWER };

            var cardType = random.Next(0, possibleCardTypes.Length);
            var powerCost = 0;

            card.AttributeSet.Set(CardAttributeNames.TYPE, cardType);

            switch (cardType)
            {
                case CardTypes.DRAW:
                    var cardsToDraw = random.Next(1, 4); //1 to 3
                    powerCost = cardsToDraw * 2;

                    card.Name = $"Draw {cardsToDraw} ({powerCost})";

                    card.Commands.Add(
                        new DrawCardsCommand(
                            cardPlayer.CardCollections[CardCollectionIdentifier.Deck],
                            cardPlayer.CardCollections[CardCollectionIdentifier.Hand],
                            cardsToDraw));
                    break;

                case CardTypes.ATTACK: // Attack
                    powerCost = card.AttributeSet.Set(CardAttributeNames.POWER_COST, random.Next(1, 6)).Value;
                    var attackValue = powerCost * 2;

                    card.Name = $"Attack {attackValue} ({powerCost})";

                    card.Commands.Add(
                        new SumGlobalAttributeCommand(GlobalAttributeNames.ENEMY_HEALTH, -attackValue));
                    break;

                case CardTypes.POWER: // Attack
                    var powerGenerated = 3;
                    card.Name = $"Power {powerGenerated}";

                    card.Commands.Add(
                        new SumAttributeCommand(cardPlayer.AttributeSet, PlayerAttributeNames.Power, powerGenerated));
                    break;

            }

            card.AttributeSet.Set(CardAttributeNames.POWER_COST, powerCost);
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

                    card.Play(_gameContext);
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
}