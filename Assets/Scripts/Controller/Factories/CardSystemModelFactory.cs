using System.Collections.Generic;
using Assets.Scripts.Core.Model.Card;
using Assets.Scripts.Core.Model.Card.Collections;

namespace Assets.Scripts.Controller.Factories
{
    internal class CardSystemModelFactory
    {
        private static List<string> PLAYER_NAMES = new List<string>()
        {
            "Player 1"
        };

        public static Dictionary<string, Player> BuildPlayers(int numOfPlayers)
        {

            var playerDictionary = new Dictionary<string, Player>();

            for (int i = 0; i < numOfPlayers; i++)
            {
                BuildPlayer(playerDictionary, PLAYER_NAMES[i]);
            }

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

        
    }
}