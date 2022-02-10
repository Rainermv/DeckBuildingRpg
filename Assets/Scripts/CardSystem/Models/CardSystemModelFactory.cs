using System.Collections.Generic;
using Assets.Scripts.CardSystem.Constants;
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
            BuildPlayer(cardSystemModel, PlayerNames.PLAYER_1)
                .CardCollections[CardCollectionIdentifier.Deck].InsertCards(BuildCards(20));
            ;
            BuildPlayer(cardSystemModel, PlayerNames.PLAYER_2)
                .CardCollections[CardCollectionIdentifier.Deck].InsertCards(BuildCards(20));

            BuildPlayer(cardSystemModel, PlayerNames.PLAYER_3)
                .CardCollections[CardCollectionIdentifier.Deck].InsertCards(BuildCards(20));

            return cardSystemModel;

        }

        private static CardPlayer BuildPlayer(CardSystemModel cardSystemModel, string playerName)
        {
            var cardPlayer = cardSystemModel.AddNewPlayer(playerName);

            cardPlayer.AddNewCardCollection(CardCollectionIdentifier.Deck);
            cardPlayer.AddNewCardCollection(CardCollectionIdentifier.Hand);
            cardPlayer.AddNewCardCollection(CardCollectionIdentifier.Discard);

            return cardPlayer;
        }

        private static List<Card> BuildCards(int numOfCards)
        {
            var cards = new List<Card>();
            for (var i = 0; i < numOfCards; i++)
            {
                var card = Card.Make();

                cards.Add(card);
            }

            return cards;
        }
    }
}