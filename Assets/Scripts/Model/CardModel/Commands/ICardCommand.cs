namespace Assets.Scripts.Model.CardModel.Commands
{
    public interface ICardCommand
    {
        CardCommandReport  Run(Card sourceCard, LevelModel levelModel);
        string Text { get; }
    }
}