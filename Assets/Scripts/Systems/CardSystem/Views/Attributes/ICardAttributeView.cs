using Assets.Scripts.Ruleset;

namespace Assets.Scripts.CardSystem.Views
{
    public interface ICardAttributeView
    {
        AttributeKey AttributeKey { get; set; }
        void Display(int value);
    }
}