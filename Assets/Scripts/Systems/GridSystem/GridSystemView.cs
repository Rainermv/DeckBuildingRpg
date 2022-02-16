using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Assets.Scripts;
using Assets.Scripts.GridSystem;
using Mono.Cecil.Cil;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

public class GridSystemView : SerializedMonoBehaviour 
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
    private Vector3Int _mouseOverGridPosition;
    private GridSystemModel _gridSystemModel;
    private GameObject _character;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(GridSystemModel gridSystemModel, Func<Vector3> onGetCursorWorldPosition)
    {
        _gridSystemModel = gridSystemModel;
        _onGetCursorWorldPosition = onGetCursorWorldPosition;

        foreach (var gridTile in gridSystemModel.GridTiles.Values)
        {
            var tilePosition = new Vector3Int(gridTile.X, gridTile.Y);

            //GridTilemap.SetTile(tilePosition, TileAsset);

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
        GridTilemapHighlight.SetTile(_mouseOverGridPosition, null);
    }

    void OnTilemapMouseOver()
    {
        var worldPosition = _onGetCursorWorldPosition?.Invoke();

        if (worldPosition == null)
        {
            return;
        }
        
        var gridPosition = Grid.WorldToCell(new Vector3(worldPosition.Value.x, worldPosition.Value.y));

        if (gridPosition.x < 0 || gridPosition.y < 0 || gridPosition.x > _gridSystemModel.Width ||
            gridPosition.y > _gridSystemModel.Height)
        {
            return;
        }

        if (gridPosition == _mouseOverGridPosition) 
            return;

        GridTilemapHighlight.SetTile(_mouseOverGridPosition, null);
        GridTilemapHighlight.SetTile(gridPosition, TileDictionary[TileViewType.Highlight]);
        _mouseOverGridPosition = gridPosition;
        Debug.Log(gridPosition);

    }

    public void SetThingTo(Transform thing, Vector3Int gridPosition)
    {
        var worldPosition = GridTilemap.CellToWorld(gridPosition);

        thing.position = worldPosition;
        //_character = Instantiate(thing, worldPosition, Quaternion.identity);
    }
}