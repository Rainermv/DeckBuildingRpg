using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.View
{
    public class TilemapListener : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler
    {
        private Action<Vector3> _onTilemapPointerMoveWorldPosition;
        private Action<Vector3> _onTilemapPointerDownWorldPosition;

        public void Initialize(Action<Vector3> onTilemapPointerMove, Action<Vector3> onTilemapPointerDown)
        {
            _onTilemapPointerDownWorldPosition = onTilemapPointerDown;
            _onTilemapPointerMoveWorldPosition = onTilemapPointerMove;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _onTilemapPointerDownWorldPosition(eventData.pointerCurrentRaycast.worldPosition);
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            var hovered = eventData.hovered;
            if (!hovered.Any())
            {
                return;
            }

            //_onTilemapPointerMoveWorldPosition(hovered.FirstOrDefault().transform.position);
            _onTilemapPointerMoveWorldPosition(eventData.pointerCurrentRaycast.worldPosition);
        }

        
    }
}