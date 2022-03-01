using System.Collections.Generic;
using Assets.Scripts.Core.Model.AttributeModel;
using Assets.Scripts.Core.Model.Cards;
using Assets.Scripts.Core.Model.EntityModel;
using Assets.Scripts.Core.Model.GridMap;

namespace Assets.Scripts.Core.Model
{
    public class CombatModel
    {
        public List<Player> Players { get; set; }  = new();
        public List<Entity> Entities { get; set; } = new();
        public Attributes GlobalAttributes { get; set; }
        public GridMapModel GridMapModel { get; set; }
        public Dictionary<string, int> AttributeMap { get; set; }
        public List<CardData> CardDataList { get; set; }
    }
}