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

        private List<CardView> _cardViews = new();
        private Func<int, RectTransform, List<CardView>> _onInstantiateCardViews;
        private Action<Card, CardView> _onCardClicked;
        private Action _onCollectionClicked;

        private CardCollectionViewOptions _collectionViewOptions;

        public string Identifier { get; set; }

        public void Initialize(Func<int, RectTransform, List<CardView>> onInstantiateCardViews,
            string identifier, Action<Card, CardView> onCardClicked,
            Action onCardCollectionClicked,
            CardCollectionViewOptions cardCollectionViewOptions)
        {
            _onCardClicked = onCardClicked;
            _onInstantiateCardViews = onInstantiateCardViews;
            name = identifier;
            _collectionViewOptions = cardCollectionViewOptions;
            _onCollectionClicked = onCardCollectionClicked;
            Identifier = identifier;

            GetComponent<RectTransform>().anchoredPosition = cardCollectionViewOptions.RectPosition;

            TitleText.text = cardCollectionViewOptions.Title;

            _cardViews.AddRange(GetComponentsInChildren<CardView>());


        }


        public void OnDeckUpdate(CardCollection cardCollection)
        {
            var cards = cardCollection.Cards;

            ResetCardViews();

            if (_cardViews.Count < cards.Count)
            {
                var newCardViews = _onInstantiateCardViews(cards.Count - _cardViews.Count, CardsContainer);
                _cardViews.AddRange(newCardViews);
            }

            for (var i = 0; i < cards.Count; i++)
            {
                var card = cards[i];
                var cardView = _cardViews[i];

                //Bind
                cardView.OnCardViewClicked = () => _onCardClicked(card, cardView);
                card.OnUpdate = () => cardView.OnCardUpdate(card);

                cardView.gameObject.SetActive(_collectionViewOptions.DisplayCards);

                // Trigger card on update
                card.OnUpdate();
            }

            CardCounterText.text = cards.Count.ToString();

        }


        private void ResetCardViews()
        {
            foreach (var cardView in _cardViews)
            {
                cardView.gameObject.SetActive(false);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _onCollectionClicked();
        }
    }
}
