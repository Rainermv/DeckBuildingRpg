using System.Collections.Generic;
using System.Numerics;
using Assets.Scripts.Controller;

namespace Assets.Scripts.Model.GridModel
{
    public class GridTileModel
    {
        public uint Width { get; set; }
        public uint Height { get; set; }
        public Dictionary<GridPosition, GridTile> GridTiles { get; set; }
    }
}