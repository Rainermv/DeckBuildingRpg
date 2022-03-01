using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Model;
using Assets.Scripts.Core.Model.AttributeModel;
using Assets.Scripts.Core.Model.Cards;
using Assets.Scripts.Core.Model.Cards.Collections;
using Assets.Scripts.Core.Model.EntityModel;
using Assets.Scripts.Core.Model.GridMap;
using Assets.Scripts.Core.Utility;

namespace Assets.Scripts.Controller.Factories
{
    public class BattleModelFactory
    {
        public static CombatModel Build(GridMapModel gridMapModel, List<CardData> cardDataList,
            Dictionary<string, int> attributeMap)
        {
            var levelModel = new CombatModel()
            {
                GridMapModel = gridMapModel,
                GlobalAttributes = new Attributes(),
                Players = CardSystemModelFactory.BuildPlayers(1),
                CardDataList = cardDataList,
                Entities = new List<Entity>(),
                AttributeMap = attributeMap
            };

            foreach (var player in levelModel.Players)
            {
                player.CardCollections[CardCollectionIdentifier.Deck].InsertCards(
                    BuildRandomCards(20, cardDataList));
            }

            

            //int i = 0;
            foreach (var player in levelModel.Players)
            {
                SetupPlayer(player);
                AddEntityTo(player, levelModel, levelModel.GridMapModel.GridTiles[0].GridPosition);
                AddEntityTo(player, levelModel, levelModel.GridMapModel.GridTiles[5].GridPosition);
                AddEntityTo(player, levelModel, levelModel.GridMapModel.GridTiles[15].GridPosition);
                AddEntityTo(player, levelModel, levelModel.GridMapModel.GridTiles[16].GridPosition);
                AddEntityTo(player, levelModel, levelModel.GridMapModel.GridTiles[17].GridPosition);
                AddEntityTo(player, levelModel, levelModel.GridMapModel.GridTiles[18].GridPosition);
                AddEntityTo(player, levelModel, levelModel.GridMapModel.GridTiles[19].GridPosition);
                AddEntityTo(player, levelModel, levelModel.GridMapModel.GridTiles[20].GridPosition);
                AddEntityTo(player, levelModel, levelModel.GridMapModel.GridTiles[25].GridPosition);
                AddEntityTo(player, levelModel, levelModel.GridMapModel.GridTiles[35].GridPosition);
                AddEntityTo(player, levelModel, levelModel.GridMapModel.GridTiles[45].GridPosition);
                AddEntityTo(player, levelModel, levelModel.GridMapModel.GridTiles[55].GridPosition);



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
                var card = Card.Make(cardDataModels[random.Next(0, cardDataModels.Count)]);

                cards.Add(card);
            }

            return cards;
        }

        private static void AddEntityTo(Player player, CombatModel combatModel,
            GridPosition gridPosition)
        {
            var entity = Entity.Make(player.Name, gridPosition, player);

            entity.Attributes.Add(0, 0);
            entity.Attributes.Add(1, 0);

            entity.MovementRange = 10;
            combatModel.Entities.Add(entity);
        }


        private static void SetupPlayer(Player player)
        {
            player.Attributes.SetValue(AttributeKey.Power, 5);


        }
    }
}