using Assets.Scripts.Core.Model.AttributeModel;

namespace Assets.Scripts.Core.Model
{
    public interface ITargetable
    {
        Attributes Attributes { get; }
        string Name { get; }
    }
}