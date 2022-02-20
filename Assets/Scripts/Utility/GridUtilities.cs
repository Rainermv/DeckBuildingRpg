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
    }
}