using System.Collections.Generic;
using Assets.Scripts.Model.Actor;
using Assets.Scripts.Model.GridMap;

namespace Assets.Scripts.Controller.MovementResolver
{
    internal interface IGridmovementResolver
    {
        public List<GridPosition> MakeMoveSequence(Entity movableEntity, GridPosition targetGridPosition);

    }
}