using Assets.Scripts.Core.Model.EntityModel;

namespace Assets.Scripts.Core.Model.Cards
{
    public class CardPlayData
    {
        public bool IsPlayValid { get; set; }
        public Player Player { get; set; }
        public Cards.Card Card { get; set; }
        public Entity SourceEntity { get; set; }
    }
}