using System;
using System.Collections.Generic;
using Assets.Scripts.CardSystem.Model;
using Assets.Scripts.CardSystem.Model.CardCollection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Assets.Scripts.CardSystem.View
{
    public class CardCollectionView : MonoBehaviour, IPointerClickHandler
    {
        
        public RectTransform CardsContainer;
        public TextMeshProUGUI TitleText;
        public TextMeshProUGUI CardCounterText;

        [SerializeField] private CardCollectionViewOptions viewOptions;

        private List<CardView> _cardViews = new();
        private Func<int, RectTransform, List<CardView>> _onInstantiateCardViews;
        private Action<CardCollectionView> _onCollectionClicked;

        public CardCollection CardCollection { get; private set; }

        public void Initialize(Func<int, RectTransform, List<CardView>> onInstantiateCardViews,
            CardCollection cardCollection,
            Action<CardCollectionView> onCardCollectionClicked)
        {
            this.CardCollection = cardCollection;

            _onInstantiateCardViews = onInstantiateCardViews;

            _onCollectionClicked = onCardCollectionClicked;

            name = cardCollection.CollectionIdentifier.ToString();
            TitleText.text = cardCollection.CollectionIdentifier.ToString();

            cardCollection.OnCardListUpdate += UpdateCardCollectionView;

            _cardViews.AddRange(GetComponentsInChildren<CardView>());

        }


        public void UpdateCardCollectionView()
        {
            var cards = CardCollection.Cards;

            foreach (var cardView in _cardViews)
            {
                cardView.gameObject.SetActive(false);
            }

            if (_cardViews.Count < cards.Count)
            {
                var newCardViews = _onInstantiateCardViews(cards.Count - _cardViews.Count, CardsContainer);
                _cardViews.AddRange(newCardViews);
            }

            for (var i = 0; i < cards.Count; i++)
            {
                var cardView = _cardViews[i];

                cardView.SetCard(cards[i]);
                cardView.gameObject.SetActive(viewOptions.DisplayCards);

                // Trigger card on update
                cardView.OnCardUpdate();
            }

            CardCounterText.text = cards.Count.ToString();

        }

        
        public void OnPointerClick(PointerEventData eventData)
        {
            _onCollectionClicked(this);
        }

        
    }
}
