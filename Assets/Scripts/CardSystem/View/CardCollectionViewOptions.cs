using System;
using Assets.Scripts.CardSystem.Model.CardCollection;
using UnityEngine;

namespace Assets.Scripts.CardSystem.View
{
    public class CardCollectionViewOptions
    {
        private const float Distance = 100f;


        public static CardCollectionViewOptions MakeDeck(RectTransform rectTransform)
        {
            return new CardCollectionViewOptions()
            {
                RectPosition = Vector2.up * Distance,
                Title = "Deck",
                DisplayCards = false,
            };
        }

        public static CardCollectionViewOptions MakeHand(RectTransform rectTransform)
        {
            return new CardCollectionViewOptions()
            {
                RectPosition = Vector2.down * Distance,
                Title = "Hand",
                DisplayCards = true,
            };
        }
        

        public string Title { get; set; }
        public bool DisplayCards { get; set; }
        public Vector2 RectPosition { get; set; }
    }
}