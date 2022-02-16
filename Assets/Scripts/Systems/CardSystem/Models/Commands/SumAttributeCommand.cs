using System;
using Assets.Scripts.CardSystem.Models.Attributes;
using Assets.Scripts.Ruleset;

namespace Assets.Scripts.CardSystem.Models.Commands
{
    internal class SumAttributeCommand : ICardCommand
    {
        private readonly AttributeSet _affectedAttributeSet;
        private readonly AttributeKey _attributeKey;
        private readonly int _sumValue;

        public SumAttributeCommand(AttributeSet affectedAttributeSet, AttributeKey attributeKey, int sumValue)
        {
            _affectedAttributeSet = affectedAttributeSet;
            _attributeKey = attributeKey;
            _sumValue = sumValue;
        }

        public CardCommandReport Run(Card sourceCard, GameModel gameModel)
        {
            _affectedAttributeSet.Sum(_attributeKey, _sumValue);
            return new CardCommandReport(CardCommandStatus.Success);
        }

        public string Text
        {
            get
            {
                if (_sumValue >= 0)
                    return $"Add {_sumValue} to {_attributeKey}";
                return $"Subtract {Math.Abs(_sumValue)} to {_attributeKey}";
            }
        }
    }
}