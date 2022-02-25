using System.Collections.Generic;
using Assets.Scripts.Core.Model.AttributeModel;
using Assets.Scripts.Core.Model.Card.Collections;
using Assets.Scripts.Core.Model.Command;

namespace Assets.Scripts.Core.Model.Card
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

        public CardCollectionModel AddNewCardCollection(CardCollectionIdentifier identifier)
        {
            var cardCollection = CardCollectionModel.Make();
            cardCollection.CollectionIdentifier = identifier;
            cardCollection.PlayerParent = this;
            CardCollections.Add(identifier, cardCollection);

            return cardCollection;
        }
    }
}