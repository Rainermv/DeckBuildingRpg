using System;
using System.Collections.Generic;
using Assets.Scripts.Constants;
using Assets.Scripts.Controller;
using Assets.Scripts.Controller.Factories;
using Assets.Scripts.Model.Actor;
using Assets.Scripts.Model.AttributeModel;
using Assets.Scripts.Model.Card;
using Assets.Scripts.Model.Card.Collections;
using Assets.Scripts.Model.Card.Commands;
using Assets.Scripts.Model.GridMap;
using Assets.Scripts.Utility;

namespace Assets.Scripts.Model
{
    internal class LevelModelFactory
    {
        public static LevelModel Build(GridMapModel gridMapModel)
        {
            var levelModel = new LevelModel()
            {
                GridMapModel = gridMapModel,
                GlobalAttributeSet = new AttributeSet(),
                Players = CardSystemModelFactory.BuildPlayers(1),
                Entities = new List<Entity>()
            };

            levelModel.GlobalAttributeSet.Set(AttributeKey.Health, 100);

            int i = 0;
            foreach (var player in levelModel.Players.Values)
            {
                SetupPlayer(player);
                AddCharacterTo(player, levelModel, levelModel.GridMapModel.GridTiles[i++].GridPosition);
                
                // Add cards to Deck
                foreach (var card in player.CardCollections[CardCollectionIdentifier.Deck].Cards)
                {
                    SetupCard(card, player);
                }

                // Draw initial hand
                CardUtilities.DrawCards(player.CardCollections[CardCollectionIdentifier.Deck],
                    player.CardCollections[CardCollectionIdentifier.Hand],
                    5);
            }

            return levelModel;
        }
        
        

        private static void AddCharacterTo(Player player, LevelModel levelModel,
            GridPosition gridPosition)
        {
            var character = Entity.Make(player.Name, gridPosition, player);

            levelModel.Entities.Add(character);
        }


        private static void SetupPlayer(Player player)
        {
            player.AttributeSet.Set(AttributeKey.Power, 5);


        }

        private static void SetupCard(CardModel cardModel, Player player)
        {
            var random = new Random();

            var possibleCardTypes = new[] { CardTypes.DRAW, CardTypes.ATTACK, CardTypes.POWER };

            var cardType = random.Next(0, possibleCardTypes.Length);
            var powerCost = 0;
            cardModel.ImageIndex = cardType;

            cardModel.AttributeSet.Set(AttributeKey.CardType, cardType);

            switch (cardType)
            {
                case CardTypes.DRAW:
                    var cardsToDraw = random.Next(1, 4); //1 to 3
                    powerCost = cardsToDraw * 2;

                    cardModel.Name = $"Insight";


                    cardModel.Commands.Add(
                        new DrawCardsCommand(
                            player.CardCollections[CardCollectionIdentifier.Deck],
                            player.CardCollections[CardCollectionIdentifier.Hand],
                            cardsToDraw));
                    break;

                case CardTypes.ATTACK: // Attack
                    powerCost = random.Next(1, 6);

                    cardModel.Name = $"Sword Attack";

                    cardModel.Commands.Add(
                        new SumGlobalAttributeCommand(AttributeKey.Health, -powerCost * 2));
                    break;

                case CardTypes.POWER: // Attack
                    var powerGenerated = 3;
                    cardModel.Name = $"Empower";

                    cardModel.Commands.Add(
                        new SumAttributeCommand(player, AttributeKey.Power, powerGenerated));
                    break;

            }

            cardModel.AttributeSet.Set(AttributeKey.PowerCost, powerCost);
        }

    }
}