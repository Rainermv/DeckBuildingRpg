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
        private CardSystemModel _cardSystemModel = new();
        private ICardRuleset _cardRuleset;


        public CardSystemController(ICardRuleset cardRuleset)
        {
            _cardRuleset = cardRuleset;
        }


        public CardSystemModel Setup(CardSystemModel cardSystemModel)
        {
            _cardSystemModel = cardSystemModel;

            _cardRuleset.SetupSystem(cardSystemModel);

            foreach (var cardPlayer in _cardSystemModel.CardPlayers.Values)
            {
                _cardRuleset.SetupPlayer(cardPlayer);

                foreach (var cardCollection in cardPlayer.CardCollections.Values)
                {
                    _cardRuleset.SetupCollection(cardCollection);

                    foreach (var card in cardCollection.Cards)
                    {
                       _cardRuleset.SetupCard(card);
                    }

                }
            }

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
            var from = card.CardCollectionParent;

            if (card.CardCollectionParent.RemoveCard(card))
            {
                to.InsertCards(new List<Card>(){card}, 0);
                Debug.Log($"MOVE [{card.Name}] from [{from}] to [{to}]");
            }


        }

    }
}
