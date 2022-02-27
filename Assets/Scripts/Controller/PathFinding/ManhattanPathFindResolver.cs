using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.Model.GridMap;

namespace Assets.Scripts.Controller.MovementResolver
{
    internal class ManhattanPathFindResolver : IPathFindResolver
    {

        public Func<GridPosition, bool> OnIsPositionValid { get; set; }
        public Func<GridPosition, GridPosition, double> OnGetCostToCrossAtoB { get; set; }


        public PathFindResult FindPathToTarget(GridPosition initialPosition, GridPosition targetGridPosition)
        {
            var movementPathPositions = new List<GridPosition>();
            for (var i = 1; i <= Math.Abs(targetGridPosition.X - initialPosition.X); i++)
            {
                if (targetGridPosition.X - initialPosition.X > 0)
                {
                    var movePos = new GridPosition(initialPosition.X + i, initialPosition.Y);

                    movementPathPositions.Add(movePos);
                    continue;
                }

                movementPathPositions.Add(new GridPosition(initialPosition.X - i, initialPosition.Y));
            }

            initialPosition = movementPathPositions.LastOrDefault() ?? initialPosition;

            for (var i = 1; i <= Math.Abs(targetGridPosition.Y - initialPosition.Y); i++)
            {
                if (targetGridPosition.Y - initialPosition.Y > 0)
                {
                    movementPathPositions.Add(new GridPosition(initialPosition.X, initialPosition.Y + i));
                    continue;
                }

                movementPathPositions.Add(new GridPosition(initialPosition.X, initialPosition.Y - i));
            }

            return new PathFindResult()
            {
                PathFound = true,
                MovementPathPositions = movementPathPositions
            };
        }

    }
}