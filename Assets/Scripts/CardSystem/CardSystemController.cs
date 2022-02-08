using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assets.Scripts.CardSystem.Model.CardCollection;
using UnityEngine;

namespace Assets.Scripts.CardSystem
{
    public class CardSystemController
    {

        private Dictionary<CardCollectionIdentifier, CardCollection> _cardCollections { get; set; } = new();


        public Dictionary<CardCollectionIdentifier, CardCollection> Initialize()
        {
            // shuffle cards,
            // do other stuff
            return _cardCollections;
        }


        public void AddCardCollection(CardCollection cardCollection)
        {
            _cardCollections.Add(cardCollection.CollectionIdentifier, cardCollection);
        }

        public async Task DrawCards(CardCollectionIdentifier from, CardCollectionIdentifier to, int quantity = 1)
        {
            if (!_cardCollections.TryGetValue(from, out var fromCollection) || fromCollection == null ||
                !_cardCollections.TryGetValue(to, out var toCollection) || toCollection == null ||
                fromCollection.Cards.Count < quantity)
            {
                Debug.Log($"FAILED DRAW [{quantity}] cards from [{from}] to [{to}]");
                return;
            }

            var cards = fromCollection.Pop(quantity).ToList();
            toCollection.Push(cards);

            Debug.Log($"DRAW [{string.Join(",", cards.Select(c => c.Name))}] from [{from}] to [{to}]");

            // Do card animation here



        }
    }
}
