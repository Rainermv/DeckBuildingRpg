using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace Assets.Scripts
{
    public class GridSystemModel
    {
        public uint Width { get; set; }
        public uint Height { get; set; }
        public Dictionary<Vector2, GridTile> GridTiles { get; set; }
    }
}