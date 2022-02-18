using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.GridMapSerializer
{
    public class GridMapSerializer : SerializedMonoBehaviour
    {
        [Button(ButtonStyle.Box)]
        private void Serialize(string filename)
        {
            var tilemap = GetComponent<Tilemap>();

            var bounds = tilemap.cellBounds;
            var allTiles = tilemap.GetTilesBlock(bounds);

            var gridTiles = new List<GridTileSerializable>();

            for (var x = 0; x < bounds.size.x; x++)
            {
                for (var y = 0; y < bounds.size.y; y++)
                {
                    var tile = allTiles[x + y * bounds.size.x];
                    if (tile == null)
                        continue;

                    var gridPosition = new GridPositionSerializable(x, y);

                    gridTiles.Add(new GridTileSerializable(gridPosition, 1));
                    Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);

                }
            }

            var gridMapModelSerializable = new GridMapModelSerializable(bounds.size.x, bounds.size.y, gridTiles);

            AssetDatabase.CreateAsset(new GridMapModelScriptableObject(gridMapModelSerializable), $"Assets/Maps/{filename}.asset");
            AssetDatabase.SaveAssets();

        }

        [FoldoutGroup("Clear Map")]
        [Button("Confirm Clear", Expanded = false)]
        private void ClearMap()
        {
            var tilemap = GetComponent<Tilemap>();

            tilemap.ClearAllTiles();
        }
    }
}