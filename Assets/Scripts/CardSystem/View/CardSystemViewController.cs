using System;
using System.Collections.Generic;
using Assets.Scripts.CardSystem.Model;
using Assets.Scripts.CardSystem.Model.CardCollection;
using UnityEngine;

namespace Assets.Scripts.CardSystem.View
{
    public class CardSystemViewController : MonoBehaviour
    {
        public RectTransform DrawArea;
        public RectTransform HandArea;
        public RectTransform DiscardArea;


        public CardCollectionView DeckCollectionViewPrefab;
        public CardCollectionView HandcollectionViewPrefab;
        public CardView CardPrefab;

        private List<CardCollectionView> CardCollectionViews { get; set; } = new();

        public void Initialize(Dictionary<string, CardCollection> cardCollections,
            Action<Card, CardView> onCardClicked,
            Action<CardCollection, CardCollectionView> OnCardCollectionClicked)
        {

            var viewControllerTransform = GetComponent<RectTransform>();

            var collectionViewOptions = new Dictionary<string, CardCollectionViewOptions>()
            {
                {DeckSystemConstants.COLLECTION_DECK, CardCollectionViewOptions.MakeDeck(DrawArea)},
                {DeckSystemConstants.COLLECTION_HAND, CardCollectionViewOptions.MakeHand(HandArea)},
                {DeckSystemConstants.COLLECTION_DISCARD, CardCollectionViewOptions.MakeDeck(DiscardArea)}
            };


            foreach (var (identifier, cardCollection) in cardCollections)
            {
                var cardCollectionView = InstantiateCollectionPrefabFor(identifier);

                cardCollectionView.GetComponent<RectTransform>().SetParent(viewControllerTransform, false);
                cardCollectionView.Initialize(OnInstantiateCardViews,
                    identifier,
                    onCardClicked,
                    () => OnCardCollectionClicked(cardCollection,
                        cardCollectionView),
                    collectionViewOptions[identifier]);


                CardCollectionViews.Add(cardCollectionView);

                cardCollection.OnUpdate += () => cardCollectionView.OnDeckUpdate(cardCollection);

                cardCollection.OnUpdate();

            }

        
        }

        private CardCollectionView InstantiateCollectionPrefabFor(string identifier)
        {
            switch (identifier)
            {
                case DeckSystemConstants.COLLECTION_DECK:
                    return Instantiate(DeckCollectionViewPrefab);
                case DeckSystemConstants.COLLECTION_HAND:
                    return Instantiate(HandcollectionViewPrefab);
            }

            Debug.LogError($"No Prefab for [{identifier}] when instantiating");
            return null;
        }

        private List<CardView> OnInstantiateCardViews(int numberOfCards, RectTransform parent)
        {
            var cardViews = new List<CardView>();
            for (int i = 0; i < numberOfCards; i++)

            {
                var cardView = Instantiate(CardPrefab);
                cardView.GetComponent<RectTransform>().SetParent(parent, false);
                cardViews.Add(cardView);

                // VIEW -> CONTROLLER
                //cardView.CardButton.onClick.AddListener(() => _onCardClicked(card, cardView));

                //card.OnUpdate();
            }

            return cardViews;

        }

    }
}