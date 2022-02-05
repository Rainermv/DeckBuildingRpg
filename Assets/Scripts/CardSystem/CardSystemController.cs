using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSystemController
{

    private Dictionary<string, CardCollection> _cardCollections { get; set; } = new();


    public Dictionary<string, CardCollection> Initialize()
    {
        // shuffle cards,
        // do other stuff

        return _cardCollections;
    }


    public void AddCardCollection(string identifier, CardCollection cardCollection)
    {
        _cardCollections.Add(identifier, cardCollection);
    }

    public void DrawCards(string from, string to, int quantity = 1)
    {
        if (!_cardCollections.TryGetValue(from, out var fromCollection) ||
            !_cardCollections.TryGetValue(to, out var toCollection) ||
            fromCollection.Cards.Count < quantity)
        {
            Debug.Log($"Not able to get [{quantity}] cards from [{from}] to [{to}]");
            return;
        }
        var cards = fromCollection.Pop(quantity);
        toCollection.Push(cards);
          




    }
}
