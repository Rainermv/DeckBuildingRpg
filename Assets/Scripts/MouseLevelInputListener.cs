using System;
using UnityEngine;

namespace Assets.Scripts
{
    internal class MouseLevelInputListener : MonoBehaviour, ILevelInputListener
    {
        private Action<Vector3> _onInputTrigger;


        public void Initialize(Action<Vector3> _onWorldInputTrigger)
        {
            this._onInputTrigger = _onWorldInputTrigger;
        }

        public Vector3 WorldInputPosition => CameraController.ScreenToWorldPoint(Input.mousePosition)
        ;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _onInputTrigger(WorldInputPosition);
            }
        }
    }
}