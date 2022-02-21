using System.Collections.Generic;
using Assets.Scripts.Model.GridMap;

namespace Assets.Scripts.View
{
    public class PathFindResult
    {
        public bool PathFound { get; set; }
        public List<GridPosition> MovementPathPositions { get; set; }
    }
}