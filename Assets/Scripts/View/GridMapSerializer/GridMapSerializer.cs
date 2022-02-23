using Sirenix.OdinInspector;

namespace Assets.Scripts.View.GridMapSerializer
{
    public class GridMapSerializer : SerializedMonoBehaviour
    {/*
        [Button(ButtonStyle.Box)]
        private void Serialize(string filename)
        {
            var tilemap = GetComponent<Tilemap>();


            var gridMapModel = GridUtilities.GridMapModelFrom(tilemap);

            var serializableTiles =
                gridMapModel.GridTiles.Select(gt => new GridTileSerializable(new GridPositionSerializable(gt.GridPosition.X, gt.GridPosition.Y), 
                    gt.TileType,
                    gt.MoveCostToEnter
                )).ToList();


            var gridMapModelSerializable = new GridMapModelSerializable(gridMapModel.Width, gridMapModel.Height, serializableTiles);

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
        */
    }
}