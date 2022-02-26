using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assets.Scripts.Core.Model.AttributeModel;
using Assets.Scripts.Core.Model.Card;
using Assets.Scripts.Core.Model.Command;
using Assets.Scripts.Core.Model.GridMap;

namespace Assets.Scripts.Core.Model.Entity
{
    public class Entity : ITargetable
    {
        private string _name;
        private Player _owner;

        public AttributeSet AttributeSet { get; set; } = new();
        public GridPosition GridPosition { get; private set; }

        public Action<Entity> OnUpdate { get; set; }
        public Action<GridPosition> OnSetPosition { get; set; }
        public Action<GridPosition> OnFinishedMovePath { get; set; }
        

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

        public int MovementRange { get; set; }

        public void SetPosition(GridPosition gridPosition)
        {
            GridPosition = gridPosition;
            OnSetPosition?.Invoke(gridPosition);
        }

        public async Task SetPositionAsync(GridPosition gridPosition)
        {
            GridPosition = gridPosition;
            OnSetPosition(gridPosition);
        }

        public async Task MovePathAsync(List<GridPosition> movePositions, int delayTime)
        {
            foreach (var movePosition in movePositions)
            {
                await SetPositionAsync(movePosition);
                await Task.Delay(delayTime);
            }

            OnFinishedMovePath?.Invoke(movePositions.LastOrDefault());

        }
    }
}