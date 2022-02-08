using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assets.Scripts.CardSystem.Model;
using Assets.Scripts.CardSystem.Model.Collection;
using UnityEngine;

namespace Assets.Scripts.CardSystem
{
    public class CardSystemController
    {
        private CardSystemModel _cardSystemModel = new CardSystemModel();


        public CardSystemModel Initialize(CardSystemModel cardSystemModel)
        {
            _cardSystemModel = cardSystemModel;
            // shuffle cards,
            // do other stuff
            return _cardSystemModel;
        }


        public async Task DrawCards(CardPlayer cardPlayer, CardCollection from,
            CardCollection to, int quantity = 1)
        {

            if (from.CardsCount < quantity)
            {
                Debug.Log($"FAILED DRAW [{quantity}] cards from [{from}] to [{to}]");
                return;
            }

            var cards = from.Pop(quantity).ToList();
            to.InsertCards(cards); // insert to top

            Debug.Log($"DRAW [{string.Join(",", cards.Select(c => c.Name))}] from [{from}] to [{to}]");

            // Do card animation here
        }

        public async Task MoveCardTo(Card card, CardCollection to)
        {
            var from = card.Collection;

            if (card.Collection.RemoveCard(card))
            {
                to.InsertCards(new List<Card>(){card}, 0);
                Debug.Log($"MOVE [{card.Name}] from [{from}] to [{to}]");
            }


        }
    }
}
