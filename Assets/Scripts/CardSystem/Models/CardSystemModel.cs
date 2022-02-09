using System.Collections.Generic;

namespace Assets.Scripts.CardSystem.Model
{
    public class CardSystemModel
    {
        internal Dictionary<string, CardPlayer> CardPlayers = new();

        public CardPlayer AddNewPlayer(string playerName)
        {
            var cardPlayer = CardPlayer.Make(playerName);
            CardPlayers.Add(playerName, cardPlayer);

            return cardPlayer;
        }


    }
}