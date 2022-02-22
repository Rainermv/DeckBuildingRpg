namespace Assets.Scripts.Core.Model.GridMap
{
    public record GridPosition
    {
        private static GridPosition p2;
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