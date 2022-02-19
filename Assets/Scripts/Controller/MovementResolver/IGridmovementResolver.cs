using System;
using System.Collections.Generic;
using Assets.Scripts.Model.Actor;
using Assets.Scripts.Model.GridMap;
using Assets.Scripts.View;

namespace Assets.Scripts.Controller.MovementResolver
{
    internal interface IGridmovementResolver
    {
        public FindPathResult FindPathToTarget(Entity movableEntity, GridPosition targetGridPosition,
            Func<GridPosition, bool> onGetPositionIsValid);

    }
}