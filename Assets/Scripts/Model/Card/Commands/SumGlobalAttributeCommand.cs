using System;

namespace Assets.Scripts.Model.Card.Commands
{
    internal class SumGlobalAttributeCommand : ICardCommand
    {
        private readonly int _attributeKey;
        private readonly int _sumValue;

        public SumGlobalAttributeCommand(int attributeKey, int sumValue)
        {
            _attributeKey = attributeKey;
            _sumValue = sumValue;
        }

        public CardCommandReport Run(CardModel sourceCardModel, LevelModel levelModel)
        {
            levelModel.GlobalAttributeSet.Sum(_attributeKey, _sumValue);
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