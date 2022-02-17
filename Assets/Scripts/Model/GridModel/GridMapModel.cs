using System;
using System.Collections.Generic;
using System.Numerics;
using Assets.Scripts.Controller;

namespace Assets.Scripts.Model.GridModel
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