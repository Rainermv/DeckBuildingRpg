using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Model.AttributeModel;

namespace Assets.Scripts.Core.Model.Cards.Collections
{
    public class CardCollectionModel
    {
        public List<Cards.Card> Cards = new();
        public CardCollectionIdentifier CollectionIdentifier { get; set; }

        public int CardsCount => Cards.Count;
        public Player PlayerParent { get; set; }
        public Action<CardCollectionModel> OnCardCollectionCardListUpdate { get; set; }


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

        public void OnCardListUpdate()
        {
            OnCardCollectionCardListUpdate?.Invoke(this);
        }

        public void InsertCards(List<Cards.Card> cards, int index = 0)
        {
            Cards.InsertRange(index, cards);
            foreach (var card in cards)
            {
                card.CardCollectionModelParent = this;
            }

            OnCardListUpdate();
        }


        public IEnumerable<Cards.Card> Draw(int quantity)
        {
            var cards = Cards.GetRange(0, quantity);
            Cards.RemoveRange(0, quantity);

            OnCardListUpdate();

            return cards;
        }


        public bool RemoveCard(Cards.Card card)
        {
            var remove = Cards.Remove(card);

            OnCardListUpdate();
            
            return remove;
        }

        
    }
}