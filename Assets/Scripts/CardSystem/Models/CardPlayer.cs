using System.Collections.Generic;
using Assets.Scripts.CardSystem.Model.Collection;

namespace Assets.Scripts.CardSystem.Model
{
    public class CardPlayer
    {
        public string Name { get; set; }

        public Dictionary<CardCollectionIdentifier, CardCollection> CardCollections { get; set; } = new();
        public Dictionary<string, Resource> Resources { get; set; } = new();


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

        public Resource AddNewResource(string name)
        {
            var resource = new Resource(name);
            Resources.Add(name, resource);
            return resource;
        }


    }
}