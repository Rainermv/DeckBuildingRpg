using System;
using Assets.Scripts.CardSystem;
using Assets.Scripts.CardSystem.Model;
using Assets.Scripts.CardSystem.Model.Collection;

namespace Assets.Scripts
{
    internal class StandardCardRuleset : ICardRuleset
    {
        private ICollectionShuffler _collectionShuffler;

        public StandardCardRuleset(ICollectionShuffler collectionShuffler)
        {
            _collectionShuffler = collectionShuffler;
        }

        public void SetupSystem(CardSystemModel cardSystemModel)
        {
        }

        public void SetupPlayer(CardPlayer cardPlayer)
        {
            cardPlayer.AttributeSet.Set(PlayerAttributeNames.Power, 10);
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

        public void SetupCard(Card card)
        {

        }
    }
}