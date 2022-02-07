using UnityEngine;

namespace Assets.Scripts.CardSystem.Model.CardCommand
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