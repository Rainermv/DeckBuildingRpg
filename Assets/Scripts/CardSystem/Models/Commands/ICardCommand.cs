namespace Assets.Scripts.CardSystem.Models.Commands
{
    public interface ICardCommand
    {
        CardCommandReport  Run(Card sourceCard, GameContext gameContext);
        string Text { get; }
    }
}