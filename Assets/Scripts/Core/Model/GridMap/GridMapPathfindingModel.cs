using System.Collections.Generic;

namespace Assets.Scripts.Core.Model.GridMap
{
    public class GridMapPathfindingModel
    {
        public List<GridPosition> GridPositions { get; set; } = new();
        public int MovementRange { get; set; }
    }
}