using System.Collections.Generic;
using Assets.Scripts.CardSystem.Models.Attributes;
using Assets.Scripts.Model.CardModel;
using Assets.Scripts.Model.CharacterModel;
using Assets.Scripts.Model.GridModel;

namespace Assets.Scripts.Model
{
    public class LevelModel
    {
        public Dictionary<string, Player> Players { get; set; }  = new();
        public List<Character> Characters { get; set; } = new();
        public AttributeSet GlobalAttributeSet { get; set; }
        public GridMapModel GridMapModel { get; set; }
    }
}