using System;
using Assets.Scripts.CardSystem.Models.Attributes;

namespace Assets.Scripts.CardSystem.Models.Commands
{
    internal class SumAttributeCommand : ICardCommand
    {
        private readonly AttributeSet _affectedAttributeSet;
        private readonly string _attributeName;
        private readonly int _sumValue;

        public SumAttributeCommand(AttributeSet affectedAttributeSet, string attributeName, int sumValue)
        {
            _affectedAttributeSet = affectedAttributeSet;
            _attributeName = attributeName;
            _sumValue = sumValue;
        }

        public CardCommandReport Run(Card sourceCard, GameContext gameContext)
        {
            _affectedAttributeSet.Sum(_attributeName, _sumValue);
            return new CardCommandReport(CardCommandStatus.Success);
        }

        public string Text
        {
            get
            {
                if (_sumValue >= 0)
                    return $"Add {_sumValue} to {_attributeName}";
                return $"Subtract {Math.Abs(_sumValue)} to {_attributeName}";
            }
        }
    }
}