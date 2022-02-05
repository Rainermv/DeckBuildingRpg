using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSystemController
{

    private Dictionary<string, Deck> _decks { get; set; } = new();


    public Dictionary<string, Deck> Initialize()
    {
        // shuffle cards,
        // do other stuff

        return _decks;
    }


    public void AddDeck(string deckIdentifier, Deck deck)
    {
        _decks.Add(deckIdentifier, deck);
    }
}
