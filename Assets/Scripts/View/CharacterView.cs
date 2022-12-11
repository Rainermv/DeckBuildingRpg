using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Scripts.Core.Model.EntityModel;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts.View
{

    internal class CharacterView : SerializedMonoBehaviour
    {

        [SerializeField, ChildGameObjectsOnly]
        private Dictionary<int, TMPro.TextMeshPro> attributeContainers;
        

        public void Initialize(Entity entity)
        {

            transform.position = new Vector2(-1f, -0f);

            entity.OnEntityUpdate += OnEntityUpdate;

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
        }

       


    }
}