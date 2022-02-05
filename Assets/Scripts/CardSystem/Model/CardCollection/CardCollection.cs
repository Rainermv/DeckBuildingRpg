using System;
using System.Collections.Generic;
using Assets.Scripts.CardSystem;

public class CardCollection
{
    internal List<Card> Cards { get; set; }
    public Action OnUpdate { get; set; }
    public int CollectionType { get; set; }

    public static CardCollection Make(List<Card> cards)
    {

        return new CardCollection()
        {
            Cards = cards,
        };
    }

    public class Types
    {
        public const int DECK = 0;
        public const int HAND = 1;
    }

    public IEnumerable<Card> Pop(int quantity)
    {
        var cards = Cards.GetRange(0, quantity);
        Cards.RemoveRange(0, quantity);

        OnUpdate?.Invoke();

        return cards;
    }

    public void Push(IEnumerable<Card> cards)
    {
        Cards.AddRange(cards);

        OnUpdate?.Invoke();
    }
}