using Assets.Scripts.Core.Events;
using Assets.Scripts.Core.Model.Cards;
using Assets.Scripts.View.Cards;
using UnityEngine;

namespace Assets.Scripts.View.CardInteraction.States
{
    public class PlayingCardInteractionState : ICardInteractionState
    {
        private readonly CardView _selectedCardView;
        private readonly Card _selectedCard;
        private readonly bool _isDragging;
        private Transform _cardDragTransform;
        private CardCollectionView _playerDeckCollectionView;

        //             -> in play area:
        //                 -> "move": card view OFF, target reticule OR card ghost
        //                 -> "release": play card, go to idle
        //             -> outside play area:
        //                 -> "move": Card view ON, reticule/ghost off
        //                 -> "release": Cancel, go to idle

        private PlayingCardInteractionState(CardView selectedCardView, Card selectedCard, bool isDragging)
        {
            _selectedCardView = selectedCardView;
            _selectedCard = selectedCard;
            _isDragging = isDragging;

        }

        public static PlayingCardInteractionState Create(CardView selectedCardView, Card selectedCard,
            bool isDragging)
        {
            return new PlayingCardInteractionState(selectedCardView, selectedCard, isDragging);
        }

        public void Initialize(Transform cardDragTransform, CardCollectionView playerDeckCollectionView)
        {
            _cardDragTransform = cardDragTransform;
            _playerDeckCollectionView = playerDeckCollectionView;
            DebugEvents.Log(this, $"Begin Playing {_selectedCard.Name}, dragging: {_isDragging}");

            _selectedCardView.RectTransform.SetParent(_cardDragTransform, true);
        }

        public void Finalize()
        {
            _selectedCardView.RectTransform.SetParent(_playerDeckCollectionView.transform, false);
            _selectedCardView.RectTransform.anchoredPosition = Vector2.zero;
            DebugEvents.Log(this, $"Begin Activate");

        }

        public ICardInteractionState OnCardPointerEnter(CardInteractionStateModel stateModel)
        {
            return this;
           // Play card if NOT dragging (user clicked card, then clicked target)
           if (!_isDragging && stateModel.InPlayArea)
           {
               // this will not check if the play is valid or not (e.g: target has been selected)
               GameplayEvents.OnCardEvent(stateModel.Card, CardEvents.Play);
           }

           // whether the play is successful or not, we go back to Idle
           return IdleCardInteractionState.Create();
        }

        public ICardInteractionState OnCardPointerMove(CardInteractionStateModel stateModel)
        {
            _selectedCardView.RectTransform.anchoredPosition = stateModel.PointerPosition;
            
            return this;
        }

        public ICardInteractionState OnCardPointerExit(CardInteractionStateModel stateModel)
        {
            return this;

            // Play card if it IS dragging (user clicked card, then clicked target)
            if (_isDragging && stateModel.InPlayArea)
            {
                // this will not check if the play is valid or not (e.g: target has been selected)
                GameplayEvents.OnCardEvent(stateModel.Card, CardEvents.Play);
            }

            // whether the play is successful or not, we go back to Idle
            return IdleCardInteractionState.Create();
        }

        public ICardInteractionState OnCardPointerDown(CardInteractionStateModel stateModel)
        {
            return this;

        }

        public ICardInteractionState OnCardPointerUp(CardInteractionStateModel stateModel)
        {
            return this;
        }

        public ICardInteractionState OnPlayAreaMove(CardInteractionStateModel stateModel)
        {
            return this;
        }

        public ICardInteractionState OnCardBeginDrag(CardInteractionStateModel stateModel)
        {
            return this;
        }

        public ICardInteractionState OnCardClick(CardInteractionStateModel stateModel)
        {
            return this;
        }
    }
}