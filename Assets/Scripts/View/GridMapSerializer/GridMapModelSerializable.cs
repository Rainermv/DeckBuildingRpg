using System.Collections.Generic;
using Assets.Scripts.Core.Model.GridMap;
using Sirenix.Serialization;

namespace Assets.Scripts.View.GridMapSerializer
{

    //[Serializable]
    internal class GridMapModelSerializable
    {
        [OdinSerialize] private int _width;
        [OdinSerialize] private int _height;
        [OdinSerialize] private List<GridTileSerializable> _gridTiles;

        public GridMapModelSerializable(int width, int height, List<GridTileSerializable> gridTiles)
        {
            _width = width;
            _height = height;
            _gridTiles = gridTiles;
        }

        public GridMapModel ToGridMapModel()
        {
            var gridTiles = new List<GridTile>();
            foreach (var gridTileSerializable in _gridTiles)
            {
                gridTiles.Add(gridTileSerializable.ToGridTile());
            }

            return new GridMapModel(_width, _height, gridTiles);
        }
    }
}