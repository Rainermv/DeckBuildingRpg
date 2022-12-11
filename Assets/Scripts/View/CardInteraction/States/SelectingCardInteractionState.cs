using System;
using System.Timers;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Core.Model.Cards;
using Assets.Scripts.View.Cards;
using UnityEngine;

namespace Assets.Scripts.View.CardInteraction.States
{
    public class SelectingCardInteractionState : ICardInteractionState
    {
        private readonly Card _selectedCard;
        private bool _isDragging;

        private SelectingCardInteractionState(Card selectedCard)
        {
            _selectedCard = selectedCard;
            _isDragging = false;
        }

        public static SelectingCardInteractionState Create(Card selectedCard)
        {
            return new SelectingCardInteractionState(selectedCard);
        }

        public void Initialize(Transform cardDragTransform, CardCollectionView playerDeckCollectionView)
        {
            DebugEvents.Log(this, $"Begin Select ({_selectedCard.Name})");

            // At this point, user will be on pointer_down state
            // start timer to determine a drag 
            var timer = new Timer(500);
            timer.AutoReset = false;
            timer.Elapsed += (sender, args) =>
            {
                _isDragging = true;
                DebugEvents.Log(this, $"Drag Timer Elapsed");
            }; 
            timer.Start();
        }

        public void Finalize()
        {
            DebugEvents.Log(this, $"End Select ({_selectedCard.Name})");

            // nothing for now
        }

        public ICardInteractionState OnCardPointerEnter(CardInteractionStateModel stateModel)
        {
            if (_selectedCard != stateModel.Card)
            {
                // select a different card
                return SelectingCardInteractionState.Create(stateModel.Card);
            }

            // if we are hovering on the same card, keep state
            return this;

        }

        public ICardInteractionState OnCardPointerMove(CardInteractionStateModel stateModel)
        {
            return this;
        }

        public ICardInteractionState OnCardPointerExit(CardInteractionStateModel stateModel)
        {
            // 
            return this;
        }

        public ICardInteractionState OnCardPointerDown(CardInteractionStateModel stateModel)
        {
            return IdleCardInteractionState.Create();
        }

        public ICardInteractionState OnCardPointerUp(CardInteractionStateModel stateModel)
        {
            // todo: just a stub
            Console.WriteLine($"Play the cards");
            return IdleCardInteractionState.Create();
        }

        public ICardInteractionState OnPlayAreaMove(CardInteractionStateModel stateModel)
        {
            throw new NotImplementedException();
        }

        public ICardInteractionState OnCardBeginDrag(CardInteractionStateModel stateModel)
        {
            throw new NotImplementedException();
        }

        public ICardInteractionState OnCardClick(CardInteractionStateModel stateModel)
        {
            throw new NotImplementedException();
        }
    }
}