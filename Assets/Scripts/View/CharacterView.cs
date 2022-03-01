using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Scripts.Core.Model.AttributeModel;
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

        public void Initialize(Entity entity, Func<GridPosition, Vector3> onGetWorldPosition)
        {
            _onGetWorldPosition = onGetWorldPosition;

            entity.OnEntityUpdate += OnEntityUpdate;
            entity.OnEntitySetPosition += OnSetPositionMoveAsync;

            entity.Attributes.OnAttributeValueChange = (index, value) =>
            {
                if (attributeContainers.TryGetValue(index, out var container))
                {
                    container.text = value.ToString();
                }
            };

            OnEntityUpdate(entity);
            
        }

        private void OnEntityUpdate(Entity entity)
        {
            name = entity.Name;
            transform.position = _onGetWorldPosition(entity.GridPosition);
        }

        private void OnSetPositionInstant(GridPosition gridPosition)
        {
            transform.position = _onGetWorldPosition(gridPosition);
        }

        private async void OnSetPositionMoveAsync(Entity entity)
        {
            var initialPosition = transform.position;
            var targetPosition = _onGetWorldPosition(entity.GridPosition);
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