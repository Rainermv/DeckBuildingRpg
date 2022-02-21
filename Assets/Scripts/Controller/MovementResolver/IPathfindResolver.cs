using System;
using System.Collections.Generic;
using Assets.Scripts.Model.GridMap;
using Assets.Scripts.View;

namespace Assets.Scripts.Controller.MovementResolver
{
    internal interface IPathFindResolver
    {
        Func<GridPosition, bool> OnIsPositionValid { get; set; }
        Func<GridPosition, GridPosition, double> OnGetCostToCrossAtoB { get; set; }


        public PathFindResult FindPathToTarget(GridPosition initialPosition, GridPosition targetGridPosition);



    }
}