using System;
using System.Collections.Generic;
using Assets.Scripts.CardSystem;
using UnityEngine;

public class CardSystemViewController : MonoBehaviour
{

    public CardCollectionView CardCollectionPrefab;
    public CardView CardPrefab;

    private Action<Card, CardView> _onCardClicked;
    private Action<CardCollection, CardCollectionView> _onCardCollectionClicked;

    private Dictionary<string, CardCollectionView> DeckComponents { get; set; }

    public void Initialize(Dictionary<string, CardCollection> cardCollections,
        Action<Card, CardView> onCardClicked,
        Action<CardCollection, CardCollectionView> onCardCollectionClicked)
    {
        _onCardClicked = onCardClicked;
        _onCardCollectionClicked = onCardCollectionClicked;

        var viewControllerTransform = GetComponent<RectTransform>();

        foreach (var (identifier, cardCollection) in cardCollections)
        {
            var cardCollectionView = Instantiate(CardCollectionPrefab);

            //var CardCollectionTransform = cardCollectionView.GetComponent<RectTransform>();

            cardCollectionView.GetComponent<RectTransform>().SetParent(viewControllerTransform, false);
            cardCollectionView.Initialize(OnInstantiateCardViews, identifier, onCardClicked);
            cardCollectionView.OnCollectionClicked = () => _onCardCollectionClicked(cardCollection, cardCollectionView);


            cardCollection.OnUpdate += () => cardCollectionView.OnDeckUpdate(cardCollection);

            cardCollection.OnUpdate?.Invoke();

        }

        
    }

    private List<CardView> OnInstantiateCardViews(int numberOfCards, CardCollectionView collectionView)
    {
        var cardCollectionTransform = collectionView.GetComponent<RectTransform>();
        var cardViews = new List<CardView>();
        for (int i = 0; i < numberOfCards; i++)

        {
            var cardView = Instantiate(CardPrefab);
            cardView.GetComponent<RectTransform>().SetParent(cardCollectionTransform, false);

            cardViews.Add(cardView);

            // VIEW -> CONTROLLER
            //cardView.CardButton.onClick.AddListener(() => _onCardClicked(card, cardView));

            //card.OnUpdate?.Invoke();
        }

        return cardViews;

    }

}