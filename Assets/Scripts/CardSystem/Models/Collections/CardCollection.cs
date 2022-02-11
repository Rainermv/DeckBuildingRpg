using System;
using System.Collections.Generic;

namespace Assets.Scripts.CardSystem.Models.Collections
{
    public class CardCollection
    {
        public List<Card> Cards = new();
        public CardCollectionIdentifier CollectionIdentifier { get; set; }

        public Action<List<Card>> OnCardListUpdate { get; set; }
        public int CardsCount => Cards.Count;
        public CardPlayer CardPlayerParent { get; set; }


        public static CardCollection Make()
        {
            return new CardCollection()
            {
                Cards = new List<Card>(),
            };
        }

        private CardCollection()
        {

        }

        public void OnUpdate()
        {
            OnCardListUpdate?.Invoke(Cards);
        }

        public void InsertCards(List<Card> cards, int index = 0)
        {
            Cards.InsertRange(index, cards);
            foreach (var card in cards)
            {
                card.CardCollectionParent = this;
            }

            OnUpdate();
        }


        public IEnumerable<Card> Pop(int quantity)
        {
            var cards = Cards.GetRange(0, quantity);
            Cards.RemoveRange(0, quantity);

            OnUpdate();

            return cards;
        }


        public bool RemoveCard(Card card)
        {
            var remove = Cards.Remove(card);

            OnUpdate();
            
            return remove;
        }

        
    }
}