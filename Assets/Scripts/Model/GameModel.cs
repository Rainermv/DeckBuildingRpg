using Assets.Scripts.CardSystem.Models;
using Assets.Scripts.CardSystem.Models.Attributes;

namespace Assets.Scripts
{
    public class GameModel
    {
        public CardSystemModel CardSystemModel { get; set; }
        public AttributeSet GlobalAttributeSet { get; set; }
        public GridSystemModel GridSystemModel { get; set; }
    }
}