using System;
using Assets.Scripts.Core.Model.Cards;
using UnityEngine.EventSystems;

namespace Assets.Scripts.View.Cards
{
    public static class UIEvents
    {
        public static Action<Card, PointerEventData, int> OnCardPointerUIEvent { get; set; }
        public static Action<PointerEventData, int> OnTilemapPointerEvent { get; set; }
    }
}