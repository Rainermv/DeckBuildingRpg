using Assets.Scripts.View.Cards;
using UnityEngine;

namespace Assets.Scripts.View.CardInteraction.States
{
    internal class DisabledCardInteractionState : ICardInteractionState
    {
        private DisabledCardInteractionState()
        {

        }

        internal static ICardInteractionState Create()
        {
            return new DisabledCardInteractionState();
        }

        public void Initialize(Transform cardDragTransform, CardCollectionView playerDeckCollectionView)
        {
            throw new System.NotImplementedException();
        }

        public void Finalize()
        {
            throw new System.NotImplementedException();
        }

        public ICardInteractionState OnCardPointerEnter(CardInteractionStateModel stateModel)
        {
            return this;
        }

        public ICardInteractionState OnCardPointerMove(CardInteractionStateModel stateModel)
        {
            throw new System.NotImplementedException();
        }

        public ICardInteractionState OnCardPointerExit(CardInteractionStateModel stateModel)
        {
            return this;
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
            throw new System.NotImplementedException();
        }

        public ICardInteractionState OnCardBeginDrag(CardInteractionStateModel stateModel)
        {
            throw new System.NotImplementedException();
        }

        public ICardInteractionState OnCardClick(CardInteractionStateModel stateModel)
        {
            throw new System.NotImplementedException();
        }
    }
}