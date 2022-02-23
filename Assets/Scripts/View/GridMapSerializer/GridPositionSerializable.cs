using Assets.Scripts.Core.Model.GridMap;
using Sirenix.Serialization;

namespace Assets.Scripts.View.GridMapSerializer
{
    //[Serializable]
    internal record GridPositionSerializable
    {
        [OdinSerialize]  private int X;
        [OdinSerialize]  private int Y;

        public GridPositionSerializable(int x, int y)
        {
            X = x;
            Y = y;
        }

        public GridPosition ToGridPosition()
        {
            return new GridPosition(X, Y);

        }
    }
}