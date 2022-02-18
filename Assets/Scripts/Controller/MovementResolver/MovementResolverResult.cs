using System.Collections.Generic;
using Assets.Scripts.Model.Actor;
using Assets.Scripts.Model.GridMap;

namespace Assets.Scripts.Controller.MovementResolver
{
    internal class MovementResolverResult
    {
        public bool Success { get; set; }
        public List<GridPosition> MoveList { get; set; }
        public Entity MovedEntity { get; set; }
    }
}