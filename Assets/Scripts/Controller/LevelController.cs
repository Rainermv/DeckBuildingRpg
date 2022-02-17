using System;
using System.Linq;
using Assets.Scripts.CardSystem.Models.Attributes;
using Assets.Scripts.Controller.Factories;
using Assets.Scripts.Model;
using Assets.Scripts.Model.CardModel;
using Assets.Scripts.Model.CardModel.Collections;
using Assets.Scripts.Model.CardModel.Commands;
using Assets.Scripts.Model.CharacterModel;
using Assets.Scripts.Model.GridModel;
using Assets.Scripts.Systems.CardSystem.Constants;
using Assets.Scripts.Systems.CardSystem.Utility;
using Assets.Scripts.Systems.GridSystem;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Controller
{
    internal class LevelController
    {
        private readonly ICardShuffler _cardShuffler;

        private LevelModel _levelModel;

        public LevelController(ICardShuffler cardShuffler)
        {
            _cardShuffler = cardShuffler;

        }

        

        public LevelModel SetupWithSettings(GameControllerSettings gameControllerSettings)
        {
            _levelModel = new LevelModel()
            {
                Players = CardSystemModelFactory.Build(),
                GridTileModel = GridSystemModelFactory.Build(gameControllerSettings.MapWidth, gameControllerSettings.MapHeight),
                GlobalAttributeSet = new AttributeSet()
            };

            _levelModel.GlobalAttributeSet.Set(AttributeKey.Health, 100);

            int pIndex = 0;
            foreach (var player in _levelModel.Players.Values)
            {
                SetupPlayer(player);
                AddCharacterTo(player, gameControllerSettings.CharacterPositions[pIndex++]);


                foreach (var cardCollection in player.CardCollections.Values)
                {
                    SetupCollection(cardCollection);
                }

                // Add cards to Deck
                foreach (var card in player.CardCollections[CardCollectionIdentifier.Deck].Cards)
                {
                    SetupCard(card, player);
                }

                // Draw initial hand
                CardService.DrawCards(player.CardCollections[CardCollectionIdentifier.Deck],
                    player.CardCollections[CardCollectionIdentifier.Hand],
                    5);
            }

            return _levelModel;

        }

        private void AddCharacterTo(Player player, GridPosition characterPosition)
        {
            var character = Character.Make(player.Name, characterPosition, player);

            _levelModel.Characters.Add(character);
        }


        private void SetupPlayer(Player player)
        {
            player.AttributeSet.Set(AttributeKey.Power, 5);

            
        }

        private void SetupCard(Card card, Player player)
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
                            player.CardCollections[CardCollectionIdentifier.Deck],
                            player.CardCollections[CardCollectionIdentifier.Hand],
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
                        new SumAttributeCommand(player, AttributeKey.Power, powerGenerated));
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

        public void OnGridPositionInput(GridPosition moveGridPosition)
        {
            if (
                !InsideBounds(_levelModel.GridTileModel, moveGridPosition) ||
                _levelModel.Characters.Any(character => character.GridPosition == moveGridPosition))
            {
                Debug.Log($"Failed to move to {moveGridPosition}");
                return;
            }
            
            _levelModel.Characters[0].GridPosition = moveGridPosition;

        }

        private static bool InsideBounds(GridTileModel gridTileModel, GridPosition gridPosition)
        {
            return new Rect(0, 0, gridTileModel.Width, gridTileModel.Height).Contains(new Vector2(gridPosition.X,
                gridPosition.Y));
        }


        public async void OnCardClicked(Card card)
        {
            var player = card.CardCollectionParent.PlayerParent;

            switch (card.CardCollectionParent.CollectionIdentifier)
            {
                case CardCollectionIdentifier.Hand:

                    //if (player.AttributeSet.GetValue(PlayerAttributeNames.Power))

                    card.Play(_levelModel);
                    CardService.MoveCardTo(card, player.CardCollections[CardCollectionIdentifier.Discard]);
                    break;

            }
        }

        public void OnCardCollectionClicked(CardCollection cardCollection)
        {
            if (cardCollection.CollectionIdentifier == CardCollectionIdentifier.Deck)
            {
                CardService.DrawCards(cardCollection, cardCollection.PlayerParent.CardCollections[CardCollectionIdentifier.Hand], 1);
            }
        }
    }

    public static class AttributeKey
    {
        public const int Power = 0;
        public const int Health = 1;
        public const int CardType = 2;
        public const int PowerCost = 3;
    }
}