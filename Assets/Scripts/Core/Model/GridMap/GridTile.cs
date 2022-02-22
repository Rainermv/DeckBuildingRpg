namespace Assets.Scripts.Core.Model.GridMap
{
    public class GridTile
    {
        public uint TileType { get; }
        public GridPosition GridPosition { get; }
        public int MoveCostToEnter { get; set; }

        public GridTile(GridPosition gridPosition, uint tileType, int moveCostToEnter = 1)
        {
            GridPosition = gridPosition;
            TileType = tileType;
            MoveCostToEnter = moveCostToEnter;
        }
    }
}