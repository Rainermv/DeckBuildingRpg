using System;
using Assets.Scripts.Core.Model.GridMap;

namespace Assets.Scripts.Controller.MovementResolver
{
    public interface IPathFindResolver
    {
        Func<GridPosition, bool> OnIsPositionValid { get; set; }
        Func<GridPosition, GridPosition, double> OnGetCostToCrossAtoB { get; set; }


        public PathFindResult FindPathToTarget(GridPosition initialPosition, GridPosition targetGridPosition);



    }
}