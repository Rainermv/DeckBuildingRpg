using System;

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

        public override string ToString()
        {
            return $"(X:{X}, Y:{Y})";
        }
    }
}