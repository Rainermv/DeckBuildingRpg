using Assets.Scripts.CardSystem.Models;
using Assets.Scripts.CardSystem.Models.Collections;

namespace Assets.Scripts.Ruleset
{
    public interface IGameRuleset
    {
        void Setup(GameContext gameContext);

        void OnCardClicked(Card card);

        void OnCardCollectionClicked(CardCollection cardCollection);
    }
}