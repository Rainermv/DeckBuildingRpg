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

        public void Initialize(BattleEntity battleEntity, Func<GridPosition, Vector3> onGetWorldPosition)
        {
            _onGetWorldPosition = onGetWorldPosition;

            OnEntityUpdate(battleEntity);
        }

        private void OnEntityUpdate(BattleEntity battleEntity)
        {
            battleEntity.OnUpdate += OnEntityUpdate;
            battleEntity.OnSetPosition += OnSetPositionMoveAsync;

            name = battleEntity.Name;

            transform.position = _onGetWorldPosition(battleEntity.GridPosition);
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