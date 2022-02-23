using Assets.Scripts.Core.Model.AttributeModel;

namespace Assets.Scripts.Core.Model.Command
{
    public interface ITargetable
    {
        AttributeSet AttributeSet { get; }
    }
}