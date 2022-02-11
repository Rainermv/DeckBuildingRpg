using Assets.Scripts.Ruleset;

namespace Assets.Scripts.CardSystem.Models.Commands
{
    internal class SumGlobalAttributeCommand : ICardCommand
    {
        private readonly AttributeKey _attributeKey;
        private readonly int _sumValue;

        public SumGlobalAttributeCommand(AttributeKey attributeKey, int sumValue)
        {
            _attributeKey = attributeKey;
            _sumValue = sumValue;
        }

        public CardCommandReport Run(Card sourceCard, GameContext gameContext)
        {
            gameContext.GlobalAttributeSet.Sum(_attributeKey, _sumValue);
            return new CardCommandReport(CardCommandStatus.Success);
        }

        public string Text
        {
            get
            {
                if (_sumValue >= 0)
                    return $"Add {_sumValue} to {_attributeKey}";
                return $"Subtract {_sumValue - _sumValue} to {_attributeKey}";
            }
        }
    }
}