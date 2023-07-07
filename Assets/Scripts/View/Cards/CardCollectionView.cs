using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.Model.AttributeModel;
using Assets.Scripts.Core.Model.Cards;
using Assets.Scripts.Core.Model.Cards.Collections;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View.Cards
{
    public class CardCollectionView : SerializedMonoBehaviour
    {
        [SerializeField, ChildGameObjectsOnly, Required] private Image Image;
        [SerializeField, ChildGameObjectsOnly, Required] private RectTransform CardsContainer;
        [SerializeField, ChildGameObjectsOnly, Required] private TextMeshProUGUI TitleText;
        [SerializeField, ChildGameObjectsOnly, Required] private TextMeshProUGUI CardCounterText;

        [SerializeField] private CardCollectionViewOptions viewOptions;

        private List<CardView> _cardViews = new();
        private CardView _cardPrefab;
        private CardSpriteLibrary _spriteLibrary;

        //private CardCollectionModel _cardCollectionModel;

        public void Initialize(CardView cardPrefab, CardSpriteLibrary spriteLibrary)
        {
            _cardPrefab = cardPrefab;
            _spriteLibrary = spriteLibrary;

            CardsContainer.gameObject.SetActive(viewOptions.CardsVisible);

        }

        public void Display(CardCollectionModel cardCollectionModel)
        {
            //this._cardCollectionModel = cardCollectionModel;

            name = cardCollectionModel.CollectionIdentifier.ToString();
            TitleText.text = cardCollectionModel.CollectionIdentifier.ToString();

            cardCollectionModel.OnCardCollectionCardListUpdate += UpdateCardList;
            
            // Trigger update to display data
            UpdateCardList(cardCollectionModel);
            
        }

        private void UpdateCardList(CardCollectionModel cardCollectionModel)
        {
            foreach (var cardView in _cardViews)
            {
                cardView.SetActive(false);
            }

            if (!viewOptions.CardsVisible)
            {
                return;
            }

            var cards = cardCollectionModel.Cards;

            if (_cardViews.Count < cards.Count)
            {
                var newCardViews = InstantiateCardViews(cards.Count - _cardViews.Count, CardsContainer);
                _cardViews.AddRange(newCardViews);
            }

            for (var i = 0; i < cards.Count; i++)
            {
                var card = cards[i];
                var cardView = _cardViews[i];

                cardView.Display(card, _spriteLibrary.Get(card.CardDataIndex));
            }

        }

        private List<CardView> InstantiateCardViews(int numberOfCards, RectTransform parent)
        {
            var cardViews = new List<CardView>();
            for (var i = 0; i < numberOfCards; i++)
            {
                var cardView = Instantiate<CardView>(_cardPrefab);
                cardView.ReportEvents = true;

                cardView.transform.SetParent(parent, false);
                cardViews.Add(cardView);
            }

            return cardViews;

        }


    }
}
