using Assets.Scripts.Core.Model.AttributeModel;

namespace Assets.Scripts.Core.Commands
{
    internal interface ITargetable
    {
        AttributeSet AttributeSet { get; }
    }
}