using System;
using Assets.Scripts.Controller;
using Assets.Scripts.Model.CharacterModel;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace Assets.Scripts
{

    internal class CharacterView : MonoBehaviour
    {
        Func<GridPosition, Vector3> _onGetWorldPosition;
        private Character _character;


        public void Initialize(Character character, Func<GridPosition, Vector3> onGetWorldPosition)
        {
            _onGetWorldPosition = onGetWorldPosition;

            OnCharacterUpdate(character);
        }

        private void OnCharacterUpdate(Character character)
        {
            _character = character;


            character.OnUpdate = OnCharacterUpdate;

            name = character.Name;
            transform.position = _onGetWorldPosition(character.GridPosition);

            Debug.Log($"{character.Name} moved to {character.GridPosition} ({transform.position})");
        }

        private void MoveTo(Vector3 position)
        {
            transform.position = position;
        }
    }
}