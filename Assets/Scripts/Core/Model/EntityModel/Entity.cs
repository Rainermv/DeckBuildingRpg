using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assets.Scripts.Core.Model.AttributeModel;
using Assets.Scripts.Core.Model.Cards;
using Assets.Scripts.Core.Model.GridMap;

namespace Assets.Scripts.Core.Model.EntityModel
{
    public class Entity : ITargetable
    {
        private string _name;
        private Player _owner;

        public Attributes Attributes { get; set; } = new();
        public GridPosition GridPosition { get; private set; }

        public Action<Entity> OnEntityUpdate { get; set; }
        public Action<Entity> OnEntitySetPosition { get; set; }
        public  Action<Entity> OnEntityFinishedMovePath { get; set; }

        public static Entity Make(string name, GridPosition gridPosition, Player owner)
        {
            return new Entity()
            {
                _name = name,
                GridPosition = gridPosition,
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

        public int MovementRange { get; set; }

        public void SetPosition(GridPosition gridPosition)
        {
            GridPosition = gridPosition;
            OnEntitySetPosition?.Invoke(this);
        }

        public async Task SetPositionAsync(GridPosition gridPosition)
        {
            GridPosition = gridPosition;
            OnEntitySetPosition(this);
        }

        public async Task MovePathAsync(List<GridPosition> movePositions, int delayTime)
        {
            foreach (var movePosition in movePositions)
            {
                await SetPositionAsync(movePosition);
                await Task.Delay(delayTime);
            }

            OnEntityFinishedMovePath?.Invoke(this);

        }
    }
}