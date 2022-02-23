using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.Model.Entity;
using Assets.Scripts.Core.Model.GridMap;

namespace Assets.Scripts.Core.Utility
{
    public static class GridUtilities
    {
        public static double GetCostToPosition(GridPosition fromGridPosition, GridPosition toGridPosition,
            Dictionary<GridPosition, GridTile> tileDictionary, List<BattleEntity> entities)
        {
            // todo: block movement when passing through entities on diagonal movement
            return IsPositionValid(toGridPosition, tileDictionary, entities) 
                ? tileDictionary[toGridPosition].MoveCostToEnter * Distance(fromGridPosition, toGridPosition)
                : double.MaxValue;

        }

        public static bool IsPositionValid(GridPosition position, Dictionary<GridPosition, GridTile> tileDictionary,
            List<BattleEntity> entities)
        {
            return tileDictionary.ContainsKey(position) && entities.All(entity => entity.GridPosition != position);
            //return tileDictionary.ContainsKey(position);
        }

        public static double Distance(GridPosition p1, GridPosition p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + (Math.Pow(p1.Y - p2.Y, 2)));
        }
    }


}