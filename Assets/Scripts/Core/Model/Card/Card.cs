using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.Commands;
using Assets.Scripts.Core.Model.AttributeModel;
using Assets.Scripts.Core.Model.Card.Collections;

namespace Assets.Scripts.Core.Model.Card
{
    public class Card 
    {
        public CardData CardData { get; private set; }

        public string Name => CardData.Name;
        public int CardDataIndex => CardData.Index;
        public string Text => CardData.Text;
        
        public CardCollectionModel CardCollectionModelParent { get; set; }
        public AttributeSet AttributeSet { get; set; }
        
        public Action OnUpdate { get; set; }

        public static Card Make(CardData cardData)
        {
            return new Card()
            {
                CardData = cardData,
                AttributeSet = new AttributeSet()
            };
        }
        private Card() {}



    }
}