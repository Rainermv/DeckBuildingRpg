using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.Model.GridMap;
using Assets.Scripts.Core.Utility;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
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
            Action<PointerEventData, int> onTilemapPointerEvent)
        {
            _gridMapModel = gridMapModel;


            foreach (var gridTile in _gridMapModel.GridTiles)
            {
                var tilePosition = new Vector3Int(gridTile.GridPosition.X, gridTile.GridPosition.Y);

                UpdateTile(tilePosition, gridTile);
            }
            //Debug.Log($"WRITE: {string.Join(",", _gridMapModel.GridTiles.Select(tile => $"[{ tile.GridPosition.X},{ tile.GridPosition.Y}]"))}");


            GridTilemap.GetComponent<TilemapListener>().OnTilemapPointerEvent += onTilemapPointerEvent;

        }

        public GridPosition WorldToTilemapGrid(Vector3 worldPosition)
        {
            var vector3IntPosition = GridTilemap.WorldToCell(worldPosition);
            return new GridPosition(vector3IntPosition.x, vector3IntPosition.y);
        }

        private void UpdateTile(Vector3Int tilePosition, GridTile gridTile)
        {
            var isOffset = TilemapUtilities.IsTileOffset(tilePosition);
            var tile = isOffset
                ? TileDictionary[TileViewType.Offset]
                : TileDictionary[TileViewType.Normal];

            var tileColor = Color.Lerp(Color.white, Color.black, gridTile.MoveCostToEnter - 1);

            GridTilemap.SetTile(tilePosition, tile);
            GridTilemap.SetColor(tilePosition, tileColor);
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

        public void DrawHighlight(List<GridPosition> gridPositions, int moveLimit)
        {
            GridTilemapHighlight.ClearAllTiles();
            for (var i = 0; i < gridPositions.Count; i++)
            {
                var vectorPosition = TilemapUtilities.VectorFrom(gridPositions[i]);

                var color = i <= moveLimit ? Color.white : Color.red;
                GridTilemapHighlight.SetTile((vectorPosition), TileDictionary[TileViewType.Highlight]);
                GridTilemapHighlight.SetTileFlags(vectorPosition, TileFlags.None);
                GridTilemapHighlight.SetColor(vectorPosition, color);
            }
        }

        public void ClearHighlight()
        {
            GridTilemapHighlight.ClearAllTiles();
        }


        public Vector3 GridToWorld(GridPosition gridPosition)
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