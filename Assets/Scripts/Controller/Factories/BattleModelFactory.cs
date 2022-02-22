using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Constants;
using Assets.Scripts.Core.Model;
using Assets.Scripts.Core.Model.AttributeModel;
using Assets.Scripts.Core.Model.Card;
using Assets.Scripts.Core.Model.Card.Collections;
using Assets.Scripts.Core.Model.Entity;
using Assets.Scripts.Core.Model.GridMap;
using Assets.Scripts.Core.Utility;

namespace Assets.Scripts.Controller.Factories
{
    public class BattleModelFactory
    {
        public static BattleModel Build(GridMapModel gridMapModel)
        {
            var levelModel = new BattleModel()
            {
                GridMapModel = gridMapModel,
                GlobalAttributeSet = new AttributeSet(),
                Players = CardSystemModelFactory.BuildPlayers(1),
                Entities = new List<BattleEntity>()
            };

            levelModel.GlobalAttributeSet.Set(AttributeKey.Health, 100);

            //int i = 0;
            foreach (var player in levelModel.Players.Values)
            {
                SetupPlayer(player);
                AddCharacterTo(player, levelModel, levelModel.GridMapModel.GridTiles[0].GridPosition);
                AddCharacterTo(player, levelModel, levelModel.GridMapModel.GridTiles[5].GridPosition);
                AddCharacterTo(player, levelModel, levelModel.GridMapModel.GridTiles[15].GridPosition);
                AddCharacterTo(player, levelModel, levelModel.GridMapModel.GridTiles[20].GridPosition);
                AddCharacterTo(player, levelModel, levelModel.GridMapModel.GridTiles[25].GridPosition);

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
        
        

        private static void AddCharacterTo(Player player, BattleModel battleModel,
            GridPosition gridPosition)
        {
            var character = BattleEntity.Make(player.Name, gridPosition, player);
            character.MovementRange = 10;
            battleModel.Entities.Add(character);
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

                    /*
                    cardModel.Commands.Add(
                        new DrawCardsCommand(
                            player.CardCollections[CardCollectionIdentifier.Deck],
                            player.CardCollections[CardCollectionIdentifier.Hand],
                            cardsToDraw));
                    */
                    break;

                case CardTypes.ATTACK: // Attack
                    powerCost = random.Next(1, 6);

                    cardModel.Name = $"Sword Attack";
                    /*
                    cardModel.Commands.Add(
                        new SumGlobalAttributeCommand(AttributeKey.Health, -powerCost * 2));
                    */
                    break;

                case CardTypes.POWER: // Attack
                    var powerGenerated = 3;
                    cardModel.Name = $"Empower";

                    /*
                    cardModel.Commands.Add(
                        new SumAttributeCommand(player, AttributeKey.Power, powerGenerated));
                    */
                    break;

            }

            cardModel.AttributeSet.Set(AttributeKey.PowerCost, powerCost);
        }

    }
}