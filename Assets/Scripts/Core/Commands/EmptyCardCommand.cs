using Assets.Scripts.Core.Model;
using Assets.Scripts.Core.Model.Card;
using Assets.Scripts.Core.Model.Command;
using UnityEngine;

namespace Assets.Scripts.Core.Commands
{
    public class EmptyCardCommand : ICardCommand
    {
        public CardCommandReport Run(CardModel sourceCardModel, BattleModel battleModel)
        {
            Debug.Log("A basic command was run");

            return new CardCommandReport(CardCommandStatus.Success);
        }

        public string Text => string.Empty;
    }
}