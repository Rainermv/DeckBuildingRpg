using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.Model.Entity;
using Assets.Scripts.Core.Model.GridMap;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Core.Utility
{
    public static class GridUtilities
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

        public static double GetCostToPosition(GridPosition fromGridPosition, GridPosition toGridPosition,
            Dictionary<GridPosition, GridTile> tileDictionary, List<BattleEntity> entities)
        {
            return IsPositionValid(toGridPosition, tileDictionary, entities) 
                ? tileDictionary[toGridPosition].MoveCostToEnter * Distance(fromGridPosition, toGridPosition)
                : double.MaxValue;

            //return (double)tileDictionary[toGridPosition].MoveCostToEnter;
        }

        public static bool IsPositionValid(GridPosition position, Dictionary<GridPosition, GridTile> tileDictionary,
            List<BattleEntity> entities)
        {
            return tileDictionary.ContainsKey(position) && entities.All(entity => entity.GridPosition != position);
            //return tileDictionary.ContainsKey(position);
        }

        private static bool InsideBounds(GridMapModel gridMapModel, GridPosition gridPosition)
        {
            return new Rect(0, 0, gridMapModel.Width, gridMapModel.Height).Contains(new Vector2(gridPosition.X,
                gridPosition.Y));
        }

        public static double Distance(GridPosition p1, GridPosition p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + (Math.Pow(p1.Y - p2.Y, 2)));
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

    
    public class TerrainTile : TileBase
    {
        public Sprite Sprite;
        public bool DifficultTerrain;
        
        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            tileData.colliderType = Tile.ColliderType.Grid;
            tileData.sprite = Sprite;
        }
    }

    //[CustomGridBrush(true, false, true, "TerrainBrush")]
    public class TerrainBrush : GridBrushBase
    {
        private bool IsDifficultTerrain;

        public override void Paint(GridLayout grid, GameObject brushTarget, Vector3Int position)
        {
            //var terrain = brushTarget as TerrainTile;
            base.Paint(grid, brushTarget, position);
        }

    }
}