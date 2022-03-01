using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.Model.Cards;
using Assets.Scripts.Core.Model.Cards.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.View.Cards
{
    public class CardSystemView : SerializedMonoBehaviour
    {
        [SerializeField, SceneObjectsOnly, Required] private CardCollectionView PlayerDeckCollectionView;
        [SerializeField, SceneObjectsOnly, Required] private CardCollectionView PlayerHandCollectionView;
        [SerializeField, SceneObjectsOnly, Required] private CardCollectionView PlayerDiscardCollectionView;
        
        [SerializeField, SceneObjectsOnly, Required] private CardPlayView CardPlayView;

        [SerializeField, AssetsOnly, Required] private CardView CardPrefab;
        
        private CardSpriteLibrary _cardSpriteLibrary;

        public void Initialize(List<Player> players, CardSpriteLibrary cardSpriteLibrary)
        {
            _cardSpriteLibrary = cardSpriteLibrary;

            PlayerDeckCollectionView.Initialize(CardPrefab, cardSpriteLibrary);
            PlayerHandCollectionView.Initialize(CardPrefab, cardSpriteLibrary);
            PlayerDiscardCollectionView.Initialize(CardPrefab, cardSpriteLibrary);

            CardPlayView.Initialize();

            DisplayPlayer(players.First());

            UIEvents.OnCardPointerUIEvent += OnCardPointerEvent;
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
                    return PlayerDeckCollectionView;

                case CardCollectionIdentifier.Hand:
                    return PlayerHandCollectionView;

                case CardCollectionIdentifier.Discard:
                    return PlayerDiscardCollectionView;

                default:
                    return null;
            }
        }

        private void OnCardPointerEvent(Card card, PointerEventData pointerEventData, int pointerTrigger)
        {
            switch (pointerTrigger)
            {
                case PointerEventTrigger.ENTER:
                    CardPlayView.Set(card, _cardSpriteLibrary.Get(card.CardDataIndex));
                    return;

                case PointerEventTrigger.EXIT:
                    CardPlayView.Hide();
                    return;

                case PointerEventTrigger.DOWN:
                    GameplayEvents.OnCardEvent(card, CardEventIdentifiers.Activate);
                    return;
            }
        }

    }
}