namespace Assets.Scripts.CardSystem.Model.Command
{
    public class ComparingCommand : ICardCommand
    {
        private readonly int _v1;
        private readonly int _v2;

        public ComparingCommand(int v1, int v2)
        {
            _v1 = v1;
            _v2 = v2;
        }

        public CardCommandReport Run(Card sourceCard, GameContext gameContext)
        {
            return
                new CardCommandReport((_v1 == _v2)
                    ? CardCommandStatus.Success
                    : CardCommandStatus.Failed);

        }
    }
}