using Assets.Scripts.CardSystem.Model;
using Assets.Scripts.CardSystem.Model.Command;

namespace Assets.Scripts.CardSystem
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
    }
}