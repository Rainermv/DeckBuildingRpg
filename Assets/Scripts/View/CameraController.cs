using System.Threading.Tasks;
using Codice.Client.Common.GameUI;
using Sirenix.OdinInspector;
using UnityEditor.UIElements;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class CameraController : SerializedMonoBehaviour
    {
        [SerializeField, SceneObjectsOnly] private Camera _camera;


        [Title("Smooth Jump")]
        [SerializeField] private AnimationCurve _smoothJumpCurve;
        [SerializeField] private float _smoothJumpSpeed = 2;
        [SerializeField, MinValue(0), MaxValue(1)] private float _smoothJumpNormalizedPositionFromBottom = 0.5f;

        [SerializeField] private float _minOrthograficSize = 5;
        [SerializeField] private float _maxOrthograficSize = 10;
        [SerializeField] private float _zoomSpeed = 10f;
        [SerializeField] private float _moveSpeed = 10f;
        [SerializeField] private float  _cameraYDistance = -10;

        private const string MouseScrollwheel = "Mouse ScrollWheel";
        private const string AxisVertical = "Vertical";
        private const string AxisHorizontal = "Horizontal";

        public void JumpTo(Vector3 worldPos)
        {
            transform.position = new Vector3(worldPos.x, worldPos.y, _cameraYDistance);
            
        }

        public async void SmoothJumpTo(Vector3 target)
        {
            var t = 0f;
            var start = transform.position;

            var adjust = Mathf.Lerp(
                _camera.orthographicSize, 
                -_camera.orthographicSize, 
                _smoothJumpNormalizedPositionFromBottom);
            target = new Vector3(target.x, target.y + adjust, _cameraYDistance);
            
            while (t < 1f)
            {
                var curve = _smoothJumpCurve.Evaluate(t);
                transform.position = Vector3.Lerp(start, target, curve);
                t += Time.deltaTime * _smoothJumpSpeed;

                await Task.Yield();
            }


        }

        public void Move(Vector3 movePosition)
        {
            transform.position += movePosition;
        }
        
        public void Zoom(float zoom)
        {
            _camera.orthographicSize = Mathf.Clamp(
                _camera.orthographicSize - zoom, 
                _minOrthograficSize, 
                _maxOrthograficSize);
        }

        public static Vector3 ScreenToWorldPoint(Vector3 pos)
        {
            return Camera.main.ScreenToWorldPoint(pos);
        }
        
        void Update()
        {
            Zoom(UnityEngine.Input.GetAxis(MouseScrollwheel) * _zoomSpeed);
            

            Move(Vector3.up * _moveSpeed * UnityEngine.Input.GetAxis(AxisVertical) * Time.deltaTime); ;
            Move(Vector3.right * _moveSpeed * UnityEngine.Input.GetAxis(AxisHorizontal) * Time.deltaTime);

        }

        
    }
}