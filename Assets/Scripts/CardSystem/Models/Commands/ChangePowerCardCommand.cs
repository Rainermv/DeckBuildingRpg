using Assets.Scripts.CardSystem.Model;
using Assets.Scripts.CardSystem.Model.Command;

namespace Assets.Scripts.CardSystem
{
    internal class ChangePowerCardCommand : ICardCommand
    {
        public CardCommandReport Run(Card sourceCard, GameContext gameContext)
        {
            var effectType = sourceCard.AttributeSet.GetValue(CardAttributeNames.POWER_EFFECT_TYPE);
            var effectValue = sourceCard.AttributeSet.GetValue(CardAttributeNames.POWER_EFFECT);
            var playerPower = sourceCard.CardCollectionParent.CardPlayerParent.AttributeSet.Get(PlayerAttributeNames.Power);

            switch (effectType)
            {
                case 1:
                    playerPower.Value += effectValue;
                    return new CardCommandReport(CardCommandStatus.Success);

                case 2:
                    playerPower.Value -= effectValue;
                    return new CardCommandReport(CardCommandStatus.Success);

            }

            return new CardCommandReport(CardCommandStatus.Failed);
        }
    }
}