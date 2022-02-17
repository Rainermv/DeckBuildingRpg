using System;
using Assets.Scripts.Controller;
using Assets.Scripts.Model;
using Assets.Scripts.Model.CardModel;
using Assets.Scripts.Model.CharacterModel;
using Assets.Scripts.Systems.CardSystem.Constants;
using Assets.Scripts.Systems.CardSystem.Views;
using Assets.Scripts.Systems.GridSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts
{
    public class LevelView : SerializedMonoBehaviour
    {
        [SerializeField, AssetsOnly] private CharacterView _characterViewPrefab;

        [SerializeField, SceneObjectsOnly] private CardSystemView _cardView;
        [SerializeField, SceneObjectsOnly] private GridView _gridView;
        [SerializeField, SceneObjectsOnly] private Transform _charactersContainer;
        [SerializeField, SceneObjectsOnly] private ILevelInputListener _levelInputListener;

        private Action<GridPosition> _onGridPositionInput;


        public void Initialize(LevelModel levelModel, Action<CardView> onCardClicked,
            Action<CardCollectionView> onCardCollectionClicked, Action<GridPosition> onGridInputTrigger)
        {
            _onGridPositionInput = onGridInputTrigger;

            _levelInputListener.Initialize(
                (worldPosition => onGridInputTrigger(
                    _gridView.WorldToCell(new Vector3(worldPosition.x, worldPosition.y)))));

            _gridView.Initialize(levelModel.GridTileModel, () => _levelInputListener.WorldInputPosition);
            _cardView.Initialize(levelModel.Players,
                onCardClicked,
                onCardCollectionClicked);

            foreach (var character in levelModel.Characters)
            {
                var characterView = Instantiate(_characterViewPrefab, _charactersContainer);

                characterView.Initialize(character, _gridView.CellToWorld);
            }


            //CameraController..position += GridView.GridCenter;

        }


        /// <summary>
        /// ///////////////////////////////////////////////////////////
        /// </summary>

        private float minSize = 5;
        private float maxSize = 10;
        float sensitivity = 10f;
        private float speed = 10f;

        void Update()
        {
            var camOrthographicSize = CameraController.OrthographicSize - Input.GetAxis("Mouse ScrollWheel") * sensitivity;

            CameraController.OrthographicSize = Mathf.Clamp(camOrthographicSize, minSize, maxSize);
            
            CameraController.MoveCamera(Vector3.up * speed * Input.GetAxis("Vertical") * Time.deltaTime);;
            CameraController.MoveCamera(Vector3.right * speed * Input.GetAxis("Horizontal") * Time.deltaTime);

        }

        
    }
}