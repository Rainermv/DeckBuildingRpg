using System.Collections.Generic;
using System.Numerics;

namespace Assets.Scripts.Ruleset
{
    internal class GridSystemModelFactory
    {
        public static GridSystemModel Build(uint width, uint height)
        {
            var gridDictionary = new Dictionary<Vector2, GridTile>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    gridDictionary.Add(new Vector2(x,y), new GridTile()
                    {
                        Type = 0,
                        X = x,
                        Y = y
                    });
                }
            }

            return new GridSystemModel()
            {
                Width = width,
                Height = height,
                GridTiles = gridDictionary
            };

            

        }
    }
}