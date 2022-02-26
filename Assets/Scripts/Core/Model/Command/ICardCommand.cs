using Assets.Scripts.Core.Model;
using Assets.Scripts.Core.Model.Card;
using Assets.Scripts.Core.Model.Command;

namespace Assets.Scripts.Core.Commands
{
    public interface ICardCommand
    {
        CardCommandReport  Run(Card sourceCard, CombatModel combatModel);
        string Text { get; }
    }
}