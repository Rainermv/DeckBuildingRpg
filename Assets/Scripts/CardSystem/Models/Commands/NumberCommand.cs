using UnityEngine;

namespace Assets.Scripts.CardSystem.Model.Command
{
    public class NumberCommand : ICardCommand
    {
        public int Value { get; set; }


        public NumberCommand(int value)
        {
            Value = value;
        }

        public CardCommandReport Run(Card sourceCard, GameContext gameContext)
        {
            Debug.Log($"Number Command Was Run. Value: [{Value}]");

            return new CardCommandReport(CardCommandStatus.Success);
        }

    }
}