using System;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class TilemapListener : MonoBehaviour
    {
        private Action _onTilemapMouseEnter;
        private Action _onTilemapMouseExit;
        private Action _onTilemapMouseOver;

        public void Initialize(Action onTilemapMouseEnter, Action onTilemapMouseExit, Action onTilemapMouseOver)
        {
            _onTilemapMouseEnter = onTilemapMouseEnter;
            _onTilemapMouseExit = onTilemapMouseExit;
            _onTilemapMouseOver = onTilemapMouseOver;
        }

        void OnMouseEnter()
        {
            _onTilemapMouseEnter?.Invoke();
        }

        void OnMouseExit()
        {
            _onTilemapMouseExit?.Invoke();
        }

        void OnMouseOver()
        {
            _onTilemapMouseOver?.Invoke();
        }
    }
}