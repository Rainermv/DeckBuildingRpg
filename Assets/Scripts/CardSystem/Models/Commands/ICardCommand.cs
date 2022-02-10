namespace Assets.Scripts.CardSystem.Model.Command
{
    public interface ICardCommand
    {
        CardCommandReport  Run(Card sourceCard, GameContext gameContext);
    }
}