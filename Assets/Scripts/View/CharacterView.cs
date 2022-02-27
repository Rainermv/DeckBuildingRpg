using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Scripts.Core.Model.EntityModel;
using Assets.Scripts.Core.Model.GridMap;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts.View
{

    internal class CharacterView : SerializedMonoBehaviour
    {

        [SerializeField, ChildGameObjectsOnly]
        private Dictionary<int, TMPro.TextMeshPro> attributeContainers;
        
        private Func<GridPosition, Vector3> _onGetWorldPosition;

        private Entity _lastEntity;
        //private GridPosition _gridPosition;

        public void Initialize(Entity entity, Func<GridPosition, Vector3> onGetWorldPosition)
        {
            _onGetWorldPosition = onGetWorldPosition;

            OnEntityUpdate(entity);
        }

        private void OnEntityUpdate(Entity entity)
        {
            if ( _lastEntity != null)
            {
                _lastEntity.OnUpdate -= OnEntityUpdate;
                _lastEntity.OnSetPosition -= OnSetPositionMoveAsync;
            }
            _lastEntity = entity;


            entity.OnUpdate += OnEntityUpdate;
            entity.OnSetPosition += OnSetPositionMoveAsync;

            entity.AttributeSet.OnAttributeValueChange = (index, value) =>
            {
                if (attributeContainers.TryGetValue(index, out var container))
                {
                    container.text = value.ToString();
                }
            };



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