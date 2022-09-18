using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assets.Scripts.Core.Model.AttributeModel;
using Assets.Scripts.Core.Model.Cards;

namespace Assets.Scripts.Core.Model.EntityModel
{
    public class Entity : ITargetable
    {
        private string _name;
        private Player _owner;

        public Attributes Attributes { get; set; } = new();
        public Action<Entity> OnEntityUpdate { get; set; }

        public static Entity Make(string name, Player owner)
        {
            return new Entity()
            {
                _name = name,
                _owner = owner
            };
        }

        private Entity()
        {
            
        }



        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnEntityUpdate?.Invoke(this);
            }
        }

        public Player Owner
        {
            get => _owner;
            set
            {
                _owner = value;
                OnEntityUpdate?.Invoke(this);
            }
        }

    }
}