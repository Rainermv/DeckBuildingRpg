using System.Runtime.CompilerServices;
using Assets.Scripts.Core.Events;
using Assets.Scripts.View.Cards;
using UnityEngine;

namespace Assets.Scripts.View.CardInteraction.States
{
    public class IdleCardInteractionState : ICardInteractionState
    {
        public static IdleCardInteractionState Create()
        {
            return new IdleCardInteractionState();
        }

        public void Initialize(Transform cardDragTransform, CardCollectionView playerDeckCollectionView)
        {
            DebugEvents.Log(this, $"Begin Idle");
        }

        public void Finalize()
        {
            DebugEvents.Log(this, $"End Idle");
        }

        public ICardInteractionState OnCardPointerEnter(CardInteractionStateModel stateModel)
        {
            return this;
        }

        public ICardInteractionState OnCardPointerMove(CardInteractionStateModel stateModel)
        {
            return this;
        }

        public ICardInteractionState OnCardPointerExit(CardInteractionStateModel stateModel)
        {
            // do nothing
            return this;

            /*
            var cardScript = _parsedCardScripts[card.CardData.Index];
            var commandPlayData = new CardScriptCommandPlayData();

            foreach (var cardScriptCommand in cardScript.CardScriptCommands)
            {
                if (cardScriptCommand.OnValidatePlay != null &&
                    !cardScriptCommand.OnValidatePlay(sourceEntity, commandPlayData, combatModel))
                {
                    DebugEvents.OnLog(this,$"Failed to Validate: {cardScriptCommand.ScriptCommandText}\n" +
                                    $"on Targets: {string.Join(", ", commandPlayData.targetables.Select(t => t.Name))}");
                    return;
                }

                DebugEvents.OnLog(this, $"Activating: {cardScriptCommand.ScriptCommandText}\n" +
                                        $"on Targets: {string.Join(", ", commandPlayData.targetables.Select(t => t.Name))}");
                commandPlayData = cardScriptCommand.OnPlay(sourceEntity, commandPlayData, combatModel);
            }
            */
        }

        public ICardInteractionState OnCardPointerDown(CardInteractionStateModel stateModel)
        {
            return this;
        }

        public ICardInteractionState OnCardPointerUp(CardInteractionStateModel stateModel)
        {
            // nothing for now
            return this;
        }

        public ICardInteractionState OnPlayAreaMove(CardInteractionStateModel stateModel)
        {
            return this;
        }

        public ICardInteractionState OnCardBeginDrag(CardInteractionStateModel stateModel)
        {
            //GameplayEvents.OnCardEvent(stateModel.Card, CardEvents.Select);

            return PlayingCardInteractionState.Create(stateModel.CardView, stateModel.Card, true);
        }

        public ICardInteractionState OnCardClick(CardInteractionStateModel stateModel)
        {
            //GameplayEvents.OnCardEvent(stateModel.Card, CardEvents.Select);

            return PlayingCardInteractionState.Create(stateModel.CardView, stateModel.Card, false);
        }
    }
}