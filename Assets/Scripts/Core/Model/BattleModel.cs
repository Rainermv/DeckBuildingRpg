using System.Collections.Generic;
using Assets.Scripts.Core.Model.AttributeModel;
using Assets.Scripts.Core.Model.Card;
using Assets.Scripts.Core.Model.Entity;
using Assets.Scripts.Core.Model.GridMap;

namespace Assets.Scripts.Core.Model
{
    public class BattleModel
    {
        public List<Player> Players { get; set; }  = new();
        public List<BattleEntity> Entities { get; set; } = new();
        public AttributeSet GlobalAttributeSet { get; set; }
        public GridMapModel GridMapModel { get; set; }
    }
}