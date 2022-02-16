using System.Collections.Generic;
using Assets.Scripts.CardSystem.Models.Attributes;
using Assets.Scripts.CardSystem.Models.Collections;

namespace Assets.Scripts.CardSystem.Models
{
    public class CardPlayer
    {
        public string Name { get; set; }

        public Dictionary<CardCollectionIdentifier, CardCollection> CardCollections { get; set; } = new();
        public AttributeSet AttributeSet { get; set; }


        public static CardPlayer Make(string playerName)
        {
            return new CardPlayer()
            {
                Name = playerName,
                AttributeSet = new AttributeSet()
            };
        }


        private CardPlayer(){}

        public void AddNewCardCollection(CardCollectionIdentifier identifier)
        {
            var cardCollection = CardCollection.Make();
            cardCollection.CollectionIdentifier = identifier;
            cardCollection.CardPlayerParent = this;
            CardCollections.Add(identifier, cardCollection);
        }
    }
}