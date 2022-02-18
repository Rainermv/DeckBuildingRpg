using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Model.Actor;
using Assets.Scripts.Model.GridMap;

namespace Assets.Scripts.Controller.MovementResolver
{
    internal class ManhattanMovementResolver : IGridmovementResolver
    {

        public List<GridPosition> MakeMoveSequence(Entity movableEntity, GridPosition targetGridPosition)
        {
            var initialPosition = movableEntity.GridPosition;

            var moveSequences = new List<GridPosition>();
            for (var i = 1; i <= Math.Abs(targetGridPosition.X - initialPosition.X); i++)
            {
                if (targetGridPosition.X - initialPosition.X > 0)
                {
                    moveSequences.Add(new GridPosition(initialPosition.X + i, initialPosition.Y));
                    continue;
                }

                moveSequences.Add(new GridPosition(initialPosition.X - i, initialPosition.Y));
            }

            initialPosition = moveSequences.LastOrDefault() ?? initialPosition;

            for (var i = 1; i <= Math.Abs(targetGridPosition.Y - initialPosition.Y); i++)
            {
                if (targetGridPosition.Y - initialPosition.Y > 0)
                {
                    moveSequences.Add(new GridPosition(initialPosition.X, initialPosition.Y + i));
                    continue;
                }

                moveSequences.Add(new GridPosition(initialPosition.X, initialPosition.Y - i));
            }

            return moveSequences;
        }

    }
}