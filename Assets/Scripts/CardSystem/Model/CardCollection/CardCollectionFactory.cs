using System.Collections.Generic;
using Assets.Scripts.CardSystem;

public class CardCollectionFactory
{
    public static CardCollection MakeDeck(List<Card> cards)
    {
        return new CardCollection()
        {
            Cards = cards
        };
    }
}