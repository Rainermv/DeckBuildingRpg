using Assets.Scripts.Model.GridMap;
using UnityEngine;
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

        public static GridPosition WorldToGrid(Tilemap gridTilemap, Vector3 worldPosition)
        {
            var vector3IntPosition = gridTilemap.WorldToCell(worldPosition);
            return new GridPosition(vector3IntPosition.x, vector3IntPosition.y);
        }

        public static Vector3Int VectorFrom(GridPosition gridPosition)
        {
            return new Vector3Int(gridPosition.X, gridPosition.Y);
        }
    }
}