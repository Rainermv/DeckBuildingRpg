using System.Collections.Generic;
using Assets.Scripts.Model.GridModel;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Assets.Scripts.Systems.GridSystem
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