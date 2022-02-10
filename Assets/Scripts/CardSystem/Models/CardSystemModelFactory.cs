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
            cardPlayer.AddNewCardCollection(CardCollectionIdentifier.Deck);
            cardPlayer.CardCollections[CardCollectionIdentifier.Deck]
                .InsertCards(BuildCards(20));

            cardPlayer.AddNewCardCollection(CardCollectionIdentifier.Hand);
            cardPlayer.AddNewCardCollection(CardCollectionIdentifier.Discard);

            cardPlayer.AttributeSet.Set(PlayerAttributeNames.Power, 0);
        }

        private static List<Card> BuildCards(int numOfCards)
        {
            var random = new Random();

            var cards = new List<Card>();
            for (var i = 0; i < numOfCards; i++)
            {
                var powerEfectType = random.Next(1, 3);

                var sign = powerEfectType == 1 ? "+" : "-";
                var card = Card.Make($"{sign}{i}");

                card.AttributeSet.Set(CardAttributeNames.POWER_EFFECT_TYPE, powerEfectType);
                card.AttributeSet.Set(CardAttributeNames.POWER_EFFECT, i);

                card.Commands.Add(new ChangePowerCardCommand());

                cards.Add(card);
            }

            return cards;
        }
    }
}