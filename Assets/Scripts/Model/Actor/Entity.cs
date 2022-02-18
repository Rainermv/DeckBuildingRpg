using System;
using System.Threading.Tasks;
using Assets.Scripts.Model.Card;
using Assets.Scripts.Model.GridMap;

namespace Assets.Scripts.Model.Actor
{
    public class Entity 
    {
        public Action<Entity> OnUpdate { get; set; }
        public Func<GridPosition, Task> OnSetPositionAsync { get; set; }
        public Action<GridPosition> OnSetPosition { get; set; }


        public static Entity Make(string name, GridPosition gridPosition, Player owner)
        {
            return new Entity()
            {
                _name = name,
                GridPosition = gridPosition,
                _owner = owner
            };
        }

        private string _name;
        private Player _owner;

        private Entity()
        {
            
        }

        public GridPosition GridPosition { get; private set; }


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

        public void SetPosition(GridPosition gridPosition)
        {
            GridPosition = gridPosition;
            OnSetPosition?.Invoke(gridPosition);
        }

        public async Task SetPositionAsync(GridPosition gridPosition)
        {
            GridPosition = gridPosition;

            await OnSetPositionAsync(gridPosition);
        }
    }
}