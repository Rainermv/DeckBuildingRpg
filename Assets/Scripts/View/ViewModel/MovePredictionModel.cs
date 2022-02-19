using System.Collections.Generic;
using Assets.Scripts.Model.GridMap;

namespace Assets.Scripts.View
{
    internal class MovePredictionModel
    {
        public GridPosition TargetPosition { get; set; }
        public List<GridPosition> MovementPathPositions { get; set; }
    }
}