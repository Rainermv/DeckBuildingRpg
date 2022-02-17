using UnityEngine;

namespace Assets.Scripts.Controller
{
    public record GridPosition
    {
        public int X { get; }
        public int Y { get; }

        public GridPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public GridPosition(Vector3Int vector3Position)
        {
            X = vector3Position.x;
            Y = vector3Position.y;
        }

        public override string ToString()
        {
            return $"(X:{X}, Y:{Y})";
        }
    }
}