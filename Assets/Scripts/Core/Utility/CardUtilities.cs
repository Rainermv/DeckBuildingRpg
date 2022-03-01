using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.Model.Cards;
using Assets.Scripts.Core.Model.Cards.Collections;

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

            var cards = from.Draw(quantity).ToList();
            to.InsertCards(cards); // insert to top


            // Do card animation here
        }

        public static void MoveCardTo(Card card, CardCollectionModel to)
        {
            var from = card.CardCollectionModelParent;

            if (card.CardCollectionModelParent.RemoveCard(card))
            {
                to.InsertCards(new List<Card>(){card}, 0);
            }


        }

    }
}
