using UnityEngine;

namespace Assets.Scripts.Model.CardModel.Commands
{
    public class EmptyCardCommand : ICardCommand
    {
        public CardCommandReport Run(Card sourceCard, LevelModel levelModel)
        {
            Debug.Log("A basic command was run");

            return new CardCommandReport(CardCommandStatus.Success);
        }

        public string Text => string.Empty;
    }
}