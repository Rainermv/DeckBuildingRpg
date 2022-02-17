using System;
using Assets.Scripts.Controller;
using Assets.Scripts.Model.CardModel;
using Assets.Scripts.Model.GridModel;
using UnityEngine;

namespace Assets.Scripts.Model.CharacterModel
{
    public class Character : IEntity
    {
        public static Character Make(string name, GridPosition gridPosition, Player owner)
        {
            return new Character()
            {
                _name = name,
                _gridPosition = gridPosition,
                _owner = owner
            };
        }

        private GridPosition _gridPosition;
        private string _name;
        private Player _owner;

        private Character()
        {
            
        }

        public GridPosition GridPosition
        {
            get => _gridPosition;
            set
            {
                _gridPosition = value;
                OnUpdate?.Invoke(this);
            }
        }

        public Action<Character> OnUpdate { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnUpdate?.Invoke(this);
            }
        }

        public Player Owner
        {
            get => _owner;
            set
            {
                _owner = value;
                OnUpdate?.Invoke(this);
            }
        }

    }
}