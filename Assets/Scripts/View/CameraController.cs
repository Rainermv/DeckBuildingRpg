using UnityEngine;

namespace Assets.Scripts.View
{
    public static class CameraController
    {
        private const float CAM_Y = -10;
        public static float OrthographicSize
        {
            get => Camera.main.orthographicSize;
            set { Camera.main.orthographicSize = value; }
        }
        
        public static void JumpCameraTo(Vector3 worldPos)
        {
            Camera.main.transform.position = new Vector3(worldPos.x, worldPos.y, CAM_Y);
        }

        public static void MoveCamera(Vector3 movePosition)
        {
            Camera.main.transform.position += movePosition;
        }

        public static Vector3 ScreenToWorldPoint(Vector3 pos)
        {
            return Camera.main.ScreenToWorldPoint(pos);
        }


        
    }
}