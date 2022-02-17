using System;
using System.Collections.Generic;
using Assets.Scripts.Controller;
using Assets.Scripts.Model;
using Assets.Scripts.Model.CardModel;
using Assets.Scripts.Model.GridModel;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Systems.GridSystem
{
    public class GridView : SerializedMonoBehaviour 
    {
    
        [Title("Assets")]
        [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.OneLine)]
        [AssetsOnly] public Dictionary<TileViewType, Tile> TileDictionary;
    

        [Title("Scene")]
        [SceneObjectsOnly, Required] public Tilemap GridTilemap;
        [SceneObjectsOnly, Required] public Tilemap GridTilemapHighlight;
        [SceneObjectsOnly, Required] public Grid Grid;


        internal Vector3 GridCenter => GridTilemap.localBounds.center;

        private Func<Vector3> _onGetCursorWorldPosition;
        private Vector3Int? _mouseOverGridPosition;
        private GridTileModel _gridTileModel;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void Initialize(GridTileModel gridTileModel, Func<Vector3> onGetCursorWorldPosition)
        {
            _gridTileModel = gridTileModel;

            _onGetCursorWorldPosition = onGetCursorWorldPosition;

            foreach (var gridTile in _gridTileModel.GridTiles.Values)
            {
                var tilePosition = new Vector3Int(gridTile.X, gridTile.Y);

                UpdateTile(tilePosition, gridTile);

                GridTilemapHighlight.SetTile(tilePosition, null);
            }

            GridTilemap.GetComponent<TilemapListener>().Initialize(OnTilemapMouseEnter, OnTilemapMouseExit, OnTilemapMouseOver);
        }

        private void UpdateTile(Vector3Int tilePosition, GridTile gridTile)
        {
            var isOffset = GridUtilities.IsTileOffset(tilePosition);
            var tile = isOffset
                ? TileDictionary[TileViewType.Offset]
                : TileDictionary[TileViewType.Normal];

            GridTilemap.SetTile(tilePosition, tile);
        }

        void OnTilemapMouseEnter()
        {

        }

        void OnTilemapMouseExit()
        {
            if (_mouseOverGridPosition != null) 
                GridTilemapHighlight.SetTile(_mouseOverGridPosition.Value, null);
        }

        void OnTilemapMouseOver()
        {
            var worldPosition = _onGetCursorWorldPosition?.Invoke();

            if (worldPosition == null)
            {
                return;
            }
        
            var gridPosition = Grid.WorldToCell(new Vector3(worldPosition.Value.x, worldPosition.Value.y));

            if (gridPosition.x < 0 || gridPosition.y < 0 || gridPosition.x > _gridTileModel.Width ||
                gridPosition.y > _gridTileModel.Height)
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


        public Vector3 CellToWorld(GridPosition gridPosition)
        {
            return GridTilemap.CellToWorld(new Vector3Int(gridPosition.X, gridPosition.Y));
        }

        public GridPosition WorldToCell(Vector3 worldPosition)
        {
            return new GridPosition(GridTilemap.WorldToCell(worldPosition));
        }
    }
}