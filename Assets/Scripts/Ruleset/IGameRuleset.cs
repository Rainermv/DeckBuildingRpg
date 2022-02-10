using Assets.Scripts.CardSystem.Model;
using Assets.Scripts.CardSystem.Model.Collection;

namespace Assets.Scripts.CardSystem
{
    public interface IGameRuleset
    {
        void Setup(GameContext gameContext);

        void OnCardClicked(Card card);

        void OnCardCollectionClicked(CardCollection cardCollection);
    }
}