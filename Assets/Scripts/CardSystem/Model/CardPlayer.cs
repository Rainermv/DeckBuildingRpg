using System.Collections.Generic;
using Assets.Scripts.CardSystem.Model.Collection;

namespace Assets.Scripts.CardSystem.Model
{
    public class CardPlayer
    {
        public Dictionary<CardCollectionIdentifier, CardCollection> CardCollections { get; private set; } = new();
        public string Name { get; set; }


        public static CardPlayer Make(string playerName)
        {
            return new CardPlayer()
            {
                Name = playerName
            };
        }


        private CardPlayer(){}

        public void AddNewCollection(CardCollectionIdentifier identifier)
        {
            var cardCollection = CardCollection.Make();
            cardCollection.CollectionIdentifier = identifier;
            cardCollection.CardPlayer = this;
            CardCollections.Add(identifier, cardCollection);

        }
    }
}