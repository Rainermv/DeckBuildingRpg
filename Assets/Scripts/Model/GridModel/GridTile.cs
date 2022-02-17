using System;
using Assets.Scripts.Controller;
using Sirenix.Serialization;

namespace Assets.Scripts.Model.GridModel
{
    public class GridTile
    {
        public uint TileType { get; }
        public GridPosition GridPosition { get; }
        public IEntity PositionedEntity { get; set; }

        public GridTile(GridPosition gridPosition, uint tileType)
        {
            GridPosition = gridPosition;
            TileType = tileType;
        }
    }
}