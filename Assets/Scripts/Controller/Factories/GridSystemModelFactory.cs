using System.Collections.Generic;
using System.Numerics;
using Assets.Scripts.Model.GridModel;

namespace Assets.Scripts.Controller.Factories
{
    internal class GridSystemModelFactory
    {
        public static GridTileModel Build(uint width, uint height)
        {
            var gridTiles = new Dictionary<GridPosition, GridTile>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    gridTiles.Add(new GridPosition(x,y), new GridTile()
                    {
                        Type = 0,
                        X = x,
                        Y = y
                    });
                }
            }

            return new GridTileModel()
            {
                Width = width,
                Height = height,
                GridTiles = gridTiles
            };

            

        }
    }
}