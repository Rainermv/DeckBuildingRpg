using Assets.Scripts.CardSystem.Models;
using Assets.Scripts.CardSystem.Models.Collections;

namespace Assets.Scripts.Ruleset
{
    public interface IGameController
    {
        GameModel SetupWithSettings(GameControllerSettings gameControllerSettings);

        void OnCardClicked(Card card);

        void OnCardCollectionClicked(CardCollection cardCollection);
    }
}