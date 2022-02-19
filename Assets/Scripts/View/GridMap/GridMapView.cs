using System;
using System.Collections.Generic;
using Assets.Scripts.Model.GridMap;
using Assets.Scripts.Utility;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.View.GridMap
{
    public class GridMapView : SerializedMonoBehaviour 
    {
    
        [Title("Assets")]
        [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.OneLine)]
        [AssetsOnly] public Dictionary<TileViewType, Tile> TileDictionary;
    

        [Title("Scene")]
        [SceneObjectsOnly, Required] public Tilemap GridTilemap;
        [SceneObjectsOnly, Required] public Tilemap GridTilemapHighlight;
        [SceneObjectsOnly, Required] public Grid Grid;

        private Vector3Int? _mouseOverGridPosition;
        private GridMapModel _gridMapModel;

        public void Initialize(GridMapModel gridMapModel,
            Action<GridPosition> onTilemapPointerMove,
            Action<GridPosition> onTilemapPointerDown)
        {
            _gridMapModel = gridMapModel;

            foreach (var gridTile in _gridMapModel.GridTiles)
            {
                var tilePosition = new Vector3Int(gridTile.GridPosition.X, gridTile.GridPosition.Y);

                UpdateTile(tilePosition, gridTile);
            }

            GridTilemap.GetComponent<TilemapListener>().Initialize(
                // Tilemap World To GridPosition PointerMove
                worldPosition => onTilemapPointerMove(
                    GridUtilities.WorldToGrid(GridTilemap, worldPosition)),
                // Tilemap World To GridPosition PointerDown
                worldPosition => onTilemapPointerDown(
                    GridUtilities.WorldToGrid(GridTilemap, worldPosition)));
                
        }

        private void UpdateTile(Vector3Int tilePosition, GridTile gridTile)
        {
            var isOffset = GridUtilities.IsTileOffset(tilePosition);
            var tile = isOffset
                ? TileDictionary[TileViewType.Offset]
                : TileDictionary[TileViewType.Normal];

            GridTilemap.SetTile(tilePosition, tile);
        }

        void _OnTilemapMouseOver()
        {
            var worldPosition = Vector3.zero;

            if (worldPosition == null)
            {
                return;
            }
        
            var gridPosition = Grid.WorldToCell(new Vector3(worldPosition.x, worldPosition.y));

            if (gridPosition.x < 0 || gridPosition.y < 0 || gridPosition.x > _gridMapModel.Width ||
                gridPosition.y > _gridMapModel.Height)
            {
                _mouseOverGridPosition = null;
                return;
            }

            if (_mouseOverGridPosition == null || gridPosition == _mouseOverGridPosition) 
                return;

            GridTilemapHighlight.SetTile(_mouseOverGridPosition.Value, null);
            GridTilemapHighlight.SetTile(gridPosition, TileDictionary[TileViewType.Highlight]);
            _mouseOverGridPosition = gridPosition;
        }

        public void DrawPath(List<GridPosition> gridPositions)
        {
            GridTilemapHighlight.ClearAllTiles();

            foreach (var gridPosition in gridPositions)
            {
                GridTilemapHighlight.SetTile(GridUtilities.VectorFrom(gridPosition), TileDictionary[TileViewType.Highlight]);
            }
            
        }


        public Vector3 CellToWorld(GridPosition gridPosition)
        {
            return GridTilemap.CellToWorld(new Vector3Int(gridPosition.X, gridPosition.Y));
        }

        public Vector3Int WorldToCell(Vector3 worldPosition)
        {
            return GridTilemap.WorldToCell(worldPosition);
        }


        public bool PointWithinBounds(Vector3Int cellPosition)
        {
            return GridTilemap.cellBounds.Contains(cellPosition);
        }

       
    }
}