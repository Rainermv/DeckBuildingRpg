using Assets.Scripts.CardSystem.Model;
using Assets.Scripts.CardSystem.Model.Command;

namespace Assets.Scripts.CardSystem
{
    internal class SumGlobalAttributeCommand : ICardCommand
    {
        private readonly string _gameContextAttributeName;
        private readonly int _sumValue;

        public SumGlobalAttributeCommand(string gameContextAttributeName, int sumValue)
        {
            _gameContextAttributeName = gameContextAttributeName;
            _sumValue = sumValue;
        }

        public CardCommandReport Run(Card sourceCard, GameContext gameContext)
        {
            gameContext.GlobalAttributeSet.Sum(_gameContextAttributeName, _sumValue);
            return new CardCommandReport(CardCommandStatus.Success);
        }
    }
}