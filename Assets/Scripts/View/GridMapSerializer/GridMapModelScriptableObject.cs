using Assets.Scripts.Core.Model.GridMap;
using Sirenix.Serialization;

namespace Assets.Scripts.View.GridMapSerializer
{
    internal class GridMapModelScriptableObject
    {
        [OdinSerialize] private GridMapModelSerializable GridMapModelSerializable;

        public GridMapModelScriptableObject(GridMapModelSerializable gridMapModelSerializable)
        {
            GridMapModelSerializable = gridMapModelSerializable;
        }

        public GridMapModel GridMapModel => GridMapModelSerializable.ToGridMapModel();
    }

}