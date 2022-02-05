using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.CardSystem;
using Assets.Scripts.CardSystem.CardCommand;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public CardViewController CardViewController;

    


    private CardSystemController _cardSystemController;

    // Start is called before the first frame update
    void Start()
    {
        _cardSystemController = new CardSystemController();
        _cardSystemController.AddDeck("Deck", 
            MakeDeck(
                new List<Card>()
                {
                    MakeCard("Card 1", 1),
                    MakeCard("Card 2", 2)
                }));
        
        var decks = _cardSystemController.Initialize();

        CardViewController.Initialize(decks, OnCardClicked);

    }

    private void OnCardClicked(Card card)
    {
        card.Play();
    }

    // Probably another class later
    

    private Deck MakeDeck(List<Card> cards)
    {
        return Deck.Make(cards);
    }

    private Card MakeCard(string cardName, int cardType)
    {
        var card = Card.Make(cardName);

        card.Commands = new List<ICardCommand>()
        {
            new SwitchTypeCommand(card)
        };

        card.OnUpdate += () => OnCardUpdate(card);

        card.OnStartPlay += OnCardStartPlay;
        card.OnComandRun += OnCardCommandRun;
        card.OnFinishPlay += OnCardFinishPlay;

        card.CardType = cardType;

        return card;

    }

    private void OnCardUpdate(Card obj)
    {
    }

    private void OnCardFinishPlay(Card c, CardPlayReport carPlayReport)
    {
    }

    private void OnCardCommandRun(Card c, CardPlayReport arg2, CardCommandReport arg3)
    {
    }

    private void OnCardStartPlay(Card c, CardPlayReport arg2)
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}