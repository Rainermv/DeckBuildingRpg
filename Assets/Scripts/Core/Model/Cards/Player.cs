using System.Collections.Generic;
using Assets.Scripts.Core.Model.AttributeModel;
using Assets.Scripts.Core.Model.Cards.Collections;

namespace Assets.Scripts.Core.Model.Cards
{
    public class Player : ITargetable
    {
        public string Name { get; set; }

        public Dictionary<CardCollectionIdentifier, CardCollectionModel> CardCollections { get; set; } = new();
        public Attributes Attributes { get; set; }
        
        public static Player Make(string playerName)
        {
            return new Player()
            {
                Name = playerName,
                Attributes = new Attributes()
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