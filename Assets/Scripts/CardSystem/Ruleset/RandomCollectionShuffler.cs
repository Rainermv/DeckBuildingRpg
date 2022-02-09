using System;
using System.Collections.Generic;
using Assets.Scripts.CardSystem;
using Assets.Scripts.CardSystem.Model;
using Assets.Scripts.CardSystem.Model.Collection;

namespace Assets.Scripts
{
    internal class RandomCollectionShuffler : ICollectionShuffler
    {
        public List<Card> Run(List<Card> cards)
        {
            var random = new Random();

            //1. For each unshuffled item in collection
            for (var i = cards.Count -1; i > 0; --i)
            {
                //2.Randomly pick an item which has not been shuffled
                var j = random.Next(i + 1);

                //3. Swap the selected item with the last "unstruck" item in the collection
                (cards[i], cards[j]) = (cards[j], cards[i]);
            }

            return cards;

        }
    }
}