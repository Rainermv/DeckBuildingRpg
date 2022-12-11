using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.Model.Cards;
using Assets.Scripts.Core.Model.Cards.Collections;
using Assets.Scripts.View.CardInteraction;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.View.Cards
{
    public class CardCanvasViewController : SerializedMonoBehaviour, IPointerMoveHandler
    {
        [SerializeField, SceneObjectsOnly, Required] private Transform _cardDragTransform;
        [SerializeField, SceneObjectsOnly, Required] private CardCollectionView _playerDeckCollectionView;
        [SerializeField, SceneObjectsOnly, Required] private CardCollectionView _playerHandCollectionView;
        [SerializeField, SceneObjectsOnly, Required] private CardCollectionView _playerDiscardCollectionView;
        
        [SerializeField, SceneObjectsOnly, Required] private CardPlayView _cardPlayView;

        [SerializeField, AssetsOnly, Required] private CardView _cardPrefab;
        
        private CardSpriteLibrary _cardSpriteLibrary;
        private CardInteractionStateController _cardInteractionStateController;

        public void Initialize(List<Player> players,
            CardSpriteLibrary cardSpriteLibrary)
        {
            _cardSpriteLibrary = cardSpriteLibrary;
            
            _cardInteractionStateController = new CardInteractionStateController(_cardDragTransform, _playerDeckCollectionView);
            
            _playerDeckCollectionView.Initialize(_cardPrefab, cardSpriteLibrary);
            _playerHandCollectionView.Initialize(_cardPrefab, cardSpriteLibrary);
            _playerDiscardCollectionView.Initialize(_cardPrefab, cardSpriteLibrary);
            _cardPlayView.Initialize();

            _cardPlayView.OnPointerUiEvent += _cardInteractionStateController.OnPlayViewInteractionEvent; 
            InteractionEvents.OnCardPointerUIEvent += _cardInteractionStateController.OnCardInteractionEvent;
            InteractionEvents.OnCardPointerUIEvent += HandleCardPlayView;  // todo: Should be moved to its own controller OR to interactionstatectonroller


            DisplayPlayer(players.First());

        }

        public void DisplayPlayer(Player player)
        {

            foreach (var (cardCollectionIdentifier, cardCollection) in player.CardCollections)
            {
                var cardCollectionView = CollectionViewFromIdentifier(cardCollectionIdentifier);
                if (cardCollectionView == null)
                {
                    Debug.LogError($"{cardCollectionIdentifier.ToString()} not found in SceneCardCollectionViews");
                    continue;
                }

                cardCollectionView.Display(cardCollection);

            }

        }
        

        private CardCollectionView CollectionViewFromIdentifier(CardCollectionIdentifier cardCollectionIdentifier)
        {
            switch (cardCollectionIdentifier)
            {
                case CardCollectionIdentifier.Deck:
                    return _playerDeckCollectionView;

                case CardCollectionIdentifier.Hand:
                    return _playerHandCollectionView;

                case CardCollectionIdentifier.Discard:
                    return _playerDiscardCollectionView;

                default:
                    return null;
            }
        }
        
        // todo: Should be moved to its own controller OR to cardInteractionState
        private void HandleCardPlayView(CardView cardView, Card card, PointerEventData arg3, int ev)
        {
            
            switch (ev)
            {
                case PointerEventTrigger.ENTER:
                    _cardPlayView.Set(card, _cardSpriteLibrary.Get(card.CardDataIndex));
                    return;

                case PointerEventTrigger.EXIT:
                    _cardPlayView.Hide();
                    return;
            }
        }


        public void OnPointerMove(PointerEventData eventData)
        {
            _cardInteractionStateController.SetPointerPosition(eventData.position);
        }
    }
}