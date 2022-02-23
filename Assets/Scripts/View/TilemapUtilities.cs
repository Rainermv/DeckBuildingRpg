using System.Collections.Generic;
using Assets.Scripts.Core.Model.GridMap;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.View
{
    public static class TilemapUtilities
    {
        public static bool IsTileOffset(Vector3Int tilePosition)
        {
            var x = tilePosition.x;
            var y = tilePosition.y;

            return (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
        }

        public static Vector3Int VectorFrom(GridPosition gridPosition)
        {
            return new Vector3Int(gridPosition.X, gridPosition.Y);
        }

        private static bool InsideBounds(GridMapModel gridMapModel, GridPosition gridPosition)
        {
            return new Rect(0, 0, gridMapModel.Width, gridMapModel.Height).Contains(new Vector2(gridPosition.X,
                gridPosition.Y));
        }

        public static GridMapModel GridMapModelFrom(Tilemap tilemap)
        {
            var bounds = tilemap.cellBounds;
            var allTiles = tilemap.GetTilesBlock(bounds);

            var gridTiles = new List<GridTile>();

            foreach (var position in tilemap.cellBounds.allPositionsWithin)
            {
                if (!tilemap.HasTile(position))
                {
                    continue;
                }

                var tile = tilemap.GetTile(position);

                var spriteName = tilemap.GetSprite(position).name;
                


                var gridPosition = new GridPosition(position.x, position.y);

                gridTiles.Add(new GridTile(gridPosition, 1, spriteName.Contains("Stone")? 1 : 2 ));
            }

            //Debug.Log($"READ: {string.Join(",", gridTiles.Select(tile =>  $"[{tile.GridPosition.X},{ tile.GridPosition.Y}]"))}");

            return new GridMapModel(bounds.size.x, bounds.size.y, gridTiles);
        }
    }
}