using System.Collections.Generic;
using Assets.Scripts.Core.Model.GridMap;

namespace Assets.Scripts.Controller.MovementResolver
{
    public class PathFindResult
    {
        public bool PathFound { get; set; }
        public List<GridPosition> MovementPathPositions { get; set; }
    }
}