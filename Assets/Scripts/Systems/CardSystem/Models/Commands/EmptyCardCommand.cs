using UnityEngine;

namespace Assets.Scripts.CardSystem.Models.Commands
{
    public class EmptyCardCommand : ICardCommand
    {
        public CardCommandReport Run(Card sourceCard, GameModel gameModel)
        {
            Debug.Log("A basic command was run");

            return new CardCommandReport(CardCommandStatus.Success);
        }

        public string Text => string.Empty;
    }
}