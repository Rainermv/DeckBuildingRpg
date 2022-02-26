using System;
using System.Threading.Tasks;
using Assets.Scripts.Core.Model.Entity;
using Assets.Scripts.Core.Model.GridMap;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts.View
{

    internal class CharacterView : MonoBehaviour
    {
    
        private Func<GridPosition, Vector3> _onGetWorldPosition;
        //private GridPosition _gridPosition;

        public void Initialize(Entity entity, Func<GridPosition, Vector3> onGetWorldPosition)
        {
            _onGetWorldPosition = onGetWorldPosition;

            OnEntityUpdate(entity);
        }

        private void OnEntityUpdate(Entity entity)
        {
            entity.OnUpdate += OnEntityUpdate;
            entity.OnSetPosition += OnSetPositionMoveAsync;

            name = entity.Name;

            transform.position = _onGetWorldPosition(entity.GridPosition);
        }

        private void OnSetPositionInstant(GridPosition gridPosition)
        {
            transform.position = _onGetWorldPosition(gridPosition);
        }

        private async void OnSetPositionMoveAsync(GridPosition gridPosition)
        {
            var initialPosition = transform.position;
            var targetPosition = _onGetWorldPosition(gridPosition);
            float t = 0;

            while (t < 1)
            {
                transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
                t += Time.deltaTime * 10;

                await Task.Yield();
            }
        }


    }
}