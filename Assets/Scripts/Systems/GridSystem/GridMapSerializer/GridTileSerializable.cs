using System;
using Assets.Scripts.Controller;
using Assets.Scripts.Model.GridModel;
using Sirenix.Serialization;

namespace Assets.Scripts.Systems.GridSystem
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