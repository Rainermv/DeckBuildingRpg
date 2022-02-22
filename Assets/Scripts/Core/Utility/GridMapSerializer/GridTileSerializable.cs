using System;
using Assets.Scripts.Core.Model.GridMap;
using Sirenix.Serialization;

namespace Assets.Scripts.Core.Utility.GridMapSerializer
{
    //[Serializable]
    internal class GridTileSerializable
    {
        [OdinSerialize] private uint _tileType;
        [OdinSerialize] private int _moveCostToCenter;

        [OdinSerialize] private GridPositionSerializable _gridPositionSerializable;
          
        public GridTileSerializable(GridPositionSerializable gridPositionSerializable, uint tileType,
            int moveCostToCenter)
        {
            _gridPositionSerializable = gridPositionSerializable;
            _tileType = tileType;
            _moveCostToCenter = moveCostToCenter;
        }

        public GridTile ToGridTile()
        {
            return new GridTile(_gridPositionSerializable.ToGridPosition(), _tileType, _moveCostToCenter);
        }
    }
}