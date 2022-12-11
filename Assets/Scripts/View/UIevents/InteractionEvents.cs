using System;
using Assets.Scripts.Core.Model.Cards;
using UnityEngine.EventSystems;

namespace Assets.Scripts.View.Cards
{
    public static class InteractionEvents
    {
        public static Action<CardView, Card, PointerEventData, int> OnCardPointerUIEvent { get; set; }
        public static Action<PointerEventData, int> OnTilemapPointerEvent { get; set; }

        public static Action<CardView> OnCardBeingPlayedEvent { get; set; }

    }
}