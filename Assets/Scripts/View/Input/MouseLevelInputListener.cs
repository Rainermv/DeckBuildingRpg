using System;
using UnityEngine;

namespace Assets.Scripts.View.Input
{
    internal class MouseLevelInputListener : MonoBehaviour, ILevelInputListener
    {
        private Action<Vector3> _onInputTrigger;

        public bool Hold;
        public bool Down;


        public void Initialize(Action<Vector3> onWorldInputTrigger)
        {
            _onInputTrigger = onWorldInputTrigger;
        }

        public Vector3 WorldInputPosition => CameraController.ScreenToWorldPoint(UnityEngine.Input.mousePosition)
        ;

        void Update()
        {
            if (Hold && UnityEngine.Input.GetMouseButton(0))
            {
                _onInputTrigger(WorldInputPosition);
            }

            if (Down && UnityEngine.Input.GetMouseButtonDown(0))
            {
                _onInputTrigger(WorldInputPosition);
            }
        }
    }
}