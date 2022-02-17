using System;
using System.Collections.Generic;
using Assets.Scripts.Model.CardModel;
using Assets.Scripts.Model.CardModel.Collections;
using Assets.Scripts.Systems.CardSystem.Constants;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Systems.CardSystem.Views
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

        public CardCollection CardCollection { get; private set; }

        public void Initialize(Func<int, RectTransform, List<CardView>> onInstantiateCardViews,
            Action<CardCollectionView> onCardCollectionViewClicked)
        {
            _onInstantiateCardViews = onInstantiateCardViews;
            _onCardCollectionViewClicked = onCardCollectionViewClicked;

            CardsContainer.gameObject.SetActive(viewOptions.CardsVisible);

        }

        public void Display(CardCollection cardCollection)
        {
            this.CardCollection = cardCollection;

            name = cardCollection.CollectionIdentifier.ToString();
            TitleText.text = cardCollection.CollectionIdentifier.ToString();

            cardCollection.OnCardListUpdate = (cards) => UpdateCardList(cards);

            switch (cardCollection.PlayerParent.Name)
            {
                case PlayerNames.PLAYER_1:
                    Image.color = Color.blue;
                    break;

                case PlayerNames.PLAYER_2:
                    Image.color = Color.red;
                    break;

                case PlayerNames.PLAYER_3:
                    Image.color = Color.green;
                    break;

            }

            // Trigger update to display data
            UpdateCardList(cardCollection.Cards);

        }

        public void UpdateCardList(List<Card> cards)
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
