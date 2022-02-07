using System;
using System.Collections.Generic;

namespace Assets.Scripts.CardSystem.Model.CardCollection
{
    public class CardCollection
    {
        internal List<Card> Cards { get; set; }
        public Action OnUpdate { get; set; }


        public static CardCollection Make(List<Card> cards)
        {
            return new CardCollection()
            {
                Cards = cards,
            };
        }
        

        public IEnumerable<Card> Pop(int quantity)
        {
            var cards = Cards.GetRange(0, quantity);
            Cards.RemoveRange(0, quantity);

            OnUpdate();

            return cards;
        }

        public void Push(IEnumerable<Card> cards)
        {
            Cards.AddRange(cards);

            OnUpdate();
        }
    }
}