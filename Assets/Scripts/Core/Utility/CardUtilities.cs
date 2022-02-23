using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.Model.Card;
using Assets.Scripts.Core.Model.Card.Collections;

namespace Assets.Scripts.Core.Utility
{
    public static class CardUtilities
    {
        public static void DrawCards(CardCollectionModel from,
            CardCollectionModel to, int quantity = 1)
        {

            if (from.CardsCount < quantity)
            {
                return;
            }

            var cards = from.Pop(quantity).ToList();
            to.InsertCards(cards); // insert to top


            // Do card animation here
        }

        public static void MoveCardTo(CardModel cardModel, CardCollectionModel to)
        {
            var from = cardModel.CardCollectionModelParent;

            if (cardModel.CardCollectionModelParent.RemoveCard(cardModel))
            {
                to.InsertCards(new List<CardModel>(){cardModel}, 0);
            }


        }

    }
}
