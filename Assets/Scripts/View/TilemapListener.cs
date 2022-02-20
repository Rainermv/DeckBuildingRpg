using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.View
{
    public class TilemapListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler, IPointerExitHandler
    {
        public Action<PointerEventData, int> OnTilemapPointerEvent;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            OnTilemapPointerEvent?.Invoke(eventData, PointerEventTrigger.DOWN);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnTilemapPointerEvent?.Invoke(eventData, PointerEventTrigger.UP);
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            OnTilemapPointerEvent?.Invoke(eventData, PointerEventTrigger.MOVE);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnTilemapPointerEvent?.Invoke(eventData, PointerEventTrigger.EXIT);
        }
    }
}