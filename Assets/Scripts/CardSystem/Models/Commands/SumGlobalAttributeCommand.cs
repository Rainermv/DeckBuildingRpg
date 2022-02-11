namespace Assets.Scripts.CardSystem.Models.Commands
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

        public string Text
        {
            get
            {
                if (_sumValue >= 0)
                    return $"Add {_sumValue} to {_gameContextAttributeName}";
                return $"Subtract {_sumValue - _sumValue} to {_gameContextAttributeName}";
            }
        }
    }
}