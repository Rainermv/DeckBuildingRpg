using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.CardSystem;
using Mono.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class CardCollectionView : MonoBehaviour, IPointerClickHandler
{
    public bool IsClickable { get; set; }


    private List<CardView> _cardViews = new List<CardView>();

    private Func<int, CardCollectionView, List<CardView>> _onInstantiateCardViews;

    private Action<Card, CardView> _onCardClicked;
    public Action OnCollectionClicked;

    public void Initialize(Func<int, CardCollectionView, List<CardView>> onInstantiateCardViews,
        string identifier,
        Action<Card, CardView> onCardClicked)
    {
        _onCardClicked = onCardClicked;
        _onInstantiateCardViews = onInstantiateCardViews;
        name = identifier;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDeckUpdate(CardCollection cardCollection)
    {
        switch (cardCollection.CollectionType)
        {
            case CardCollection.Types.DECK:
                IsClickable = true;
                GetComponent<Image>().color = Color.blue;

                UpdateCards(cardCollection);
                ResetCardViews();
                break;
            case CardCollection.Types.HAND:
                IsClickable = false;
                UpdateCards(cardCollection);
                break;
        }
    }


    private void UpdateCards(CardCollection cardCollection)
    {
        var cards = cardCollection.Cards;
        
        ResetCardViews();

        if (_cardViews.Count < cards.Count)
        {
            var newCardViews = _onInstantiateCardViews(cards.Count - _cardViews.Count, this);
            _cardViews.AddRange(newCardViews);
        }

        for (var i = 0; i < cards.Count; i++)
        {
            var card = cards[i];
            var cardView = _cardViews[i];

            //Bind
            cardView.OnCardViewClicked = () => _onCardClicked(card, cardView);
            card.OnUpdate = () => cardView.OnCardUpdate(card);

            cardView.gameObject.SetActive(true);
        }


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
        OnCollectionClicked?.Invoke();
    }
}
