using System.Collections.Generic;
using Assets.Scripts.Model.Actor;
using Assets.Scripts.Model.AttributeModel;
using Assets.Scripts.Model.Card;
using Assets.Scripts.Model.GridMap;

namespace Assets.Scripts.Model
{
    public class LevelModel
    {
        public Dictionary<string, Player> Players { get; set; }  = new();
        public List<Entity> Entities { get; set; } = new();
        public AttributeSet GlobalAttributeSet { get; set; }
        public GridMapModel GridMapModel { get; set; }
    }
}