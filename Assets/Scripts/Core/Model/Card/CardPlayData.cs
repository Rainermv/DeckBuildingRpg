using Assets.Scripts.Core.Model.EntityModel;

namespace Assets.Scripts.Core.Model.Card
{
    public class CardPlayData
    {
        public bool IsPlayValid { get; set; }
        public Player Player { get; set; }
        public Card Card { get; set; }
        public Entity SourceEntity { get; set; }
    }
}