using System;
using Assets.Scripts.Core.Model;
using Assets.Scripts.Core.Model.Card;
using Assets.Scripts.Core.Model.Command;

namespace Assets.Scripts.Core.Commands
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

        public CardCommandReport Run(CardModel sourceCardModel, BattleModel battleModel)
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
}