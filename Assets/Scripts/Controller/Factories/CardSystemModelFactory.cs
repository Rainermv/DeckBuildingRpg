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

        public static List<Player> BuildPlayers(int numOfPlayers)
        {

            var players = new List<Player>();

            for (int i = 0; i < numOfPlayers; i++)
            {
                BuildPlayer(players, PLAYER_NAMES[i]);
            }

            return players;

        }

        private static Player BuildPlayer(List<Player> players, string playerName)
        {
            var player = Player.Make(playerName);

            players.Add(player);

            player.AddNewCardCollection(CardCollectionIdentifier.Deck);
            player.AddNewCardCollection(CardCollectionIdentifier.Hand);
            player.AddNewCardCollection(CardCollectionIdentifier.Discard);

            return player;
        }

        
    }
}