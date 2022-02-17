using System;
using Assets.Scripts.CardSystem.Models.Attributes;
using Assets.Scripts.Controller;

namespace Assets.Scripts.Model.CardModel.Commands
{
    internal class SumAttributeCommand : ICardCommand
    {
        private readonly ITargetable _affectedTargetable;
        private readonly int _attributeKey;
        private readonly int _sumValue;

        public SumAttributeCommand(ITargetable affectedTargetable, int attributeKey, int sumValue)
        {
            _affectedTargetable = affectedTargetable;
            _attributeKey = attributeKey;
            _sumValue = sumValue;
        }

        public CardCommandReport Run(Card sourceCard, LevelModel levelModel)
        {
            _affectedTargetable.AttributeSet.Sum(_attributeKey, _sumValue);
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

    internal interface ITargetable
    {
        AttributeSet AttributeSet { get; }
    }
}