using UnityEngine;

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
    }
}