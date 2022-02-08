using System;
using System.Collections.Generic;
using Assets.Scripts.CardSystem.Model;
using Assets.Scripts.CardSystem.Model.CardCollection;
using UnityEngine;

namespace Assets.Scripts.CardSystem.View
{
    public class CardSystemViewController : MonoBehaviour
    {
        //public Dictionary<CardCollectionIdentifier, CardCollectionView> SceneCardCollectionViews;
        public CardCollectionView PlayerDeckCollectionView;
        public CardCollectionView PlayerHandCollectionView;
        public CardCollectionView PlayerDiscardCollectionView;
        
        public CardView CardPrefab;
        private Action<CardView> _onCardViewClicked;

        public void Initialize(Dictionary<CardCollectionIdentifier, CardCollection> cardCollections,
            Action<CardView> onCardViewClicked,
            Action<CardCollectionView> onCardCollectionViewClicked)
        {
            _onCardViewClicked = onCardViewClicked;

            foreach (var (cardCollectionIdentifier, cardCollection) in cardCollections)
            {
                var cardCollectionView = CollectionViewFromIdentifier(cardCollectionIdentifier);
                if (cardCollectionView == null)
                {
                    Debug.LogError($"{cardCollectionIdentifier.ToString()} not found in SceneCardCollectionViews");
                    continue;
                }

                cardCollectionView.Initialize(OnInstantiateCardViews,
                    cardCollection,
                    onCardCollectionViewClicked);

                cardCollectionView.UpdateCardCollectionView();
            }
        }

        private CardCollectionView CollectionViewFromIdentifier(CardCollectionIdentifier cardCollectionIdentifier)
        {
            switch (cardCollectionIdentifier)
            {
                case CardCollectionIdentifier.PlayerDeck:
                    return PlayerDeckCollectionView;

                case CardCollectionIdentifier.PlayerHand:
                    return PlayerHandCollectionView;

                case CardCollectionIdentifier.PlayerDiscard:
                    return PlayerDiscardCollectionView;

                default:
                    return null;
            }
        }


        private List<CardView> OnInstantiateCardViews(int numberOfCards, RectTransform parent)
        {
            var cardViews = new List<CardView>();
            for (int i = 0; i < numberOfCards; i++)

            {
                var cardView = Instantiate(CardPrefab);
                cardView.GetComponent<RectTransform>().SetParent(parent, false);
                cardViews.Add(cardView);

                cardView.Initialize(_onCardViewClicked);
                // VIEW -> CONTROLLER
                //cardView.CardButton.onClick.AddListener(() => _onCardClicked(card, cardView));

                //card.OnUpdate();
            }

            return cardViews;

        }

    }
}