using Assets.Scripts.Controller;

namespace Assets.Scripts.Systems.CardSystem.Views.Attributes
{
    public interface ICardAttributeView
    {
        int AttributeKey { get; set; }
        void Display(int value);
    }
}