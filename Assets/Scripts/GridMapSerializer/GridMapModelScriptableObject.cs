using Assets.Scripts.Model.GridMap;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Assets.Scripts.GridMapSerializer
{
    internal class GridMapModelScriptableObject : SerializedScriptableObject
    {
        [OdinSerialize] private GridMapModelSerializable GridMapModelSerializable;

        public GridMapModelScriptableObject(GridMapModelSerializable gridMapModelSerializable)
        {
            GridMapModelSerializable = gridMapModelSerializable;
        }

        public GridMapModel GridMapModel => GridMapModelSerializable.ToGridMapModel();
    }

}