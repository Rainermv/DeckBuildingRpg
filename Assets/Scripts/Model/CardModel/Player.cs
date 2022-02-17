using System.Collections.Generic;
using Assets.Scripts.CardSystem.Models.Attributes;
using Assets.Scripts.Model.CardModel.Collections;
using Assets.Scripts.Model.CardModel.Commands;
using Assets.Scripts.Model.CharacterModel;

namespace Assets.Scripts.Model.CardModel
{
    public class Player : ITargetable
    {
        public string Name { get; set; }

        public Dictionary<CardCollectionIdentifier, CardCollection> CardCollections { get; set; } = new();
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
            var cardCollection = CardCollection.Make();
            cardCollection.CollectionIdentifier = identifier;
            cardCollection.PlayerParent = this;
            CardCollections.Add(identifier, cardCollection);
        }
    }
}