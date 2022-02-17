using System;
using UnityEngine;

namespace Assets.Scripts
{
    internal interface ILevelInputListener
    {
        void Initialize(Action<Vector3> _onWorldInputTrigger);
        Vector3 WorldInputPosition { get; }
    }
}