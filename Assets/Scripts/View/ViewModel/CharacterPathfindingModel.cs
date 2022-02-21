using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Model.GridMap;

namespace Assets.Scripts.View
{
    public class CharacterPathfindingModel
    {
        public List<GridPosition> GridPositions { get; set; } = new();
        public int MoveRange { get; set; }
    }
}