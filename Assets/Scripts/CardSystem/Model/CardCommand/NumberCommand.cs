using Assets.Scripts.CardSystem.CardCommand;
using UnityEngine;

namespace Assets.Tests
{
    public class NumberCommand : ICardCommand
    {
        public int Value { get; set; }


        public NumberCommand(int value)
        {
            Value = value;
        }

        public CardCommandReport Run()
        {
            Debug.Log($"Number Command Was Run. Value: [{Value}]");

            return new CardCommandReport(CardCommandStatus.Success);
        }

    }
}