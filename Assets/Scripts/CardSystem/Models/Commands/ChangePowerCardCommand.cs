using Assets.Scripts.CardSystem.Model;
using Assets.Scripts.CardSystem.Model.Command;

namespace Assets.Scripts.CardSystem
{
    internal class ChangePowerCardCommand : ICardCommand
    {
        public CardCommandReport Run(Card sourceCard, GameContext gameContext)
        {
            var effectType = sourceCard.Resources[CardResourceNames.POWER_EFFECT_TYPE];
            var effectValue = sourceCard.Resources[CardResourceNames.POWER_EFFECT];
            var playerResource = sourceCard.Collection.CardPlayer.Resources[PlayerResourceNames.Power];

            switch (effectType.Value)
            {
                case 1:
                    playerResource.Value += effectValue.Value;
                    return new CardCommandReport(CardCommandStatus.Success);

                case 2:
                    playerResource.Value -= effectValue.Value;
                    return new CardCommandReport(CardCommandStatus.Success);

            }

            return new CardCommandReport(CardCommandStatus.Failed);
        }
    }
}