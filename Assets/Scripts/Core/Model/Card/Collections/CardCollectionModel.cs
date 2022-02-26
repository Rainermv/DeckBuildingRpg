using System;
using System.Collections.Generic;

namespace Assets.Scripts.Core.Model.Card.Collections
{
    public class CardCollectionModel
    {
        public List<Card> Cards = new();
        public CardCollectionIdentifier CollectionIdentifier { get; set; }

        public Action<List<Card>> OnCardListUpdate { get; set; }
        public int CardsCount => Cards.Count;
        public Player PlayerParent { get; set; }


        public static CardCollectionModel Make()
        {
            return new CardCollectionModel()
            {
                Cards = new List<Card>(),
            };
        }

        private CardCollectionModel()
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
                card.CardCollectionModelParent = this;
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