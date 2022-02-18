namespace Assets.Scripts.Model.Card.Commands
{
    public interface ICardCommand
    {
        CardCommandReport  Run(CardModel sourceCardModel, LevelModel levelModel);
        string Text { get; }
    }
}