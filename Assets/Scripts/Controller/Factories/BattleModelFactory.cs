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
        public static CombatModel Build(GridMapModel gridMapModel, List<CardData> cardDataModels)
        {
            var levelModel = new CombatModel()
            {
                GridMapModel = gridMapModel,
                GlobalAttributeSet = new AttributeSet(),
                Players = CardSystemModelFactory.BuildPlayers(1),
                Entities = new List<Entity>()
            };

            foreach (var player in levelModel.Players)
            {
                player.CardCollections[CardCollectionIdentifier.Deck].InsertCards(
                    BuildRandomCards(20, cardDataModels));
            }

            

            //int i = 0;
            foreach (var player in levelModel.Players)
            {
                SetupPlayer(player);
                AddCharacterTo(player, levelModel, levelModel.GridMapModel.GridTiles[0].GridPosition);
                AddCharacterTo(player, levelModel, levelModel.GridMapModel.GridTiles[5].GridPosition);
                AddCharacterTo(player, levelModel, levelModel.GridMapModel.GridTiles[15].GridPosition);
                AddCharacterTo(player, levelModel, levelModel.GridMapModel.GridTiles[16].GridPosition);
                AddCharacterTo(player, levelModel, levelModel.GridMapModel.GridTiles[17].GridPosition);
                AddCharacterTo(player, levelModel, levelModel.GridMapModel.GridTiles[18].GridPosition);
                AddCharacterTo(player, levelModel, levelModel.GridMapModel.GridTiles[19].GridPosition);
                AddCharacterTo(player, levelModel, levelModel.GridMapModel.GridTiles[20].GridPosition);
                AddCharacterTo(player, levelModel, levelModel.GridMapModel.GridTiles[25].GridPosition);
                AddCharacterTo(player, levelModel, levelModel.GridMapModel.GridTiles[35].GridPosition);
                AddCharacterTo(player, levelModel, levelModel.GridMapModel.GridTiles[45].GridPosition);
                AddCharacterTo(player, levelModel, levelModel.GridMapModel.GridTiles[55].GridPosition);



                // Draw initial hand
                CardUtilities.DrawCards(player.CardCollections[CardCollectionIdentifier.Deck],
                    player.CardCollections[CardCollectionIdentifier.Hand],
                    5);
            }

            return levelModel;
        }

        private static List<Card> BuildRandomCards(int numOfCards, List<CardData> cardDataModels)
        {
            var random = new Random();
            
            var cards = new List<Card>();
            for (var i = 0; i < numOfCards; i++)
            {
                var card = Card.MakeFromCardData(cardDataModels[random.Next(0, cardDataModels.Count)]);

                cards.Add(card);
            }

            return cards;
        }

        private static void AddCharacterTo(Player player, CombatModel combatModel,
            GridPosition gridPosition)
        {
            var character = Entity.Make(player.Name, gridPosition, player);
            character.MovementRange = 10;
            combatModel.Entities.Add(character);
        }


        private static void SetupPlayer(Player player)
        {
            player.AttributeSet.Set(AttributeKey.Power, 5);


        }

        private static void SetupCard(Card card, Player player)
        {
            var random = new Random();

            var possibleCardTypes = new[] { CardTypes.DRAW, CardTypes.ATTACK, CardTypes.POWER };

            var cardType = random.Next(0, possibleCardTypes.Length);
            var powerCost = 0;
            //cardModel.CardSourceReferenceIndex = 

            card.AttributeSet.Set(AttributeKey.CardType, cardType);

            switch (cardType)
            {
                case CardTypes.DRAW:
                    var cardsToDraw = random.Next(1, 4); //1 to 3
                    powerCost = cardsToDraw * 2;

                    card.Name = $"Insight";

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

                    card.Name = $"Sword Attack";
                    /*
                    cardModel.Commands.Add(
                        new SumGlobalAttributeCommand(AttributeKey.Health, -powerCost * 2));
                    */
                    break;

                case CardTypes.POWER: // Attack
                    var powerGenerated = 3;
                    card.Name = $"Empower";

                    /*
                    cardModel.Commands.Add(
                        new SumAttributeCommand(player, AttributeKey.Power, powerGenerated));
                    */
                    break;

            }

            card.AttributeSet.Set(AttributeKey.PowerCost, powerCost);
        }

    }
}