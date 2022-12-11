using Assets.Scripts.Core.Model.Cards;
using Assets.Scripts.View.Cards;
using UnityEngine;

namespace Assets.Scripts.View.CardInteraction
{
    public class CardInteractionStateModel
    {
        
        public bool PointerOver { get; set; }
        public Card Card { get; set; }
        public bool PointerDown { get; set; }
        public Vector2 PointerPosition { get; set; }
        public CardView CardView { get; set; }
        public bool InPlayArea { get; set; }
    }
}