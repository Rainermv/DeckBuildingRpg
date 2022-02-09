using System.Collections.Generic;
using Assets.Scripts.CardSystem.Model;
using Assets.Scripts.CardSystem.Model.Collection;

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
        }

        private static List<Card> BuildCards(int numOfCards)
        {
            var cards = new List<Card>();
            for (var i = 0; i < numOfCards; i++)
            {
                cards.Add(Card.Make($"{i}", 0));
            }

            return cards;
        }
    }
}