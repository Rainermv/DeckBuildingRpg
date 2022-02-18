using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Model.Card;
using Assets.Scripts.Model.Card.Collections;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public static class CardUtilities
    {
        public static void DrawCards(CardCollectionModel from,
            CardCollectionModel to, int quantity = 1)
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

        public static void MoveCardTo(CardModel cardModel, CardCollectionModel to)
        {
            var from = cardModel.CardCollectionModelParent;

            if (cardModel.CardCollectionModelParent.RemoveCard(cardModel))
            {
                to.InsertCards(new List<CardModel>(){cardModel}, 0);
                Debug.Log($"MOVE [{cardModel.Name}] from [{from.CollectionIdentifier}] to [{to.CollectionIdentifier}]");
            }


        }

    }
}
