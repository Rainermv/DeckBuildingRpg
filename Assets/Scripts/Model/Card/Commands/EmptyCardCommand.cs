using UnityEngine;

namespace Assets.Scripts.Model.Card.Commands
{
    public class EmptyCardCommand : ICardCommand
    {
        public CardCommandReport Run(CardModel sourceCardModel, LevelModel levelModel)
        {
            Debug.Log("A basic command was run");

            return new CardCommandReport(CardCommandStatus.Success);
        }

        public string Text => string.Empty;
    }
}