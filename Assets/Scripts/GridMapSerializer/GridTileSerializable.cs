using System;
using Assets.Scripts.Model.GridMap;
using Sirenix.Serialization;

namespace Assets.Scripts.GridMapSerializer
{
    [Serializable]
    internal class GridTileSerializable
    {
        [OdinSerialize] private uint TileType;

        [OdinSerialize] private GridPositionSerializable GridPositionSerializable;

        public GridTileSerializable(GridPositionSerializable gridPositionSerializable, uint tileType)
        {
            GridPositionSerializable = gridPositionSerializable;
            TileType = tileType;
        }

        public GridTile ToGridTile()
        {
            return new GridTile(GridPositionSerializable.ToGridPosition(), TileType);
        }
    }
}