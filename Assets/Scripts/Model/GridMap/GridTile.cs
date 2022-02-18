namespace Assets.Scripts.Model.GridMap
{
    public class GridTile
    {
        public uint TileType { get; }
        public GridPosition GridPosition { get; }
        public GridTile(GridPosition gridPosition, uint tileType)
        {
            GridPosition = gridPosition;
            TileType = tileType;
        }
    }
}