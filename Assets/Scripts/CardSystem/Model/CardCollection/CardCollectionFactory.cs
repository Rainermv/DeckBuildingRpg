using System.Collections.Generic;

namespace Assets.Scripts.CardSystem.Model.CardCollection
{
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
}