using Assets.Scripts.View.Cards;
using UnityEngine;

namespace Assets.Scripts.View.CardInteraction.States
{
    public interface ICardInteractionState
    {
        void Initialize(Transform cardDragTransform, CardCollectionView playerDeckCollectionView); 
        void Finalize();
        ICardInteractionState OnCardPointerEnter(CardInteractionStateModel stateModel);
        ICardInteractionState OnCardPointerMove(CardInteractionStateModel stateModel);
        ICardInteractionState OnCardPointerExit(CardInteractionStateModel stateModel);
        ICardInteractionState OnCardPointerDown(CardInteractionStateModel stateModel);
        ICardInteractionState OnCardPointerUp(CardInteractionStateModel stateModel);
        ICardInteractionState OnPlayAreaMove(CardInteractionStateModel stateModel);
        ICardInteractionState OnCardBeginDrag(CardInteractionStateModel stateModel);
        ICardInteractionState OnCardClick(CardInteractionStateModel stateModel);
    }
}