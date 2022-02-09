using System.Collections.Generic;
using Assets.Scripts.CardSystem.Model;
using Assets.Scripts.CardSystem.Model.Collection;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.CardSystem
{
    internal class CardSystemModelFactory
    {
        public static CardSystemModel Build()
        {
            var cardSystemModel = new CardSystemModel();
            BuildPlayer(cardSystemModel, CardSystemConstants.PLAYER_1);
            BuildPlayer(cardSystemModel, CardSystemConstants.PLAYER_2);
            BuildPlayer(cardSystemModel, CardSystemConstants.PLAYER_3);

            return cardSystemModel;

        }

        private static void BuildPlayer(CardSystemModel cardSystemModel, string playerName)
        {
            var cardPlayer = cardSystemModel.AddNewPlayer(playerName);
            cardPlayer.AddNewCollection(CardCollectionIdentifier.Deck);
            cardPlayer.CardCollections[CardCollectionIdentifier.Deck]
                .InsertCards(BuildCards(20));

            cardPlayer.AddNewCollection(CardCollectionIdentifier.Hand);
            cardPlayer.AddNewCollection(CardCollectionIdentifier.Discard);

            cardPlayer.AddNewResource(PlayerResourceNames.Power).OnValueChanged += (res) =>
            {
                Debug.Log(res.Value);
            };
        }

        private static List<Card> BuildCards(int numOfCards)
        {
            var random = new Random();

            var cards = new List<Card>();
            for (var i = 0; i < numOfCards; i++)
            {
                var powerEfectType = random.Next(1, 3);
                var powerEffect = random.Next(1, 5);

                var sign = powerEfectType == 1 ? "+" : "-";
                var card = Card.Make($"{sign}{powerEffect}");

                card.AddNewResource(CardResourceNames.POWER_EFFECT_TYPE, powerEfectType);
                card.AddNewResource(CardResourceNames.POWER_EFFECT, powerEffect);

                card.Commands.Add(new ChangePowerCardCommand());

                cards.Add(card);
            }

            return cards;
        }
    }
}