using UnityEngine;

namespace Assets.Scripts.CardSystem.CardCommand
{
    public class EmptyCardCommand : ICardCommand
    {
        public CardCommandReport Run()
        {
            Debug.Log("A basic command was run");

            return new CardCommandReport(CardCommandStatus.Success);
        }
    }
}