using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Model;
using Assets.Scripts.Core.Model.AttributeModel;
using Assets.Scripts.Core.Model.Cards;
using Assets.Scripts.Core.Model.Cards.Collections;
using Assets.Scripts.Core.Model.EntityModel;
using Assets.Scripts.Core.Utility;

namespace Assets.Scripts.Controller.Factories
{
    public class BattleModelFactory
    {
        public static CombatModel Build(List<CardData> cardDataList,
            Dictionary<string, int> attributeMap)
        {
            var levelModel = new CombatModel()
            {
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
                AddEntityTo(player, levelModel);
                
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

        private static void AddEntityTo(Player player, CombatModel combatModel)
        {
            var entity = Entity.Make(player.Name, player);

            entity.Attributes.Add(0, 0);
            entity.Attributes.Add(1, 0);

            combatModel.Entities.Add(entity);
        }


        private static void SetupPlayer(Player player)
        {
            player.Attributes.SetValue(AttributeKey.Power, 5);


        }
    }
}