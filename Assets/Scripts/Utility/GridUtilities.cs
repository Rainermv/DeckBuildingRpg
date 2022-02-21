using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Model.Actor;
using Assets.Scripts.Model.GridMap;
using Assets.Scripts.View.GridMap;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Utility
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
            Dictionary<GridPosition, GridTile> tileDictionary, List<Entity> entities)
        {
            return IsPositionValid(toGridPosition, tileDictionary, entities) 
                ? tileDictionary[toGridPosition].MoveCostToEnter * Distance(fromGridPosition, toGridPosition)
                : double.MaxValue;

            //return (double)tileDictionary[toGridPosition].MoveCostToEnter;
        }

        public static bool IsPositionValid(GridPosition position, Dictionary<GridPosition, GridTile> tileDictionary,
            List<Entity> entities)
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
    }
}