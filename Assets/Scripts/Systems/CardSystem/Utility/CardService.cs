using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.CardSystem.Models;
using Assets.Scripts.CardSystem.Models.Collections;
using UnityEngine;

namespace Assets.Scripts.CardSystem.Utility
{
    public static class CardService
    {
        public static void DrawCards(CardCollection from,
            CardCollection to, int quantity = 1)
        {

            if (from.CardsCount < quantity)
            {
                Debug.Log($"FAILED DRAW [{quantity}] cards from [{from.CollectionIdentifier}] to [{to.CollectionIdentifier}]");
                return;
            }

            var cards = from.Pop(quantity).ToList();
            to.InsertCards(cards); // insert to top

            Debug.Log($"DRAW [{string.Join(",", cards.Select(c => c.Name))}] from [{from.CollectionIdentifier}] to [{to.CollectionIdentifier}]");

            // Do card animation here
        }

        public static void MoveCardTo(Card card, CardCollection to)
        {
            var from = card.CardCollectionParent;

            if (card.CardCollectionParent.RemoveCard(card))
            {
                to.InsertCards(new List<Card>(){card}, 0);
                Debug.Log($"MOVE [{card.Name}] from [{from.CollectionIdentifier}] to [{to.CollectionIdentifier}]");
            }


        }

    }
}
