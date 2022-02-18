namespace Assets.Scripts.View.Attribute
{
    public interface ICardAttributeView
    {
        int AttributeKey { get; set; }
        void Display(int value);
    }
}