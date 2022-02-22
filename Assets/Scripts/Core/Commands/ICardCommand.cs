using Assets.Scripts.Core.Model;
using Assets.Scripts.Core.Model.Card;
using Assets.Scripts.Core.Model.Command;

namespace Assets.Scripts.Core.Commands
{
    public interface ICardCommand
    {
        CardCommandReport  Run(CardModel sourceCardModel, BattleModel battleModel);
        string Text { get; }
    }
}