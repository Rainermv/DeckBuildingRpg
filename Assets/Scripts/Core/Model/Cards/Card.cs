using System;
using Assets.Scripts.Core.Model.AttributeModel;
using Assets.Scripts.Core.Model.Cards.Collections;

namespace Assets.Scripts.Core.Model.Cards
{
    public class Card 
    {
        public CardData CardData { get; private set; }

        public string Name => CardData.Name;
        public int CardDataIndex => CardData.Index;
        public string Text => CardData.Text;
        
        public CardCollectionModel CardCollectionModelParent { get; set; }
        public Attributes Attributes { get; set; }
        
        public static Card Make(CardData cardData)
        {
            return new Card()
            {
                CardData = cardData,
                Attributes = new Attributes()
            };
        }
        private Card() {}



    }
}