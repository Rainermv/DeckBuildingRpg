using System.Collections.Generic;
using Assets.Scripts.Model.GridMap;

namespace Assets.Scripts.View
{
    public class FindPathResult
    {
        public bool PathFound { get; set; }
        public List<GridPosition> MovementPathPositions { get; set; }
    }
}