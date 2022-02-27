using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Model.GridMap;

namespace Assets.Scripts.Controller.MovementResolver
{
    internal class SinglePathFindResolver : IPathFindResolver
    {
        public Func<GridPosition, bool> OnIsPositionValid { get; set; }
        public Func<GridPosition, GridPosition, double> OnGetCostToCrossAtoB { get; set; }

        public PathFindResult FindPathToTarget(GridPosition targetGridPosition, GridPosition initialPosition)
        {
            if (!OnIsPositionValid(targetGridPosition))
            {
                return new PathFindResult()
                {
                    PathFound = false
                };
            }

            return new PathFindResult()
            {
                PathFound = true,
                MovementPathPositions = new List<GridPosition>()
                {
                    targetGridPosition
                }
            };
        }
    }
}