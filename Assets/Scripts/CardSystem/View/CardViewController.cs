using System;
using System.Collections.Generic;
using Assets.Scripts.CardSystem;
using UnityEngine;

public class CardViewController : MonoBehaviour
{

    public DeckView DeckPrefab;
    public CardView CardPrefab;

    private Action<Card> _onCardClicked;

    private Dictionary<string, DeckView> DeckComponents { get; set; }

    public void Initialize(Dictionary<string, Deck> decks, Action<Card> onCardClicked)
    {
        _onCardClicked = onCardClicked;

        var viewControllerTransform = GetComponent<RectTransform>();

        foreach (var (identifier, deck) in decks)
        {
            var deckView = Instantiate(DeckPrefab);

            var deckViewTransform = deckView.GetComponent<RectTransform>();

            deckViewTransform.SetParent(viewControllerTransform, false);

            deckView.name = identifier;

            deck.OnUpdate += () => deckView.OnDeckUpdate(deck);

            foreach (var card in deck.Cards)
            {
                var cardView = Instantiate(CardPrefab);

                var cardViewTransform = cardView.GetComponent<RectTransform>();
                cardViewTransform.SetParent(deckViewTransform, false);
                
                card.OnUpdate += () => cardView.OnCardUpdate(card);
                card.OnStartPlay += cardView.OnStartPlay;
                card.OnComandRun += cardView.OnCommandRun;
                card.OnFinishPlay += cardView.OnFinishPlay;

                cardView.CardButton.onClick.AddListener(() => _onCardClicked(card));

                card.OnUpdate?.Invoke();

            }

            deck.OnUpdate?.Invoke();

        }

        
    }
}