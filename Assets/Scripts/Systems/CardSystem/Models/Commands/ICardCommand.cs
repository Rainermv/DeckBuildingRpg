namespace Assets.Scripts.CardSystem.Models.Commands
{
    public interface ICardCommand
    {
        CardCommandReport  Run(Card sourceCard, GameModel gameModel);
        string Text { get; }
    }
}