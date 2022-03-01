using System;
using System.Linq;
using Assets.Scripts.View.Cards;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.View
{
    public class TilemapListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler, IPointerExitHandler, IPointerEnterHandler
    {
        
        public void OnPointerDown(PointerEventData eventData)
        {
            UIEvents.OnTilemapPointerEvent?.Invoke(eventData, PointerEventTrigger.DOWN);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            UIEvents.OnTilemapPointerEvent?.Invoke(eventData, PointerEventTrigger.UP);
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            UIEvents.OnTilemapPointerEvent?.Invoke(eventData, PointerEventTrigger.MOVE);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            UIEvents.OnTilemapPointerEvent?.Invoke(eventData, PointerEventTrigger.EXIT);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            UIEvents.OnTilemapPointerEvent?.Invoke(eventData, PointerEventTrigger.ENTER);
        }
    }
}