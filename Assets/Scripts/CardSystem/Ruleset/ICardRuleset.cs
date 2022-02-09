using Assets.Scripts.CardSystem.Model;
using Assets.Scripts.CardSystem.Model.Collection;

namespace Assets.Scripts.CardSystem
{
    public interface ICardRuleset
    {
        void SetupSystem(CardSystemModel cardSystemModel);
        void SetupPlayer(CardPlayer cardPlayer);
        void SetupCollection(CardCollection cardCollection);
    }
}