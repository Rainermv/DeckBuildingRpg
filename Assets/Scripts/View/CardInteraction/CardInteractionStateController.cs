using System;
using Assets.Scripts.Core.Model.Cards;
using Assets.Scripts.View.CardInteraction.States;
using Assets.Scripts.View.Cards;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.View.CardInteraction
{
    internal class CardInteractionStateController
    {
        //TODO: THIS WILL NOT WORK, CARD EVENTS WILL ONLY TRIGGER WHEN ON OR OFF CARDS...
        //TODO: MUST REWRITE TO START INTERACTION ON CARD, BUT MUST KNOW POINTER POSITION OUTSIDE OF THEM (E.G. IN PLAY AREA)

        //- Play Phase (CardPlay)
        //        - Idle
        //             -> pointer_down card: check if can be played, then go to selecting
        //        - Selecting
        //             -> drag or pointer_up card: go to activated
        //        - Playing
        //             -> in play area:
        //                 -> "move": card view OFF, target reticule OR card ghost
        //                 -> "release": play card, go to idle
        //             -> outside play area:
        //                 -> "move": Card view ON, reticule/ghost off
        //                 -> "release": Cancel, go to idle

        // Contains the current card play state
        private ICardInteractionState _current;
        private CardInteractionStateModel _interactionStateModel;

        private readonly Transform _cardDragTransform;
        private readonly CardCollectionView _playerDeckCollectionView;

       

        internal CardInteractionStateController(Transform cardDragTransform, CardCollectionView playerDeckCollectionView)
        {
            _cardDragTransform = cardDragTransform;
            _playerDeckCollectionView = playerDeckCollectionView;

            _current = IdleCardInteractionState.Create();

            _interactionStateModel = new CardInteractionStateModel();

        }

        public void SetPointerPosition(Vector2 pointerPosition)
        {
            _interactionStateModel.PointerPosition = pointerPosition;
            SetState(_current.OnCardPointerMove(_interactionStateModel));
        }


        public void OnPlayViewInteractionEvent(PointerEventData pointerEventData, int ev)
        {

            _interactionStateModel.InPlayArea = false;
            if (ev == PointerEventTrigger.MOVE)
            {
                _interactionStateModel.PointerPosition = pointerEventData.position;
                _interactionStateModel.InPlayArea = true;
                //SetState(_current.OnPlayAreaMove(_interactionStateModel));
            }
        }

        public void OnCardInteractionEvent(CardView cardView, Card card, PointerEventData pointerEventData, int ev)
        {
            _interactionStateModel.CardView = cardView;
            _interactionStateModel.Card = card;
            _interactionStateModel.PointerPosition = pointerEventData.position;

            switch (ev)
            {
                case PointerEventTrigger.ENTER:
                    _interactionStateModel.PointerOver = true;
                    SetState(_current.OnCardPointerEnter(_interactionStateModel));
                    break;
                case PointerEventTrigger.EXIT:
                    _interactionStateModel.PointerOver = false;
                    SetState(_current.OnCardPointerExit(_interactionStateModel));
                    break;
                case PointerEventTrigger.DOWN:
                    _interactionStateModel.PointerDown = true;
                    SetState(_current.OnCardPointerDown(_interactionStateModel));
                    break;
                case PointerEventTrigger.UP:
                    _interactionStateModel.PointerDown = false;
                    SetState(_current.OnCardPointerUp(_interactionStateModel));
                    break;
                case PointerEventTrigger.MOVE:
                    //SetState(_current.OnCardPointerMove(_interactionStateModel));
                    break;
                case PointerEventTrigger.BEGIN_DRAG:
                    SetState(_current.OnCardBeginDrag(_interactionStateModel));
                    break;
                case PointerEventTrigger.CLICK:
                    SetState(_current.OnCardClick(_interactionStateModel));
                    break;
            }
        }


        private void SetState(ICardInteractionState state)
        {
            if (_current == state) 
                return;

            _current.Finalize();
            state.Initialize(_cardDragTransform, _playerDeckCollectionView);

            _current = state;
        }


        
    }
}