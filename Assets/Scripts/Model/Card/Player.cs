using System.Collections.Generic;
using Assets.Scripts.Model.AttributeModel;
using Assets.Scripts.Model.Card.Collections;
using Assets.Scripts.Model.Card.Commands;

namespace Assets.Scripts.Model.Card
{
    public class Player : ITargetable
    {
        public string Name { get; set; }

        public Dictionary<CardCollectionIdentifier, CardCollectionModel> CardCollections { get; set; } = new();
        public AttributeSet AttributeSet { get; set; }
        
        public static Player Make(string playerName)
        {
            return new Player()
            {
                Name = playerName,
                AttributeSet = new AttributeSet()
            };
        }


        private Player(){}

        public void AddNewCardCollection(CardCollectionIdentifier identifier)
        {
            var cardCollection = CardCollectionModel.Make();
            cardCollection.CollectionIdentifier = identifier;
            cardCollection.PlayerParent = this;
            CardCollections.Add(identifier, cardCollection);
        }
    }
}