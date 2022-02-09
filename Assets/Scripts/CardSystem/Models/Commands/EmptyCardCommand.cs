using UnityEngine;

namespace Assets.Scripts.CardSystem.Model.Command
{
    public class EmptyCardCommand : ICardCommand
    {
        public CardCommandReport Run(Card sourceCard, GameContext gameContext)
        {
            Debug.Log("A basic command was run");

            return new CardCommandReport(CardCommandStatus.Success);
        }
    }
}