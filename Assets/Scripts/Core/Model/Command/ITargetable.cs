using Assets.Scripts.Core.Model.AttributeModel;

namespace Assets.Scripts.Core.Model.Command
{
    internal interface ITargetable
    {
        AttributeSet AttributeSet { get; }
    }
}