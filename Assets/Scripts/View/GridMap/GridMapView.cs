using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assets.Scripts.Controller;
using Assets.Scripts.Core.Model.GridMap;
using Assets.Scripts.Core.Utility;
using Assets.Scripts.View.Cards;
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
        [AssetsOnly, Required] public Dictionary<TileViewType, Tile> TileDictionary;
    
        [Title("Scene")]
        [SceneObjectsOnly, Required] public Tilemap GridTilemap;
        [SceneObjectsOnly, Required] public Tilemap GridTilemapHighlight;
        [SceneObjectsOnly, Required] public Grid Grid;

        private Vector3Int? _mouseOverGridPosition;
        private GridMapModel _gridMapModel;
        private Func<GridPosition, GridMapPathfindingModel> _onFindPathToTargetGrid;
        private Func<Task<MovePathResult>> _onExecuteMovement;

        public void Initialize(GridMapModel gridMapModel,
            Func<GridPosition, GridMapPathfindingModel> onFindPathToTargetGrid,
            Func<Task<MovePathResult>> onExecuteMovement)
        {
            _gridMapModel = gridMapModel;
            _onFindPathToTargetGrid = onFindPathToTargetGrid;
            _onExecuteMovement = onExecuteMovement;


            foreach (var gridTile in _gridMapModel.GridTiles)
            {
                var tilePosition = new Vector3Int(gridTile.GridPosition.X, gridTile.GridPosition.Y);

                UpdateTile(tilePosition, gridTile);
            }

            UIEvents.OnTilemapPointerEvent += OnTilemapPointerEvent;

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

        private async void OnTilemapPointerEvent(PointerEventData pointerEventData, int pointerEventTrigger)
        {
            if (!pointerEventData.pointerCurrentRaycast.isValid)
            {
                return;
            }

            var worldPosition = pointerEventData.pointerCurrentRaycast.worldPosition;

            var gridPosition = WorldToTilemapGrid(worldPosition);

            switch (pointerEventTrigger)
            {
                case PointerEventTrigger.MOVE:
                    var pathfindModel = _onFindPathToTargetGrid(gridPosition);
                    DrawHighlight(pathfindModel.GridPositions, pathfindModel.MovementRange);
                    break;

                case PointerEventTrigger.EXIT:
                    ClearHighlight();
                    break;

                case PointerEventTrigger.DOWN:
                    ClearHighlight();
                    await _onExecuteMovement();
                    break;

            }
        }


    }
}