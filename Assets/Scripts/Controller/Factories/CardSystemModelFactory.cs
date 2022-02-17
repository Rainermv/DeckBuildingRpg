using System.Collections.Generic;
using Assets.Scripts.Model.CardModel;
using Assets.Scripts.Model.CardModel.Collections;
using Assets.Scripts.Systems.CardSystem.Constants;

namespace Assets.Scripts.Controller.Factories
{
    internal class CardSystemModelFactory
    {
        public static Dictionary<string, Player> BuildPlayers()
        {
            var playerDictionary = new Dictionary<string, Player>();
            BuildPlayer(playerDictionary, PlayerNames.PLAYER_1)
                .CardCollections[CardCollectionIdentifier.Deck].InsertCards(BuildCards(20));
            ;
            BuildPlayer(playerDictionary, PlayerNames.PLAYER_2)
                .CardCollections[CardCollectionIdentifier.Deck].InsertCards(BuildCards(20));

            BuildPlayer(playerDictionary, PlayerNames.PLAYER_3)
                .CardCollections[CardCollectionIdentifier.Deck].InsertCards(BuildCards(20));

            BuildPlayer(playerDictionary, PlayerNames.PLAYER_4)
                .CardCollections[CardCollectionIdentifier.Deck].InsertCards(BuildCards(20));

            return playerDictionary;

        }

        private static Player BuildPlayer(Dictionary<string, Player> playersDictionary, string playerName)
        {
            var player = Player.Make(playerName);

            playersDictionary.Add(playerName, player);

            player.AddNewCardCollection(CardCollectionIdentifier.Deck);
            player.AddNewCardCollection(CardCollectionIdentifier.Hand);
            player.AddNewCardCollection(CardCollectionIdentifier.Discard);

            return player;
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