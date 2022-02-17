using System;
using Assets.Scripts.Controller;
using Sirenix.Serialization;

namespace Assets.Scripts.Systems.GridSystem
{
    [Serializable]
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