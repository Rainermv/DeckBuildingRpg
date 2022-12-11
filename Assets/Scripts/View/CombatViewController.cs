using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Model;
using Assets.Scripts.Core.Model.Cards;
using Assets.Scripts.View.Cards;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class CombatViewController : SerializedMonoBehaviour
    {
        [SerializeField, AssetsOnly, Required] private CharacterView _characterViewPrefab;

        [SerializeField, SceneObjectsOnly, Required] private CameraController _cameraController;
        [SerializeField, SceneObjectsOnly, Required] private CardCanvasViewController _cardCanvasViewController;
        //[SerializeField, SceneObjectsOnly, Required] private GriMapView _gridMapView;
        [SerializeField, SceneObjectsOnly, Required] private Transform _charactersContainer;
        
        //private Func<GridPosition, GridMapPathfindingModel> _onFindPathToTargetGrid;
        //private Func<Task<MovePathResult>> _onExecuteMovement;

        private List<CharacterView> _entityViews = new();

        public void Initialize(CombatModel battleModel,
            CardSpriteLibrary cardSpriteLibrary)
        {
          
            _cardCanvasViewController.Initialize(battleModel.Players, cardSpriteLibrary);


            //todo: move characters code to a lower level
            foreach (var entity in battleModel.Entities)
            {
                var characterView = Instantiate(_characterViewPrefab, _charactersContainer);

                characterView.Initialize(entity);
                _entityViews.Add(characterView);
            }

            _cameraController.SmoothJumpTo(_entityViews[0].transform.position);

        }



        
    }
}