using System;
using System.Collections.Generic;

namespace Assets.Scripts.CardSystem.Model.CardCollection
{
    public class CardCollection
    {
        internal List<Card> Cards { get; set; }
        public CardCollectionIdentifier CollectionIdentifier { get; set; }

        public Action OnCardListUpdate { get; set; }


        public static CardCollection Make(CardCollectionIdentifier collectionIdentifier, List<Card> cards = null)
        {
            return new CardCollection()
            {
                Cards = cards?? new List<Card>(),
                CollectionIdentifier = collectionIdentifier
            };
        }

        public IEnumerable<Card> Pop(int quantity)
        {
            var cards = Cards.GetRange(0, quantity);
            Cards.RemoveRange(0, quantity);

            OnCardListUpdate();

            return cards;
        }

        public void Push(IEnumerable<Card> cards)
        {
            Cards.AddRange(cards);

            OnCardListUpdate();
        }
    }
}