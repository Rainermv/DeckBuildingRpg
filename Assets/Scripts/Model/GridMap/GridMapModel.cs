using System.Collections.Generic;

namespace Assets.Scripts.Model.GridMap
{
    public class GridMapModel
    {
        public GridMapModel(int width, int height, List<GridTile> gridTiles)
        {
            Width = width;
            Height = height;
            GridTiles = gridTiles;
        }

        public int Width { get; }
        public int Height { get; }
        public List<GridTile> GridTiles { get; }
    }
}