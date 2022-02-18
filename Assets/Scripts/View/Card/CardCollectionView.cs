using System;
using System.Collections.Generic;
using Assets.Scripts.Model.Card;
using Assets.Scripts.Model.Card.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.View.Card
{
    public class CardCollectionView : MonoBehaviour, IPointerClickHandler
    {


        public Image Image;
        public RectTransform CardsContainer;
        public TextMeshProUGUI TitleText;
        public TextMeshProUGUI CardCounterText;

        [SerializeField] private CardCollectionViewOptions viewOptions;

        private List<CardView> _cardViews = new();
        private Func<int, RectTransform, List<CardView>> _onInstantiateCardViews;
        private Action<CardCollectionView> _onCardCollectionViewClicked;

        public CardCollectionModel CardCollectionModel { get; private set; }

        public void Initialize(Func<int, RectTransform, List<CardView>> onInstantiateCardViews,
            Action<CardCollectionView> onCardCollectionViewClicked)
        {
            _onInstantiateCardViews = onInstantiateCardViews;
            _onCardCollectionViewClicked = onCardCollectionViewClicked;

            CardsContainer.gameObject.SetActive(viewOptions.CardsVisible);

        }

        public void Display(CardCollectionModel cardCollectionModel)
        {
            this.CardCollectionModel = cardCollectionModel;

            name = cardCollectionModel.CollectionIdentifier.ToString();
            TitleText.text = cardCollectionModel.CollectionIdentifier.ToString();

            cardCollectionModel.OnCardListUpdate = (cards) => UpdateCardList(cards);
            
            // Trigger update to display data
            UpdateCardList(cardCollectionModel.Cards);

        }

        public void UpdateCardList(List<CardModel> cards)
        {

            foreach (var cardView in _cardViews)
            {
                cardView.Reset(false);
            }

            if (_cardViews.Count < cards.Count)
            {
                var newCardViews = _onInstantiateCardViews(cards.Count - _cardViews.Count, CardsContainer);
                _cardViews.AddRange(newCardViews);
            }

            for (var i = 0; i < cards.Count; i++)
            {
                _cardViews[i].Display(cards[cards.Count - i - 1]);
                _cardViews[i].Reset(true);
            }

            CardCounterText.text = cards.Count.ToString();

        }

        
        public void OnPointerClick(PointerEventData eventData)
        {
            _onCardCollectionViewClicked(this);
        }


        
    }
}
