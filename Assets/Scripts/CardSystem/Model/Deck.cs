using System;
using System.Collections.Generic;
using Assets.Scripts.CardSystem;

public class Deck
{
    internal List<Card> Cards { get; set; }
    public Action OnUpdate { get; set; }

    public static Deck Make(List<Card> cards)
    {
        return new Deck()
        {
            Cards = cards
        };
    }

    
}