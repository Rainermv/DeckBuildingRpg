using System;
using Assets.Scripts.CardSystem;
using Assets.Scripts.CardSystem.Model;
using Assets.Scripts.CardSystem.Model.Collection;

namespace Assets.Scripts
{
    internal class CardRuleset : ICardRuleset
    {
        private ICollectionShuffler _collectionShuffler;

        public CardRuleset(ICollectionShuffler collectionShuffler)
        {
            _collectionShuffler = collectionShuffler;
        }

        public void SetupSystem(CardSystemModel cardSystemModel)
        {
        }

        public void SetupPlayer(CardPlayer cardPlayer)
        {
        }

        public void SetupCollection(CardCollection cardCollection)
        {
            switch (cardCollection.CollectionIdentifier)
            {
                case CardCollectionIdentifier.Deck:

                    cardCollection.Cards = _collectionShuffler.Run(
                        cardCollection.Cards);

                    break;
                case CardCollectionIdentifier.Hand:
                    break;
                case CardCollectionIdentifier.Discard:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }
    }
}