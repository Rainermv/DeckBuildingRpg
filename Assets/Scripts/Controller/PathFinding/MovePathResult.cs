using System.Collections.Generic;
using Assets.Scripts.Core.Model.EntityModel;
using Assets.Scripts.Core.Model.GridMap;

namespace Assets.Scripts.Controller
{
    public class MovePathResult
    {
        public Entity MovedEntity { get; set; }
        public List<GridPosition> PathSequence { get; set; }
    }
}