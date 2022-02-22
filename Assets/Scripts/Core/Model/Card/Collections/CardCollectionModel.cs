using System;
using System.Collections.Generic;

namespace Assets.Scripts.Core.Model.Card.Collections
{
    public class CardCollectionModel
    {
        public List<CardModel> Cards = new();
        public CardCollectionIdentifier CollectionIdentifier { get; set; }

        public Action<List<CardModel>> OnCardListUpdate { get; set; }
        public int CardsCount => Cards.Count;
        public Player PlayerParent { get; set; }


        public static CardCollectionModel Make()
        {
            return new CardCollectionModel()
            {
                Cards = new List<CardModel>(),
            };
        }

        private CardCollectionModel()
        {

        }

        public void OnUpdate()
        {
            OnCardListUpdate?.Invoke(Cards);
        }

        public void InsertCards(List<CardModel> cards, int index = 0)
        {
            Cards.InsertRange(index, cards);
            foreach (var card in cards)
            {
                card.CardCollectionModelParent = this;
            }

            OnUpdate();
        }


        public IEnumerable<CardModel> Pop(int quantity)
        {
            var cards = Cards.GetRange(0, quantity);
            Cards.RemoveRange(0, quantity);

            OnUpdate();

            return cards;
        }


        public bool RemoveCard(CardModel cardModel)
        {
            var remove = Cards.Remove(cardModel);

            OnUpdate();
            
            return remove;
        }

        
    }
}